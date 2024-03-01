using Event_Management_App.BussinessManager.IBAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class AdminDashboardController : Controller
    {

        private readonly ILogger<AdminDashboardController> _logger;

        readonly IAdminDashboardBAL _IAdminDashboardBAL;

        readonly IBookedEventsBAL _IBookedEventsBAL;


		public AdminDashboardController(ILogger<AdminDashboardController> logger, IAdminDashboardBAL dashboard, IBookedEventsBAL bookedeventsBAL)
        {
            _logger = logger;
            _IAdminDashboardBAL = dashboard;
			_IBookedEventsBAL = bookedeventsBAL;

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

		public IActionResult BookedEventsList()
		{
			return Json(_IBookedEventsBAL.GetAllBookedEvents());
		}

		[HttpGet]
        public IActionResult Populate()
        {
			return Json(_IAdminDashboardBAL.Populate());
		}
    }
}
