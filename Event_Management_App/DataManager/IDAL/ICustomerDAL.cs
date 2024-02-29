using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface ICustomerDAL
    {
        public MessageModel AddMessage(MessageModel oModel);
    }
}
