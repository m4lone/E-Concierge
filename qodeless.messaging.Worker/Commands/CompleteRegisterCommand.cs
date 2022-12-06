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
    public class CompleteRegisterCommand : BaseCommand<CompleteRegisterRequest, CompleteRegisterResponse>
    {

        public override CompleteRegisterResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null)
                return new CompleteRegisterResponse() { Status = ECommandStatus.NAK };

            return CompleteRegister(request);
        }

        public CompleteRegisterResponse CompleteRegister(CompleteRegisterRequest request)
        {
            #region BUSINESS_LOGIC_LAYER_HERE
            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (ValidateRequest(request))
                {
                    return new CompleteRegisterResponse() { Status = ECommandStatus.FAIL, AspNetUserId = string.Empty };
                }
                var identityUser = _userManager.Users.SingleOrDefault(x => x.PhoneNumber.Equals(request.Phone));

                if(identityUser != null)
                {
                    identityUser.Cpf = request.Cpf;
                    identityUser.PhoneNumber = request.Phone;
                    identityUser.NickName = request.NickName;
                    identityUser.FullName = request.FullName;
                    identityUser.PixKey = request.PixKey;
                    identityUser.Email = request.Email;
                    identityUser.NormalizedEmail = request.Email.ToUpper();
                    identityUser.UserName = request.Email;
                    identityUser.NormalizedUserName = request.Email.ToUpper();
                    identityUser.RegisterCompleted = true;
                    identityUser.Gender = request.Gender;

                    var result = _userManager.UpdateAsync(identityUser).Result;

                    if (!result.Succeeded)
                    {
                        return new CompleteRegisterResponse() { Status = ECommandStatus.FAIL, AspNetUserId = string.Empty };
                    }
                    return new CompleteRegisterResponse()
                    {
                        Status = ECommandStatus.ACK,
                        AspNetUserId = identityUser.Id,
                    };
                }
                return new CompleteRegisterResponse() { Status = ECommandStatus.FAIL, AspNetUserId = string.Empty };

            }
            #endregion //BUSINESS_LOGIC_LAYER_HERE
        }

        private bool ValidateRequest(CompleteRegisterRequest request)
        {
            if (request.NickName.Length > 12 || !request.Cpf.IsValidCpf())
            {
                return true;
            }
            return false;
        }
    }
}
