using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class TeamController : Controller
    {

        readonly ITeamBAL _ITeamBAL;

        public TeamController(ITeamBAL teamBAL)
        {
            _ITeamBAL = teamBAL;
        }

        public IActionResult Team()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PopulateTeam()
        {
            int? testid = HttpContext.Session.GetInt32("Id");

            int ID = testid.Value;

            return Json(_ITeamBAL.PopulateTeamData(ID));
        }

        [HttpPost]
        public IActionResult UpdateProfile(string model, int ID, IFormFile profile)
        {
            Admin_UserModel admin_user = JsonSerializer.Deserialize<Admin_UserModel>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            int Id = testid.Value;

            admin_user.UpdatedBy = Id;

            admin_user.UpdatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                var result = _ITeamBAL.UpdateProfileData(admin_user, ID, profile);

                if (result == "Exist")
                {
                    return Json(new { status = "warning", message = "Email Id Already Exists!" });
                }

            }

            return Json(new { status = "success", message = "User Update successfully!" });
        }
    }
}
