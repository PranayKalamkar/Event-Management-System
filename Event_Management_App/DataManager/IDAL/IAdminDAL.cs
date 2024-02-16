using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IAdminDAL
    {
        public List<Admin_UserModel> GetAdminList();

        public Admin_UserModel AddAdmin(Admin_UserModel sign);

        public bool CheckEmailExist(string emailId, int Id);

        public Admin_UserModel PopulateAdminData(int ID);

        public Admin_UserModel UpdateAdminData(Admin_UserModel adminmodel, int ID);

        public void DeleteAdminData(int ID);
    }
}
