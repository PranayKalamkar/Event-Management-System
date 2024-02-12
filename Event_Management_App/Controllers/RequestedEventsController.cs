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

        public RequestedEventsController(IRequestedEventsBAL bookedeventsBAL)
        {
            _IRequestedEventsBAL = bookedeventsBAL;
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
        public IActionResult UpdateEvent(int Status_Id, int Id)
        {
            //GetAllBookedDetails bookevent = JsonSerializer.Deserialize<GetAllBookedDetails>(model)!;

            GetAllBookedDetails bookevent = new GetAllBookedDetails();

            bookevent.RequestedEventsModel = new RequestedEventsModel();

            //int? status = Status_Id;

            //bookevent.BookedEventsModel = new BookedEventsModel();

            bookevent.RequestedEventsModel.Status_Id = Status_Id;

            _IRequestedEventsBAL.UpdateEventData(bookevent, Id);

            return Json("BookedEventsList");
        }

        public IActionResult GetStatus()
        {
            List<GetAllBookedDetails> status = _IRequestedEventsBAL.GetStatus();

            return Json(status);
        }
    }
}
