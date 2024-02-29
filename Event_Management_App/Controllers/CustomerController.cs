using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Event_Management_App.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerBAL _ICustomerBAL;

        public CustomerController(ICustomerBAL customerBAL)
        {
            _ICustomerBAL = customerBAL;
        }

        public IActionResult Customer()
        {
            Console.WriteLine(HttpContext.Session.GetInt32("Id"));

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string model)
        {
            MessageModel oModel = JsonSerializer.Deserialize<MessageModel>(model)!;

            int? testid = HttpContext.Session.GetInt32("Id");

            oModel.Signup_Id = testid.Value;

            oModel.CreatedBy = testid.Value;

            oModel.CreatedAt = DateTime.Now;

            _ICustomerBAL.AddMessage(oModel);

            return Json("Message Sent Successfully!");
        }
    }
}
