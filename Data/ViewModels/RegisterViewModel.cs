using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="شماره تلفن را وارد کنید.")]
        public string phoneNummber { get; set; }

        [Required(ErrorMessage = "رمز عبور را وارد کنید.")]
        public string password { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور را وارد کنید.")]
        [Compare(nameof(password), ErrorMessage ="رمز عبور با تکرار رمز عبور برابر نیست.")]
        public string rePassword { get; set; }
    }
}
