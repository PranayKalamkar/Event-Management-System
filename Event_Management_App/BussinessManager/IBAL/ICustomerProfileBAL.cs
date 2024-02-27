using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface ICustomerProfileBAL
    {
        public Admin_UserModel PopulateProfileData(int ID);

        public string UpdateProfileData(Admin_UserModel adminusermodel, int ID, IFormFile profile);

        public string UploadProfile(IFormFile imageFile);
    }
}
