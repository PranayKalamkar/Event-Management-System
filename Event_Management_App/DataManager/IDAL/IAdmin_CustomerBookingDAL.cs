using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IAdmin_CustomerBookingDAL
    {
        public List<GetAllBookedDetails> GetBookedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData, int ID, int Signup_Id);

        public List<GetAllBookedDetails> GetAdmin_UserList();
    }
}
