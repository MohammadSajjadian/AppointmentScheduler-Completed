using Appointment_Scheduler.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Quartz;
using SmsIrRestful;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Job
{
    [DisallowConcurrentExecution]
    public class SmsJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var option = new DbContextOptionsBuilder<DBscheduler>();
            option.UseSqlServer("Server=MOHAMMAD-PC2\\MSSQLSERVER2019;Database=DBscheduler;Trusted_Connection=True;MultipleActiveResultSets=true");

            using (DBscheduler db = new(option.Options))
            {
                db.appointments.Include(x => x.applicationUser).ToList();
                List<Appointment> appointments = db.appointments.Where(x => x.time.Date.Date == DateTime.Now.Date.Date && x.time.Hour == DateTime.Now.Hour && x.time.Minute == DateTime.Now.Minute).ToList();

                foreach (var item in appointments)
                {
                    #region Send SMS

                    var token = new Token().GetToken("-", "-");

                    var messageSendObject = new MessageSendObject()
                    {
                        Messages = new List<string> { $"قرار شما فرا رسید!\n'{item.description}'" }.ToArray(),
                        MobileNumbers = new List<string> { item.applicationUser.PhoneNumber }.ToArray(),
                        LineNumber = "-",
                        SendDateTime = DateTime.Now,
                        CanContinueInCaseOfError = false,
                    };

                    MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);

                    #endregion

                    #region Delete Appointment

                    db.Remove(item);

                    #endregion
                }

                db.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
