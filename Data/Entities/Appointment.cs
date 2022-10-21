using Appointment_Scheduler.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Appointment
    {
        public int id { get; set; }

        public DateTime time { get; set; }

        public string description { get; set; }

        #region ForeignKey

        public string userId { get; set; }
        [ForeignKey(nameof(userId))]
        public ApplicationUser applicationUser { get; set; }

        #endregion
    }
}
