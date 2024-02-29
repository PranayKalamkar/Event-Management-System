using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class Admin_CustomerBookingController : Controller
    {
        IAdmin_CustomerBookingBAL _IAdmin_CustomerBookingBAL;

        public Admin_CustomerBookingController(IAdmin_CustomerBookingBAL customerbookBAL)
        {
            _IAdmin_CustomerBookingBAL = customerbookBAL;
        }
        public IActionResult Admin_CustomerBooking()
        {
            return View();
        }

        public IActionResult CustomerListEvent()
        {
            return Json(_IAdmin_CustomerBookingBAL.GetBookedEvents());
        }

        public IActionResult Populate(int ID)
        {
            return Json(_IAdmin_CustomerBookingBAL.PopulateEventData(ID));
        }

        [HttpPost]
        public IActionResult Booked(string model, int ID, int Signup_Id)
        {
            GetAllBookedDetails oData = JsonSerializer.Deserialize<GetAllBookedDetails>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            int Id = testid.Value;

            oData.RequestedEventsModel.CreatedBy = Id;

            oData.RequestedEventsModel.CreatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                var result = _IAdmin_CustomerBookingBAL.AddbookEventData(oData, ID, Signup_Id);

                if (result == "Exist")
                {
                    return Json(new { status = "warning", message = "Date is not Avaliable!" });
                }
            }


            return Json(new { status = "success", message = "User Booked successfully!" });

        }

        public IActionResult GetUserData()
        {
            List<GetAllBookedDetails> status = _IAdmin_CustomerBookingBAL.GetAdmin_UserList();

            return Json(status);
        }
    }
}
