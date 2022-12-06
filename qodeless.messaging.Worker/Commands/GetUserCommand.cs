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
    public class GetUserCommand : BaseCommand<GetUserRequest, GetUserResponse>
    {

        public override GetUserResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null)
                return new GetUserResponse() { Status = ECommandStatus.NAK };

            return GetUserByPhone(request);
        }

        public GetUserResponse GetUserByPhone(GetUserRequest request)
        {
            #region BUSINESS_LOGIC_LAYER_HERE
            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var identityUser = _userManager.FindByIdAsync(request.UserId).Result;

                if (identityUser != null)
                {
                    try
                    {
                        return new GetUserResponse()
                        {
                            Status = ECommandStatus.ACK,
                            AspNetUserId = identityUser.Id,
                            BirthDate = identityUser.BirthDate,
                            Cpf = !string.IsNullOrEmpty(identityUser.Cpf) ? identityUser.Cpf : string.Empty,
                            Email = !string.IsNullOrEmpty(identityUser.Email) ?
                                (identityUser.Email.Contains(identityUser.PhoneNumber) ? string.Empty : identityUser.Email) : string.Empty,
                            FullName = !string.IsNullOrEmpty(identityUser.FullName) ? identityUser.FullName : string.Empty,
                            NickName = !string.IsNullOrEmpty(identityUser.NickName) ? identityUser.NickName : string.Empty,
                            Phone = !string.IsNullOrEmpty(identityUser.PhoneNumber) ? identityUser.PhoneNumber : string.Empty,
                            PixKey = !string.IsNullOrEmpty(identityUser.PixKey) ? identityUser.PixKey : string.Empty,
                            Gender = identityUser.Gender,
                        }; 
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                        throw;
                    }
                }
                return new GetUserResponse()
                {
                    Status = ECommandStatus.FAIL,
                    AspNetUserId = string.Empty,
                    BirthDate = DateTime.MinValue,
                    Cpf = string.Empty,
                    Email = string.Empty,
                    FullName = string.Empty,
                    NickName = string.Empty,
                    Phone = string.Empty,
                    PixKey = string.Empty,
                    Gender = 0
                };

            }
            #endregion //BUSINESS_LOGIC_LAYER_HERE
        }
    }
}
