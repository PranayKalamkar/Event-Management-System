using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IEventHistoryDAL
    {
        public List<GetAllBookedDetails> AllCompletedEvents();

        public GetAllBookedDetails PopulateCompletedventData(int ID);
    }
}
