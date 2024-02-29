using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IMessageBAL
    {
        public List<GetAllBookedDetails> GetMessageList();

        public GetAllBookedDetails PopulateMessageData(int ID);
    }
}
