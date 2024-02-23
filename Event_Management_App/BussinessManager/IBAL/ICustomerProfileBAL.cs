using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface ICustomerProfileBAL
    {
        public Admin_UserModel PopulateProfileData(int ID);
    }
}
