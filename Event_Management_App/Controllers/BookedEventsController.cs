using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class BookedEventsController : Controller
    {
        IBookedEventsBAL _IBookedEventsBAL;

        public BookedEventsController(IBookedEventsBAL bookedeventsBAL)
        {
            _IBookedEventsBAL = bookedeventsBAL;
        }

        public IActionResult BookedEvents()
        {
            return View();
        }

        public IActionResult BookedEventsList()
        {
            return Json(_IBookedEventsBAL.GetAllBookedEvents());
        }

        public IActionResult PopulateBookEvent(int Id)
        {
            return Json(_IBookedEventsBAL.PopulateEventData(Id));
        }

        public IActionResult UpdateBookEvent(string model, int Status_Id, int Id)
        {
			GetAllBookedDetails oData = JsonSerializer.Deserialize<GetAllBookedDetails>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            int ID = testid.Value;

            oData.RequestedEventsModel.UpdatedBy = ID;

            oData.RequestedEventsModel.UpdatedAt = DateTime.Now;

            _IBookedEventsBAL.UpdateEventData(oData, Status_Id, Id);

            return Json("BookedEventsList");
        }

        public IActionResult GetStatus()
        {
            List<GetAllBookedDetails> status = _IBookedEventsBAL.GetStatus();

            return Json(status);
        }
    }
}
