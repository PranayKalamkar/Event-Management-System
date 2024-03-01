using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface ICustomerProfileDAL
    {
        public Admin_UserModel PopulateProfileData(int ID);

        public Admin_UserModel UpdateProfileData(Admin_UserModel adminusermodel, int ID);
    }
}
