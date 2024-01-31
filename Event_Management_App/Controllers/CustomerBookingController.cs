﻿using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.CommonCode;
using Event_Management_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_App.Controllers
{
    public class CustomerBookingController : Controller
    {
        ICustomerBookingBAL _ICustomerBookingBAL;

        public CustomerBookingController(ICustomerBookingBAL customerbookBAL)
        {
            _ICustomerBookingBAL = customerbookBAL;
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

        public IActionResult Booked(GetAllBookedDetails bookmodel, BookedEventsModel oData)
        {
            bookmodel = new GetAllBookedDetails();

            oData = new BookedEventsModel();

            int? testid = HttpContext.Session.GetInt32("Id");

            oData.Signup_id = testid.Value;

            //bookmodel.BookedEventsModel.Signup_id = id.ConvertDBNullToInt();

            _ICustomerBookingBAL.AddbookEventData(bookmodel, oData);

            return Json("CustomerListEvent");
        }
    }
}
