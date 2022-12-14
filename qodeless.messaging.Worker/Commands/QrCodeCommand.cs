using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using qodeless.application.Interfaces;
using qodeless.DataManager.Seeds;
using qodeless.domain.Entities.ACL;
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
    public class QrCodeCommand : BaseCommand<JoinRequest, JoinResponse>
    {

        public override JoinResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new JoinResponse() { Status = ECommandStatus.NAK };

            #region BUSINESS_LOGIC_LAYER_HERE

            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var tokenAppService = scope.ServiceProvider.GetRequiredService<ITokenAppService>();
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var generatedEmail = $"{request.Phone}@rgdigital.com.br";
                var token = tokenAppService.GetAllBy(c => c.Phone == request.Phone && c.DtExpiration >= DateTime.Now.AddMinutes(-3)).Result.FirstOrDefault();
                if (token != null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = generatedEmail,
                        Email = generatedEmail,
                        Enabled = true,
                    };

                    IdentityUser identityUser = _userManager.FindByNameAsync(generatedEmail).Result;
                    if(identityUser == null)
                    {
                        var result = _userManager.CreateAsync(user, "!Asdf1904").Result; // Technical Debt
                        if (!result.Succeeded)
                        {
                            return new JoinResponse()
                            {
                                Status = ECommandStatus.FAIL,
                                AspNetUserId = string.Empty
                            };
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
                    return new JoinResponse()
                    {
                        Status = ECommandStatus.ACK,
                        AspNetUserId = user.Id
                    };
                }
            }
            #endregion //BUSINESS_LOGIC_LAYER_HERE

            return new JoinResponse()
            {
                Status = ECommandStatus.FAIL
            };
        }
    }    
}
