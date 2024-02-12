using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IBookedEventsDAL
    {
        public List<GetAllBookedDetails> AllBookedEvents();

        public string GetDBImagebyID(int ID);

        public GetAllBookedDetails PopulateBookedEventData(int ID);
    }
}
