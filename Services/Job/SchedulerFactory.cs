using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Job
{
    public class SchedulerFactory : IJobFactory
    {
        #region Dependency Injeciton

        private readonly IServiceProvider serviceProvider;

        public SchedulerFactory(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        
        #endregion

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
