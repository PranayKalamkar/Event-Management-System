using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class LoginController : Controller
    {
        readonly ILoginBAL _ILoginBAL;

        public LoginController(ILoginBAL loginBAL)
        {
            _ILoginBAL = loginBAL;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPost(string Email, string SPassword, int Id)
        {
            LoginModel login = new LoginModel();

            if (ModelState.IsValid)
            {
                login = _ILoginBAL.LoginUser(Email, SPassword, Id);

                if (!login.EmailExist)
                {
                    return Json(new { status = "warning", message = "Email does Not Exist!" });
                }
                else if (login.GetPassword != login.ExistingPassword)
                {
                    return Json(new { status = "warning", message = "Invalid Password!" });
                }
            }

            HttpContext.Session.SetInt32("Id", login.GetId);

            HttpContext.Session.SetString("Email", Email);

            HttpContext.Session.SetString("Password", SPassword);

            return Json(new { role = login.GetRole, status = "success", message = "Login successfully!" });
        }
    }
}
