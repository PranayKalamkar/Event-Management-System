using Event_Management_App.BussinessManager.BAL;
using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.IDAL;
using Microsoft.AspNetCore.Mvc;

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
            return Json(_IBookedEventsBAL.GetBookedEvents());
        }

        public IActionResult PopulateEvent(int Id)
        {
            return Json(_IBookedEventsBAL.PopulateEventData(Id));
        }
    }
}
