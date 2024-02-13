using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class AdminDashboardController : Controller
    {

        private readonly ILogger<AdminDashboardController> _logger;

        public AdminDashboardController(ILogger<AdminDashboardController> logger)
        {
            _logger = logger;
        }

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
