using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appointment_Scheduler.Areas.Identity.Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Appointment_Scheduler.Data
{
    public class DBscheduler : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Appointment> appointments { get; set; }

        public DBscheduler(DbContextOptions<DBscheduler> options)
            : base(options)
        {
        }
    }
}
