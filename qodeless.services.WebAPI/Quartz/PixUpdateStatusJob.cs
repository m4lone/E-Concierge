using Microsoft.Extensions.DependencyInjection;
using qodeless.application;
using qodeless.application.Managers.Pix;
using qodeless.application.ViewModels;
using qodeless.domain.Enums;
using qodeless.Infra.CrossCutting.Identity.Data;
using Qodeless.Domain.Enum;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.WebApi.Quartz
{
    public class PixUpdateStatusJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private PixManager _pixManager { get; set; } = new PixManager();
        public PixUpdateStatusJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"PixUpdateStatusJob: {DateTime.Now}");
            Console.WriteLine($"JobKey: {context.JobDetail.Key}");
            var responsePix = new ResponsePixViewModel();
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var orderServices = scope.ServiceProvider.GetService<IOrderServices>();
                    var sitePlayerServices = scope.ServiceProvider.GetService<ISitePlayerAppService>();
                    var incomeAppService = scope.ServiceProvider.GetService<IIncomeAppService>();
                    var siteAppService = scope.ServiceProvider.GetService<ISiteAppService>();
                    var db = scope.ServiceProvider.GetService<IAppDbContext>();
                    var users = db.Users.ToList();
                    Console.WriteLine($"Collecting Order");
                    var orders = orderServices.GetAllBy(x => x.EOrderStatus.Equals(EOrderStatus.Unpaid)).ToList();
                    if (orders.Count.Equals(0))
                    {
                        Console.WriteLine($"Do not contain orders");
                        return Task.CompletedTask;
                    }
                    Console.WriteLine($"Collected Order: {orders.Count}");
                    foreach (var user in users)
                    {
                        foreach (var order in orders.Where(c => c.UserId == user.Id))
                        {
                            var authToken = _pixManager.GetPixAuth("7", "43QESGXSHDi1eAY5L1cd4eOaT8eIH8BpG39UgydM");
                            if (authToken != null)
                            {
                                responsePix = _pixManager.GetPixStatus(authToken.AccessToken, order.PixId);
                                if (responsePix != null)
                                {
                                    if (responsePix.Paid)
                                    {
                                        Console.WriteLine($"Update Order: order code -> {order.Code}");
                                        var obj = new OrderViewModel
                                        {
                                            Id = order.Id,
                                            Code = order.Code,
                                            Date = order.Date,
                                            EOrderStatus = !responsePix.IsRefunded ? EOrderStatus.PaidOut : EOrderStatus.Returned,
                                            PixId = order.PixId,
                                            QrCode = order.QrCode,
                                            Value = order.Value,
                                            Fee = order.Fee,
                                            UserId = user.Id,
                                            SiteId = order.SiteId,
                                            AccountId = order.AccountId
                                        };
                                        orderServices.Update(obj);
                                        Console.WriteLine($"Create Income");
                                        CreateIncome(incomeAppService,siteAppService,obj);
                                        Console.WriteLine($"Update SitePlayer");
                                        var sitePlayerObj = sitePlayerServices.GetBySiteIdPlayerId((Guid)(order.SiteId == null ? Guid.Empty : order.SiteId), user.Id);
                                        if (sitePlayerObj != null)
                                        {
                                            sitePlayerObj.CreditAmount += (int)order.Value;
                                            sitePlayerServices.Update(sitePlayerObj);
                                        }
                                    }
                                    else if (DateTime.Now > responsePix.ExpirationDate)
                                    {
                                        var obj = new OrderViewModel
                                        {
                                            Id = order.Id,
                                            Code = order.Code,
                                            Date = order.Date,
                                            EOrderStatus = EOrderStatus.Expired,
                                            PixId = order.PixId,
                                            QrCode = order.QrCode,
                                            Value = order.Value,
                                            Fee = order.Fee,
                                            UserId = user.Id,
                                            SiteId = order.SiteId,
                                            AccountId = order.AccountId
                                        };
                                        orderServices.Update(obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
                return Task.CompletedTask;
            }

            Console.WriteLine($"CompletedTask");
            return Task.CompletedTask;
        }
        private void CreateIncome(IIncomeAppService incomeAppService, ISiteAppService siteAppService, OrderViewModel order)
        {

            var site = siteAppService.GetById((Guid)(order.SiteId == null? Guid.Empty : order.SiteId));
            var siteId = Guid.Empty;
            var accountId = Guid.Empty;
            if (order.AccountId == null && order.SiteId != null)
            {
                siteId = site.Id;
            }else if (order.AccountId != null && order.SiteId != null)
            {
                siteId = site.Id;
                accountId = site.AccountId;
            }

            incomeAppService.Add(new IncomeViewModel() 
            {
                Amount = (double)(order.Value / 100),
                SiteId = siteId.Equals(Guid.Empty) ? null : siteId,
                AccountId = accountId.Equals(Guid.Empty) ? null : accountId,
                Description = GetIncomeDescription(accountId,siteId),
                Type = EIncomeType.Pix
            });
        }
        private string GetIncomeDescription(Guid? accountId, Guid? siteId)
        {
            if (accountId == null && siteId != null)
            {
                return "Pagamento de fatura via pix.";
            }
            else if (accountId != null && siteId != null)
            {
                return "Inserido créditos via pix.";
            }
            return "Receita gerada via pix.";
        }
    }
}
