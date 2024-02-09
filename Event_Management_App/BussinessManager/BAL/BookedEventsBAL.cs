using Event_Management_App.BussinessManager.IBAL;
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

        public List<GetAllBookedDetails> GetBookedEvents()
        {
            return _IBookedEventDAL.GetBookedEvents();
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            return _IBookedEventDAL.PopulateEventData(ID);
        }

        public GetAllBookedDetails UpdateEventData(GetAllBookedDetails bookevent, int Id)
        {
            return _IBookedEventDAL.UpdateEventData(bookevent,Id);
        }

        public List<GetAllBookedDetails> GetStatus()
        {
            return _IBookedEventDAL.GetStatus();
        }
    }
}
