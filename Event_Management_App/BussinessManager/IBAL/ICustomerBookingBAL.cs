using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface ICustomerBookingBAL
    {
        public List<GetAllBookedDetails> GetBookedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData);
    }
}
