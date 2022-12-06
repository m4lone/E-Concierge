using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.Infra.CrossCutting.Identity.Entities.AccountViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using qodeless.Infra.CrossCutting.Identity.Data;
using qodeless.domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using qodeless.services.WebApi.Model;

namespace qodeless.services.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSetup _jwtSetup;


        public AuthController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWTSetup> jwtSetup
            ) : base(db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSetup = jwtSetup.Value;
        }

        /// <summary>
        /// Autentica usuário e via Unity
        /// </summary>
        /// <param name="vm">
        /// LoginUnityViewModel:
        /// Phone, Password
        /// </param>
        /// <returns>Token JWT</returns>
        [HttpPost("signInUnity")]
        public async Task<IActionResult> SignInUnity([FromBody] LoginUnityViewModel vm)
        {
            if (!ModelState.IsValid) return ModelStateError();
            var user = _signInManager.UserManager.Users.SingleOrDefault(user => user.PhoneNumber == vm.Phone);
            var result = await _signInManager.PasswordSignInAsync(user.Email, vm.Password, false, true);

            if (result.Succeeded)
            {
                if (user == null)
                {
                    return Response(success: false, errorMessage: "invalid User");
                }

                return Response(
                    new LoginUnityResponseVM
                    {
                        NickName = user.NickName,
                        UserId = user.Id,
                        PhoneNumber = user.PhoneNumber,
                        RegisterCompleted = user.RegisterCompleted,
                        RecSites = GetSiteByUserId(user.Id)
                    }
                );
            }

            return Response(success: false, errorMessage: "invalid user or password");
        }

        /// <summary>
        /// Autentica usuário e gera o token JWT
        /// </summary>
        /// <param name="vm">
        /// LoginViewModel:
        /// Email, Password
        /// </param>
        /// <returns>Token JWT</returns>
        [HttpPost("token")]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel vm)
        {
            if (!ModelState.IsValid) return ModelStateError();
            ApplicationUser signedUser = await _userManager.FindByEmailAsync(vm.Email);
            var result = await _signInManager.PasswordSignInAsync(signedUser.Email, vm.Password, false, true);

            if (result.Succeeded)
            {
                var user = GetUser(vm.Email);

                if (user == null)
                {
                    return Response(success: false, errorMessage: "invalid User");
                }

                var token = await GenerateJWTToken(user);
                return Response(
                    new
                    {
                        token = token,
                        user = user
                    }
                );
            }

            return Response(success: false, errorMessage: "invalid user or password");
        }

        /// <summary>
        /// Return logged user 
        /// </summary>
        /// <returns>User</returns>
        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            return Response(new { user = GetUser(User.Identity.Name) });
        }

        private async Task<string> GenerateJWTToken(IUserDataModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetup.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                }),

                Issuer = _jwtSetup.Emiter,
                Audience = _jwtSetup.ValidOn,
                Expires = DateTime.UtcNow.AddHours(_jwtSetup.Duration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var claims = GetDefaultClaims(user.RoleId, user);
            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    tokenDescriptor.Subject.AddClaim(new Claim(claim.ClaimType, claim.ClaimValue));
                }
            }
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        /// <summary>
        /// Reset User Password
        /// </summary>
        /// <returns>User</returns>
        [HttpPut("ResetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(string actualPassword, string newPassword, string email)
        {
            if (newPassword == null || actualPassword == null)
            {
                return BadRequest("Current password or new password required!");
            };
            if (actualPassword == newPassword)
            {
                return BadRequest("You cannot use the same password!");
            };

            ApplicationUser user = _userManager.FindByEmailAsync(email).Result;
            var validPassword = _userManager.CheckPasswordAsync(user, actualPassword);
            if (!validPassword.Result)
            {
                return BadRequest("Password does not match!");
            };

            var token = _userManager.GeneratePasswordResetTokenAsync(user);
            var result = _userManager.ResetPasswordAsync(user, token.Result, newPassword);
            if(result.Result.Errors.Count() >= 1)
            {
                return BadRequest(result.Result.Errors);
            }
            return Response(result.IsCompletedSuccessfully);
        }
    }
}
