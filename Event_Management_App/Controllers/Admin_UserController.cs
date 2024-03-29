﻿using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class Admin_UserController : Controller
    {
        readonly IAdmin_UserBAL _IAdmin_UserBAL;

        public Admin_UserController(IAdmin_UserBAL admin_userBAL)
        {
            _IAdmin_UserBAL = admin_userBAL;
        }

        public IActionResult Admin_User()
        {
            return View();
        }

        public IActionResult GetAdmin_User()
        {
            return Json(_IAdmin_UserBAL.GetAdmin_UserList());
        }

        public IActionResult AddAdmin_User()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAdmin_UserPost(string model, IFormFile idproof, IFormFile profile)
        {
            Admin_UserModel admin_user = JsonSerializer.Deserialize<Admin_UserModel>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            admin_user.CreatedBy = testid.Value;

            admin_user.CreatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                var result = _IAdmin_UserBAL.AddAdmin_User(admin_user, idproof, profile);

                if (result == "Exist")
                {
                    return Json(new { status = "warning", message = "Email Id Already Exists!" });
                }

            }

            return Json(new { status = "success", message = "User add successfully!" });
        }

        [HttpGet]
        public IActionResult PopulateAdmin_User(int ID)
        {
            return Json(_IAdmin_UserBAL.PopulateAdmin_UserData(ID));
        }

        [HttpPost]
        public IActionResult UpdateAdmin_User(string model, int ID,  int Role, IFormFile idproof, IFormFile profile)
        {
            Admin_UserModel admin_user = JsonSerializer.Deserialize<Admin_UserModel>(model)!;

            admin_user.Role = Role;

            int? testid = HttpContext.Session.GetInt32("Id");

            admin_user.UpdatedBy = testid.Value;

            admin_user.UpdatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {

                var result = _IAdmin_UserBAL.UpdateAdmin_UserData(admin_user, ID, idproof, profile);

                if (result == "Exist")
                {
                    return Json(new { status = "warning", message = "Email Id Already Exists!" });
                }

            }

            return Json(new { status = "success", message = "User Update successfully!" });
        }

        public IActionResult DeleteAdmin_User(int ID)
        {
			Admin_UserModel oModel = new Admin_UserModel();

			int? testid = HttpContext.Session.GetInt32("Id");

			oModel.DeletedBy = testid.Value;

			oModel.DeletedAt = DateTime.Now;

			_IAdmin_UserBAL.DeleteAdmin_UserData(oModel, ID);

            return Json("Admin_User");
        }
    }
}
