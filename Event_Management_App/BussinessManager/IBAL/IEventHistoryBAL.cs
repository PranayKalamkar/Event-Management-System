using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IEventHistoryBAL
    {
        public List<GetAllBookedDetails> GetAllCompletedEvents();

        public GetAllBookedDetails PopulateCompletedEventData(int ID);
    }
}
