using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class TeamController : Controller
    {

        readonly IAdminBAL _IAdminBAL;

        public TeamController(IAdminBAL adminBAL)
        {
            _IAdminBAL = adminBAL;
        }

        public IActionResult Team()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PopulateAdmin()
        {
            int? testid = HttpContext.Session.GetInt32("Id");

            int ID = testid.Value;

            return Json(_IAdminBAL.PopulateAdminData(ID));
        }
    }
}
