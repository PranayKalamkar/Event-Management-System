using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IBookedEventsBAL
    {
        public List<GetAllBookedDetails> GetBookedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);

        public GetAllBookedDetails UpdateEventData(GetAllBookedDetails bookevent,int Id);
    }
}
