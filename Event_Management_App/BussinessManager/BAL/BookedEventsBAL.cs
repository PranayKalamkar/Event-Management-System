﻿using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class BookedEventsBAL : IBookedEventsBAL
    {
        IBookedEventsDAL _IBookedEventsDAL;

        public BookedEventsBAL(IDBManager dBManager)
        {
            _IBookedEventsDAL = new BookedEventsDAL(dBManager);
        }

        public List<GetAllBookedDetails> GetAllBookedEvents()
        {
            return _IBookedEventsDAL.AllBookedEvents();
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            return _IBookedEventsDAL.PopulateBookedEventData(ID);
        }

        public int UpdateEventData(GetAllBookedDetails oData, int Status_Id, int Id)
        {

            int balance = 0;

            oData.RequestedEventsModel.Deposit = oData.AddEventModel.Amount;

            oData.RequestedEventsModel.Balance = balance.ToString();


			return _IBookedEventsDAL.UpdateBookedEventData(oData, Status_Id, Id);
        }

        public List<GetAllBookedDetails> GetStatus()
        {
            return _IBookedEventsDAL.GetStatus();
        }
    }
}
