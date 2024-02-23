using Event_Management_App.BussinessManager.IBAL;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class CustomerProfileController : Controller
    {
        readonly ICustomerProfileBAL _ICustomerProfileBAL;

        public CustomerProfileController(ICustomerProfileBAL customerprofileBAL)
        {
            _ICustomerProfileBAL = customerprofileBAL;
        }

        public IActionResult CustomerProfile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PopulateProfile()
        {
            int? testid = HttpContext.Session.GetInt32("Id");

            int ID = testid.Value;

            return Json(_ICustomerProfileBAL.PopulateProfileData(ID));
        }
    }
}
