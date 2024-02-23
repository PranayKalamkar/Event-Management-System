using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class CustomerProfileBAL : ICustomerProfileBAL
    {
        ICustomerProfileDAL _ICustomerProfileDAL;

        public CustomerProfileBAL(IDBManager dBManager)
        {
            _ICustomerProfileDAL = new CustomerProfileDAL(dBManager);
        }

        public Admin_UserModel PopulateProfileData(int ID)
        {
            return _ICustomerProfileDAL.PopulateProfileData(ID);
        }
    }
}
