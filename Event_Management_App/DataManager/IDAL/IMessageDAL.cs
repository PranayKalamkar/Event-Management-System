using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IMessageDAL
    {
        public List<GetAllBookedDetails> GetMessageList();

        public GetAllBookedDetails PopulateMessageData(int ID);
    }
}
