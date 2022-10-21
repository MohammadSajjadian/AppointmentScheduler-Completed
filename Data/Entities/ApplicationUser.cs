using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Appointment_Scheduler.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Appointment> appointments { get; set; }
    }
}
