using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IBookedEventsBAL
    {
        public List<GetAllBookedDetails> GetAllBookedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);
    }
}
