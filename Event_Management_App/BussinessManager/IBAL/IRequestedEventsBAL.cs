using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IRequestedEventsBAL
    {
        public List<GetAllBookedDetails> GetRequestedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);

        //public GetAllBookedDetails UpdateEventData(GetAllBookedDetails bookevent,int Id);
        public int UpdateEventData(int Status_Id, int Id);

        public List<GetAllBookedDetails> GetStatus();
    }
}
