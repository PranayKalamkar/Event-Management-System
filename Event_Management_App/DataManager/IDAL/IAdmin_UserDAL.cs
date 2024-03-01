using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IAdmin_UserDAL
    {
        public List<Admin_UserModel> GetAdmin_UserList();

        public Admin_UserModel AddAdmin_User(Admin_UserModel sign);

        public Admin_UserModel PopulateAdmin_UserData(int ID);

        public Admin_UserModel UpdateAdmin_UserData(Admin_UserModel adminusermodel, int ID);

        public void DeleteAdmin_UserData(Admin_UserModel oModel, int ID);
    }
}
