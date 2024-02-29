using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class CustomerBAL : ICustomerBAL
    {
        ICustomerDAL _ICustomerDAL;

        public CustomerBAL(IDBManager dBManager)
        {
            _ICustomerDAL = new CustomerDAL(dBManager);
        }

        public MessageModel AddMessage(MessageModel oModel)
        {
            return _ICustomerDAL.AddMessage(oModel);
        }
    }
}
