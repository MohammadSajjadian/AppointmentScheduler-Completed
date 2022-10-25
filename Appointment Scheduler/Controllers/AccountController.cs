using Appointment_Scheduler.Areas.Identity.Data;
using Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Scheduler.Controllers
{
    public class AccountController : Controller
    {
        #region KeyWords

        private const string success = "success";
        private const string error = "error";
        private const string info = "info";
        private const string warning = "warning";

        #endregion

        #region Dependency Injection

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        
        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        #endregion


        #region Login

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.phoneNumber);

            if (user != null)
            {
                var status = await signInManager.PasswordSignInAsync(user, model.password, model.rememberMe, false);

                if (status.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData[error] = "نام کاربری یا رمز عبور اشتباه است.";

                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData[error] = "کاربر مورد نظر یافت نشد.";

                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        #endregion


        #region Register

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.phoneNummber);

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = model.phoneNummber,
                    PhoneNumber = model.phoneNummber,
                    EmailConfirmed = true
                };

                var status = await userManager.CreateAsync(user, model.password);

                if (status.Succeeded)
                {
                    await signInManager.PasswordSignInAsync(user, model.password, true, false);

                    TempData[success] = "ثبت نام با موفقیت انجام شد.";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData[error] = "فرمت نام کاربری یا رمز عبور اشتباه است.";

                    return RedirectToAction(nameof(Register));
                }
            }
            else
            {
                TempData[info] = "کاربر مورد موجود میباشد. لطفا وارد شوید.";

                return RedirectToAction(nameof(Register));
            }
        }

        #endregion
    }
}
