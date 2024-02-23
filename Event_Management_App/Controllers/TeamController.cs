using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
