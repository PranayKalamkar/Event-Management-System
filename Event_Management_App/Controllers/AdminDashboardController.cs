﻿using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}