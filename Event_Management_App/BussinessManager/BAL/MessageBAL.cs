using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class MessageBAL : IMessageBAL
    {
        IMessageDAL _IMessageDAL;

        public MessageBAL(IDBManager dBManager)
        {
            _IMessageDAL = new MessageDAL(dBManager);
        }

        public List<GetAllBookedDetails> GetMessageList()
        {
            return _IMessageDAL.GetMessageList();
        }

        public GetAllBookedDetails PopulateMessageData(int ID)
        {
            return _IMessageDAL.PopulateMessageData(ID);
        }
    }
}
