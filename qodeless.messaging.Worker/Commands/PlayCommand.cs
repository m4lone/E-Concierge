using Microsoft.Extensions.DependencyInjection;
using qodeless.messaging.Worker.Enum;
using qodeless.messaging.Worker.Manager;
using qodeless.messaging.Worker.ViewModels.Response;
using System.Net.Sockets;
using qodeless.application;
using System;
using qodeless.application.ViewModels;
using qodeless.messaging.Worker.ViewModels.Request;
using qodeless.application.Interfaces;
using System.Collections.Generic;
using qodeless.domain.Enums;
using System.Linq;

namespace qodeless.messaging.Worker.Commands
{
    public class PlayCommand : BaseCommand<PlayRequest, PlayResponse>
    {
        public override PlayResponse Run(TcpClient client, byte[] data)
        {
            try
            {
                //Validate Payload Message
                var request = Validate(data);
                if (request == null) return new PlayResponse() { Status = ECommandStatus.NAK };

                #region BUSINESS_LOGIC_LAYER_HERE

                //AppService for DB Queries/Inserts
                var serviceProvider = ServiceManager.Instance.serviceProvider;
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    var PlayAppService = scope.ServiceProvider.GetRequiredService<IPlayAppService>();

                    var DeviceGame = scope.ServiceProvider.GetRequiredService<IDeviceGameAppService>().GetGameByDeviceId(request.DeviceId);
                    var Device = scope.ServiceProvider.GetRequiredService<IDeviceAppService>().GetById(request.DeviceId);
                    var sitePlayerAppService = scope.ServiceProvider.GetRequiredService<ISitePlayerAppService>();
                    var SitePlayer = sitePlayerAppService.GetBySiteIdPlayerId(request.SiteId, request.UserPlayId);
                    var virtualBalanceAppService = scope.ServiceProvider.GetRequiredService<IVirtualBalanceAppService>();

                    List<StatusResponse> statusResponse = new List<StatusResponse>();

                    foreach (var localPlay in request.LocalPlays)
                    {
                        var session = new PlayViewModel()
                        {
                            Id = Guid.NewGuid(),
                            SessionDeviceId = request.SessionDeviceId,
                            AmountPlay = localPlay.AmountPlay,
                            AmountExtraball = localPlay.AmountExtraball,
                            DeviceId = request.DeviceId,
                            GameId = DeviceGame.GameId,
                            SiteId = request.SiteId,
                            UserPlayId = request.UserPlayId,
                            Email = SitePlayer.Email,
                            LastIpAdress = "74.102.177.36", //TODO: Capturar IP
                            DeviceCode = Device.Code,
                            GameName = DeviceGame.GameName,
                            SiteName = SitePlayer.SiteName,
                            CreatedAt = localPlay.CreatedAt
                        };

                        var virtualBalance = new VirtualBalanceViewModel()
                        {
                            Amount = (double)((localPlay.AmountPlay + localPlay.AmountExtraball) /100m),
                            BalanceState = EBalanceState.Paid,
                            Description = $"Transação resultante de uma jogada",
                            Type = localPlay.AmountPlay < 0 ? EBalanceType.Debit : EBalanceType.Credit,
                            UserPlayId = request.UserPlayId,
                        };

                        if (!virtualBalanceAppService.Add(virtualBalance).IsValid)
                        {
                            return new PlayResponse()
                            {
                                Status = ECommandStatus.FAIL
                            };
                        }
                        var result = PlayAppService.Add(session);//TODO: Adrian aqui!
                        statusResponse.Add(new StatusResponse(localPlay.RowId, result.IsValid));
                    }
                    SitePlayer.CreditAmount += request.LocalPlays.Where(x => x.AmountPlay > 0).Sum(x => x.AmountPlay) + request.LocalPlays.Where(x => x.AmountExtraball > 0).Sum(x => x.AmountExtraball);
                    if (sitePlayerAppService.Update(SitePlayer).IsValid)
                    {
                        return new PlayResponse()
                        {
                            StatusResponses = statusResponse,
                            Status = ECommandStatus.ACK
                        };
                    }
                }
                #endregion

                return new PlayResponse()
                {
                    Status = ECommandStatus.FAIL
                };
            }
            catch (Exception e)
            {
                return new PlayResponse()
                {   
                    Status = ECommandStatus.FAIL
                };
            }
        }
    }    
}