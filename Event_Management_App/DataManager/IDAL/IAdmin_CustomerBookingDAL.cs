using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IAdmin_CustomerBookingDAL
    {
        public List<GetAllBookedDetails> GetBookedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);

        public bool CheckEmailExist(string emailId, int Id);

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData, int ID);
    }
}
