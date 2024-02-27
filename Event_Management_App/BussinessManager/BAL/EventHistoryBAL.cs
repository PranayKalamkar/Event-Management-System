using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class EventHistoryBAL : IEventHistoryBAL
    {
        IEventHistoryDAL _IEventHistoryDAL;

        public EventHistoryBAL(IDBManager dBManager)
        {
            _IEventHistoryDAL = new EventHistoryDAL(dBManager);
        }

        public List<GetAllBookedDetails> GetAllCompletedEvents()
        {
            return _IEventHistoryDAL.AllCompletedEvents();
        }

        public GetAllBookedDetails PopulateCompletedEventData(int ID)
        {
            return _IEventHistoryDAL.PopulateCompletedventData(ID);
        }
    }
}
