using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class AdminController : Controller
    {

        readonly IAdminBAL _IAdminBAL;

        public AdminController(IAdminBAL adminBAL)
        {
            _IAdminBAL = adminBAL;
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult GetAdmin()
        {
            return Json(_IAdminBAL.GetAdminList());
        }

        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAdminPost(string model, IFormFile idproof, IFormFile profile)
        {
            Admin_UserModel admin = JsonSerializer.Deserialize<Admin_UserModel>(model)!;

            if (ModelState.IsValid)
            {
                var result = _IAdminBAL.AddAdmin(admin, idproof, profile);

                if (result == "Exist")
                {
                    return Json(new { status = "warning", message = "Email Id Already Exists!" });
                }

            }

            return Json(new { status = "success", message = "User add successfully!" });
        }

        [HttpGet]
        public IActionResult PopulateAdmin(int ID)
        {
            return Json(_IAdminBAL.PopulateAdminData(ID));
        }

        [HttpPost]
        public IActionResult UpdateAdmin(string model, int ID, IFormFile idproof, IFormFile profile)
        {
            Admin_UserModel admin = JsonSerializer.Deserialize<Admin_UserModel>(model)!;

            if (ModelState.IsValid)
            {
                var result = _IAdminBAL.UpdateAdminData(admin, ID, idproof, profile);

                if (result == "Exist")
                {
                    return Json(new { status = "warning", message = "Email Id Already Exists!" });
                }

            }

            return Json(new { status = "success", message = "User Update successfully!" });
        }

        public IActionResult DeleteAdmin(int ID)
        {
            _IAdminBAL.DeleteAdminData(ID);

            return Json("Admin");
        }
    }
}
