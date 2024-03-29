﻿using Appointment_Scheduler.Areas.Identity.Data;
using Appointment_Scheduler.Data;
using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Spi;
using Services.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Scheduler.Controllers
{
    public class AppointmentController : Controller
    {
        #region KeyWords

        private const string success = "success";
        private const string error = "error";
        private const string info = "info";
        private const string warning = "warning";

        #endregion

        #region Dependency Injection

        private readonly DBscheduler db;
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentController(DBscheduler _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }

        #endregion


        [Authorize]
        public IActionResult InsertAppointment()
        {
            return View();
        }


        [Authorize]
        public IActionResult AppointmentList()
        {
            return View(db.appointments.OrderBy(x => x.time).ToList());
        }


        [Authorize]
        public async Task<IActionResult> InsertAppointmentConfirm(AppointmentViewModel model)
        {
            try
            {
                ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);

                Appointment appointment = new()
                {
                    time = model.time,
                    description = model.description,
                    userId = user.Id
                };

                db.Add(appointment);
                db.SaveChanges();

                TempData[success] = "قرار ملاقات با موفقیت ثبت شد.";
            }
            catch
            {
                TempData[error] = "ثبت قرار ملاقات با شکست مواجه شد.";
            }

            return RedirectToAction(nameof(InsertAppointment));
        }


        [Authorize]
        public IActionResult UpdateAppointment(int appointmentId)
        {
            return View(db.Find<Appointment>(appointmentId));
        }


        [Authorize]
        public IActionResult UpdateAppointmentConfirm(int appointmentId, DateTime time, string Description)
        {
            try
            {
                Appointment appointment = db.Find<Appointment>(appointmentId);

                appointment.time = time;
                appointment.description = Description;

                db.SaveChanges();

                TempData[success] = "قرار ملاقات با موفقیت ویرایش شد.";
            }
            catch
            {
                TempData[error] = "ویرایش قرار ملاقات با شکست مواجه شد.";
            }

            return RedirectToAction(nameof(AppointmentList));
        }


        public IActionResult SearchAppointment(DateTime time)
        {
            return View(db.appointments.Where(x => x.time.Date == time.Date).ToList());
        }


        public IActionResult DeleteAppointment(int appointmentId)
        {
            try
            {
                Appointment appointment = db.Find<Appointment>(appointmentId);

                db.Remove(appointment);
                db.SaveChanges();

                TempData[success] = "قرار ملاقات با موفقیت حذف شد.";
            }
            catch
            {
                TempData[error] = "حذف قرار ملاقات با شکست مواجه شد.";
            }

            return RedirectToAction(nameof(AppointmentList));
        }
    }
}
