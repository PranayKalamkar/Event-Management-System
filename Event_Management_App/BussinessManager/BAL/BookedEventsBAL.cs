﻿using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class BookedEventsBAL : IBookedEventsBAL
    {
        IBookedEventsDAL _IBookedEventDAL;

        public BookedEventsBAL(IDBManager dBManager)
        {
            _IBookedEventDAL = new BookedEventsDAL(dBManager);
        }

        public List<BookedEventsModel> GetBookedEvents()
        {
            return _IBookedEventDAL.GetBookedEvents();
        }
    }
}
