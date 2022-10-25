using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="شماره تلفن را وارد کنید.")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید.")]
        public string password { get; set; }

        public bool rememberMe { get; set; }
    }
}
