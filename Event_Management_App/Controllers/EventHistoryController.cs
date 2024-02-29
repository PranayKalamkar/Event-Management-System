using Event_Management_App.BussinessManager.IBAL;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class EventHistoryController : Controller
    {
        IEventHistoryBAL _IEventHistoryBAL;

        public EventHistoryController(IEventHistoryBAL eventhistoryBAL)
        {
            _IEventHistoryBAL = eventhistoryBAL;
        }

        public IActionResult EventHistory()
        {
            return View();
        }

        public IActionResult EventHistoryList()
        {
            return Json(_IEventHistoryBAL.GetAllCompletedEvents());
        }

        [HttpGet]
        public IActionResult PopulateCompletedEvent(int ID)
        {
            return Json(_IEventHistoryBAL.PopulateCompletedEventData(ID));
        }
    }
}
