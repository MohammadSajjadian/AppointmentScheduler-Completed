using System;
using Appointment_Scheduler.Areas.Identity.Data;
using Appointment_Scheduler.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Appointment_Scheduler.Areas.Identity.IdentityHostingStartup))]
namespace Appointment_Scheduler.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DBscheduler>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DBschedulerConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<DBscheduler>();
            });
        }
    }
}