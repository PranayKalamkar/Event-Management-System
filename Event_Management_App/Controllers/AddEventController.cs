using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class AddEventController : Controller
    {

        IAddEventBAL _IAddEventBAL;

        public AddEventController(IAddEventBAL addeventBAL)
        {
            _IAddEventBAL = addeventBAL;
        }

        public IActionResult AddEvent()
        {
            return View();
        }

        public IActionResult ListEvent()
        {
            return Json(_IAddEventBAL.AddEventList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string model, IFormFile file)
        {

            AddEventModel addevntmodel = JsonSerializer.Deserialize<AddEventModel>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            int ID = testid.Value;

            addevntmodel.CreatedBy = ID;

            addevntmodel.CreatedAt = DateTime.Now;

            _IAddEventBAL.AddEvent(addevntmodel, file);

            return Json("ListEvent");
        }

        public IActionResult Populate(int Id)
        {
            return Json(_IAddEventBAL.PopulateEventData(Id));
        }

        [HttpPost]
        public IActionResult Update(int ID, string model, IFormFile file)
        {
            AddEventModel addeventmodel = JsonSerializer.Deserialize<AddEventModel>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            int Id = testid.Value;

            addeventmodel.UpdatedBy = Id;

            addeventmodel.UpdatedAt = DateTime.Now;

            _IAddEventBAL.UpdateEventData(addeventmodel, ID, file);

            return Json("ListEvent");
        }

        public IActionResult Delete(int ID)
        {

            AddEventModel oModel = new AddEventModel();

			int? testid = HttpContext.Session.GetInt32("Id");

			int Id = testid.Value;

            oModel.DeletedBy = Id;

            oModel.DeletedAt = DateTime.Now;

			_IAddEventBAL.DeleteEventData(oModel, ID);

            return Json("ListEvent");
        }
    }
}
