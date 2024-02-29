using Event_Management_App.BussinessManager.IBAL;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class MessageController : Controller
    {
        readonly IMessageBAL _IMessageBAL;

        public MessageController(IMessageBAL messageBAL)
        {
            _IMessageBAL = messageBAL;
        }

        public IActionResult Message()
        {
            return View();
        }

        public IActionResult GetMessage()
        {
            return Json(_IMessageBAL.GetMessageList());
        }

        [HttpGet]
        public IActionResult PopulateMessage(int ID)
        {
            return Json(_IMessageBAL.PopulateMessageData(ID));
        }
    }
}
