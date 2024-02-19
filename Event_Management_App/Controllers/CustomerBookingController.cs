using Event_Management_App.BussinessManager.BAL;
using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.CommonCode;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class CustomerBookingController : Controller
    {
        ICustomerBookingBAL _ICustomerBookingBAL;

		IEmailSenderBAL _IEmailSenderBAL;

		public CustomerBookingController(ICustomerBookingBAL customerbookBAL, IEmailSenderBAL emailSenderBAL)
        {
            _ICustomerBookingBAL = customerbookBAL;
			_IEmailSenderBAL = emailSenderBAL;
		}
        public IActionResult CustomerBooking()
        {
            return View();
        }

        public IActionResult CustomerListEvent()
        {
            return Json(_ICustomerBookingBAL.GetBookedEvents());
        }

        public IActionResult Populate(int ID)
        {
            return Json(_ICustomerBookingBAL.PopulateEventData(ID));
        }

        public IActionResult Booked([FromBody] GetAllBookedDetails oData)
        {
            int? testid = HttpContext.Session.GetInt32("Id");

            oData.RequestedEventsModel.Signup_id = testid.Value;

            //bookmodel.BookedEventsModel.Signup_id = id.ConvertDBNullToInt();

            _ICustomerBookingBAL.AddbookEventData(oData);

            _IEmailSenderBAL.EmailSendAsync("pranaykalamkar07@gmail.com", "New Order", "Order Booked Successfully!");

			return Json("CustomerListEvent");
        }
    }
}
