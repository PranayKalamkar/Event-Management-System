using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IAdminBAL
    {
        public List<Admin_UserModel> GetAdminList();
        public string AddAdmin(Admin_UserModel sign, IFormFile idproof, IFormFile profile);
        public Admin_UserModel PopulateAdminData(int ID);
        public string UpdateAdminData(Admin_UserModel adminmodel, int ID, IFormFile idproof, IFormFile profile);
        public void DeleteAdminData(int ID);
    }
}
