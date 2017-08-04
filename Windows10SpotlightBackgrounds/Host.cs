using ImageProcessor;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows10SpotlightBackgrounds
{
    class Host
    {
        private static IScheduler _Scheduler;

        public virtual void Start()
        {
            try
            {
                ISchedulerFactory schedFactory = new StdSchedulerFactory();
                _Scheduler = schedFactory.GetScheduler();

                ITrigger trigger1 = TriggerBuilder.Create()
                                                 .WithIdentity("UpdateImageJobTrigger", "UpdateImageGroup")
                                                 .StartNow()
                                                 .WithSimpleSchedule(x => x
                                                     .WithIntervalInMinutes(300)
                                                     .RepeatForever()
                                                     .WithMisfireHandlingInstructionIgnoreMisfires())
                                                 .Build();

                IJobDetail jobCampaignRuleOrderer = JobBuilder.Create<ImageCopyJob>()
                                           .WithIdentity("ImageCopyJob", "UpdateImageGroup")
                                           .Build();

                _Scheduler.ScheduleJob(jobCampaignRuleOrderer, trigger1);
                _Scheduler.Start();
            }
            catch (Exception ex)
            {
            }
        }

        public virtual void Stop()
        {
            try
            {
                if (_Scheduler != null)
                {
                    _Scheduler.Shutdown();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
