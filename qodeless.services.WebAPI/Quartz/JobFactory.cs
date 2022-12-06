using Microsoft.Extensions.DependencyInjection;
using qodeless.application;
using Quartz;
using Quartz.Spi;
using System;

namespace qodeless.WebApi.Quartz
{
    public class JobFactory : IJobFactory
    {
        protected readonly IServiceProvider Container;
        protected readonly IOrderServices _orderServices;

        public JobFactory(IServiceProvider container)
        {
            try
            {
                Container = container;
                using (IServiceScope scope = Container.CreateScope())
                {
                    //Adrian, aqui
                    _orderServices = scope.ServiceProvider.GetService<IOrderServices>();
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return Container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
