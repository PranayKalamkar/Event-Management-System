using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Extension;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class UserController : Controller
    {
        readonly IUserBAL _IUserBAL;

        public UserController(IUserBAL userBAL)
        {
            _IUserBAL = userBAL;
        }

        public IActionResult GetUser()
        {
            return Json(_IUserBAL.GetUserList());
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUpPost(string model)
        {
            SignUpModel sign = JsonSerializer.Deserialize<SignUpModel>(model)!;

           if(ModelState.IsValid)
            {
                var result = _IUserBAL.SignUp(sign);

                if(result == "Exist")
                {
                    return Json(new { status = "warning", message = "Email Id Already Exists!" });
                }
            }
            return Json(new { status = "success", message = "User register successfully!" });
        }

    }
}

