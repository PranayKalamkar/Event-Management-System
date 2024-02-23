using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface ICustomerProfileDAL
    {
        public Admin_UserModel PopulateProfileData(int ID);
    }
}
