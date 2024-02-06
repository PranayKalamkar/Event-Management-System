using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IBookedEventsDAL
    {
        public List<GetAllBookedDetails> GetBookedEvents();

        public string GetDBImagebyID(int ID);

        public GetAllBookedDetails PopulateEventData(int ID);
    }
}
