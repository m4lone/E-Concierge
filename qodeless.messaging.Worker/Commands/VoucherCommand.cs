using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using qodeless.application;
using qodeless.application.Interfaces;
using qodeless.application.ViewModels;
using qodeless.DataManager.Seeds;
using qodeless.domain.Entities.ACL;
using qodeless.domain.Enums;
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
    public class VoucherCommand : BaseCommand<VoucherRequest, VoucherResponse>
    {

        public override VoucherResponse Run(TcpClient client, byte[] data)
        {
            //Validate Payload Message
            var request = Validate(data);
            if (request == null) return new VoucherResponse() { Status = ECommandStatus.NAK };

            return Voucher(request);
        }
        public VoucherResponse Voucher(VoucherRequest request)
        {
            //AppService for DB Queries/Inserts
            var serviceProvider = ServiceManager.Instance.serviceProvider;
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                try
                {
                    var voucherAppService = scope.ServiceProvider.GetRequiredService<IVouscherAppService>();
                    var virtualBalanceAppService = scope.ServiceProvider.GetRequiredService<IVirtualBalanceAppService>();
                    var sitePlayerAppService = scope.ServiceProvider.GetRequiredService<ISitePlayerAppService>();

                    var voucher = voucherAppService.GetBy(x => x.QrCodeKey.Equals(request.Code));
                    var sitePlayer = sitePlayerAppService.GetBySiteIdPlayerId(request.SiteId,request.UserId);
                    var virtualBalance = virtualBalanceAppService.GetAllBy(x => x.VoucherId == voucher.Id).FirstOrDefault();

                    if (voucher != null && ValidadeVoucher(voucher,request) && sitePlayer != null)
                    {
                        voucher.IsActive = false;
                        sitePlayer.CreditAmount += (int)(voucher.Amount * 100);
                        virtualBalance.UserPlayId = request.UserId;
                        virtualBalance.BalanceState = EBalanceState.Paid;
                        return ResponseValidate(voucherAppService.Update(voucher), virtualBalanceAppService.Update(virtualBalance), sitePlayerAppService.Update(sitePlayer), sitePlayer);
                    }
                    return new VoucherResponse()
                    {
                        Status = ECommandStatus.FAIL,
                        Message = "Voucher Invalido, entre em contato com o vendedor/atendente!",
                        Success = false
                    };
                }
                catch (Exception e)
                {
                    return new VoucherResponse()
                    {
                        Status = ECommandStatus.FAIL,
                        Message = e.Message,
                        Success = false
                    };
                }
            }
        }
        public bool ValidadeVoucher(VouscherViewModel voucher, VoucherRequest request)
        {
            if (voucher.IsActive && voucher.SiteID.Equals(request.SiteId) && voucher.DueDate > DateTime.Now)
            {
                return true;
            }
            return false;
        }
       
        public VoucherResponse ResponseValidate(ValidationResult voucherResult, ValidationResult virtualBalanceResult, ValidationResult sitePlayerResult, SitePlayerViewModel sitePlayer)
        {
            if (voucherResult.IsValid && virtualBalanceResult.IsValid && sitePlayerResult.IsValid)
            {
                return new VoucherResponse()
                {
                    Status = ECommandStatus.ACK,
                    Message = $"Créditos adicionados com sucesso!",
                    Success = true,
                    CreditAmount = sitePlayer.CreditAmount
                };
            }
            var errorMessage = !virtualBalanceResult.IsValid ? virtualBalanceResult.Errors.FirstOrDefault().ErrorMessage : 
                (!voucherResult.IsValid ? voucherResult.Errors.FirstOrDefault().ErrorMessage : 
                    (!sitePlayerResult.IsValid ? voucherResult.Errors.FirstOrDefault().ErrorMessage : "Processo abortado, procure o vendedor/atendente!")
                );
            return new VoucherResponse()
            {
                Status = ECommandStatus.FAIL,
                Message = $"{errorMessage}",
                Success = false
            };
        }
    }
}
