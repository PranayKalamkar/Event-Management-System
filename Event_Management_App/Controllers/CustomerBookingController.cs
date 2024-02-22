using Event_Management_App.BussinessManager.BAL;
using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        [HttpPost]
        public IActionResult Booked(string model, int ID, int AddEvent_Id)
        {
            GetAllBookedDetails oData = JsonSerializer.Deserialize<GetAllBookedDetails>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            oData.RequestedEventsModel.Signup_id = testid.Value;

            oData.AddEventModel.Id = AddEvent_Id;

            oData.RequestedEventsModel.Addevent_id = ID;

            if(ModelState.IsValid)
            {
                var result = _ICustomerBookingBAL.AddbookEventData(oData);

                if (result == "Exist")
                {
                    return Json(new { status = "warning", message = "Date is not Avaliable!" });
                }
            }

            //bookmodel.BookedEventsModel.Signup_id = id.ConvertDBNullToInt();

            //_ICustomerBookingBAL.AddbookEventData(oData);

            _IEmailSenderBAL.EmailSendAsync("pranaykalamkar07@gmail.com", "New Order", "Order Booked Successfully!");

			return Json(new { status = "success", message = "User Booked successfully!" });
        }
    }
}
