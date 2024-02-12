using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IRequestedEventsDAL
    {
        public List<GetAllBookedDetails> GetRequestedEvents();

        public string GetDBImagebyID(int ID);

        public GetAllBookedDetails PopulateRequestedEventData(int ID);

        public GetAllBookedDetails UpdateRequestedEventData(GetAllBookedDetails bookevent, int Id);

        public List<GetAllBookedDetails> GetStatus();
    }
}
