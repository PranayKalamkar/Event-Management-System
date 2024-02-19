using Event_Management_App.BussinessManager.BAL;
using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class RequestedEventsController : Controller
    {
        IRequestedEventsBAL _IRequestedEventsBAL;

        IEmailSenderBAL _IEmailSenderBAL;

        public RequestedEventsController(IRequestedEventsBAL bookedeventsBAL, IEmailSenderBAL emailSenderBAL)
        {
            _IRequestedEventsBAL = bookedeventsBAL;
            _IEmailSenderBAL = emailSenderBAL;
        }

        public IActionResult RequestedEvents()
        {
            return View();
        }

        public IActionResult RequestedEventsList()
        {
            return Json(_IRequestedEventsBAL.GetRequestedEvents());
        }

        public IActionResult PopulateEvent(int Id)
        {
            return Json(_IRequestedEventsBAL.PopulateEventData(Id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(int Status_Id, int Id, string Email)
        {
            //GetAllBookedDetails bookevent = JsonSerializer.Deserialize<GetAllBookedDetails>(model)!;

            GetAllBookedDetails bookevent = new GetAllBookedDetails();

            //await _IEmailSenderBAL.EmailSendAsync(Email, "Booking Confirm", "Congratulation Your Booking Is confirmed!");

            if(Status_Id == 1)
            {
				await _IEmailSenderBAL.EmailSendAsync(Email, "Cancelled", "Sorry for Incovinience!");

			}
            else if(Status_Id == 2)
            {
				await _IEmailSenderBAL.EmailSendAsync(Email, "Booking Confirm", "Congratulation Your Booking Is confirmed!");
			}
			else if(Status_Id == 3)
			{
				await _IEmailSenderBAL.EmailSendAsync(Email, "Pending", "Your Order is on Hold!");
			}

			bookevent.SignUpModel = new SignUpModel();

            string? adminEmail = HttpContext.Session.GetString("Email");

            bookevent.SignUpModel.Email = adminEmail;

			if (Status_Id == 1)
			{
				await _IEmailSenderBAL.EmailSendAsync(adminEmail, "Not Available", "Place is Already Booked!");
			}
			else if (Status_Id == 2)
			{
				await _IEmailSenderBAL.EmailSendAsync(adminEmail, "New Order", "Order Booked Successfully!");
			}
			else if (Status_Id == 3)
			{
				await _IEmailSenderBAL.EmailSendAsync(adminEmail, "Pending", "Order is on Hold!");
			}

			//await _IEmailSenderBAL.EmailSendAsync(adminEmail, "New Order", "Order Booked Successfully!");


            //int? status = Status_Id;

            //bookevent.BookedEventsModel = new BookedEventsModel();

            //bookevent.RequestedEventsModel.Status_Id = Status_Id;

            _IRequestedEventsBAL.UpdateEventData(Status_Id, Id);

            return Json("BookedEventsList");
        }

        public IActionResult GetStatus()
        {
            List<GetAllBookedDetails> status = _IRequestedEventsBAL.GetStatus();

            return Json(status);
        }
    }
}
