using Event_Management_App.BussinessManager.IBAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class AdminDashboardController : Controller
    {

        private readonly ILogger<AdminDashboardController> _logger;

        readonly IAdminDashboardBAL _IAdminDashboardBAL;


		public AdminDashboardController(ILogger<AdminDashboardController> logger, IAdminDashboardBAL dashboard)
        {
            _logger = logger;
            _IAdminDashboardBAL = dashboard;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            Console.WriteLine(HttpContext.Session.GetInt32("Id"));

            Console.WriteLine(HttpContext.Session.GetString("Email"));

            return View();
        }

        [HttpGet]
        public IActionResult Populate()
        {
			return Json(_IAdminDashboardBAL.Populate());
		}
    }
}
