using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.DataManager.Seeds;
using qodeless.domain.Entities.ACL;
using qodeless.domain.Extensions;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Request;
using qodeless.messaging.Worker.ViewModels.Response;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;

namespace qodeless.messaging.Worker.Commands
{
    public class RegisterCommand : BaseCommand<RegisterRequest, RegisterResponse>
    {

        public override RegisterResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new RegisterResponse() { Status = ECommandStatus.NAK };

            return Register(request);
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var generatedEmail = $"{request.Phone}@rgdigital.com.br";

                if (ValidateRequest(request))
                {
                    return new RegisterResponse() { Status = ECommandStatus.FAIL, AspNetUserId = string.Empty };
                }

                var user = new ApplicationUser
                {
                    PhoneNumber = request.Phone,
                    NickName = request.NickName,
                    BirthDate = request.BirthDate,
                    UserName = generatedEmail,
                    Email = generatedEmail,
                    Enabled = true,
                    CreationDate = DateTime.Now,
                    RegisterCompleted = false
                };

                IdentityUser identityUser = _userManager.FindByNameAsync(generatedEmail).Result;
                if (identityUser == null)
                {
                    var result = _userManager.CreateAsync(user, request.Password).Result;
                    if (!result.Succeeded)
                    {
                        return new RegisterResponse() { Status = ECommandStatus.FAIL, AspNetUserId = string.Empty };
                    }
                    else
                    {
                        _ = _userManager.AddToRoleAsync(user, Role.PLAYER).Result;
                    }
                }

                var roleClaims = SeederBase.LoadClaimsFromFile(Directory.GetCurrentDirectory());
                foreach (var policy in roleClaims.Where(c => c.Role == Role.PLAYER))
                {
                    foreach (var claim in policy.Claims)
                    {
                        _userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value));
                    }
                }
                return new RegisterResponse()
                {
                    Status = ECommandStatus.ACK,
                    AspNetUserId = identityUser != null ? identityUser.Id : user.Id,
                };
            }
            return new RegisterResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }

        private bool ValidateRequest(RegisterRequest request)
        {
            if (request.NickName.Length > 12 || request.Password.ContainsLetter() || request.BirthDate.AddYears(18) > DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
