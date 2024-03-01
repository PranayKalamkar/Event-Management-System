using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface ICustomerBookingDAL
    {
        public List<GetAllBookedDetails> GetBookedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData);
    }
}
