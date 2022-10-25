using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class AppointmentViewModel
    {
        [Required(ErrorMessage ="زمان را وارد کنید.")]
        public DateTime time { get; set; }

        public string description { get; set; }
    }
}
