using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IAdmin_UserBAL
    {
        public List<Admin_UserModel> GetAdmin_UserList();
        public string AddAdmin_User(Admin_UserModel sign, IFormFile idproof, IFormFile profile);
        public Admin_UserModel PopulateAdmin_UserData(int ID);
        public string UpdateAdmin_UserData(Admin_UserModel adminusermodel, int ID, IFormFile idproof, IFormFile profile);
        public void DeleteAdmin_UserData(int ID);
    }
}
