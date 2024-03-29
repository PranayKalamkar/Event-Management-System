﻿using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class RequestedEventsBAL : IRequestedEventsBAL
    {
        IRequestedEventsDAL _IBookedEventDAL;

        public RequestedEventsBAL(IDBManager dBManager)
        {
            _IBookedEventDAL = new RequestedEventsDAL(dBManager);
        }

        public List<GetAllBookedDetails> GetRequestedEvents()
        {
            return _IBookedEventDAL.GetRequestedEvents();
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            return _IBookedEventDAL.PopulateRequestedEventData(ID);
        }

        public GetAllBookedDetails UpdateEventData(GetAllBookedDetails bookevent, int Id)
        {
            return _IBookedEventDAL.UpdateRequestedEventData(bookevent, Id);
        }

        public List<GetAllBookedDetails> GetStatus()
        {
            return _IBookedEventDAL.GetStatus();
        }
    }
}
