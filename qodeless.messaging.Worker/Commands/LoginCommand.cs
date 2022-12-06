using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using qodeless.DataManager.Seeds;
using qodeless.domain.Entities.ACL;
using qodeless.Infra.CrossCutting.Identity.Entities;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Request;
using qodeless.messaging.Worker.ViewModels.Response;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading.Tasks;

namespace qodeless.messaging.Worker.Commands
{
    public class LoginCommand : BaseCommand<LoginRequest, LoginResponse>
    {
        public override LoginResponse Run(TcpClient server, byte[] data)
        {
            var request = Validate(data);
            if (request == null) return new LoginResponse() { Status = ECommandStatus.NAK };

            return SignInAsync(request).Result;
        }
        public async Task<LoginResponse> SignInAsync(LoginRequest request)
        {
            try
            {
                //AppService for DB Queries/Inserts
                var serviceProvider = ServiceManager.Instance.serviceProvider;
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var _signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
                    _signInManager.Context = new DefaultHttpContext { RequestServices = scope.ServiceProvider };
                    var user = _signInManager.UserManager.Users.SingleOrDefault(user => user.PhoneNumber == request.Phone);
                    var result = await _signInManager.PasswordSignInAsync(user.Email, request.Password, false, true);
                    if (result.Succeeded)
                    {
                        return new LoginResponse()
                        {
                            Status = ECommandStatus.ACK,
                            UserId = user.Id,
                        };
                    }
                }
                return new LoginResponse()
                {
                    Status = ECommandStatus.FAIL
                };
            }
            catch (System.Exception e)
            {
                return new LoginResponse()
                {
                    Status = ECommandStatus.FAIL
                };
            }
        }
    }
}
