using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IAdmin_CustomerBookingBAL
    {
        public List<GetAllBookedDetails> GetBookedEvents();

        public GetAllBookedDetails PopulateEventData(int ID);

        public string AddbookEventData(GetAllBookedDetails oData, int ID, int Signup_Id);

        public List<GetAllBookedDetails> GetAdmin_UserList();
    }
}
