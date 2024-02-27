using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface ITeamBAL
    {
        public Admin_UserModel PopulateTeamData(int ID);

        public string UpdateProfileData(Admin_UserModel adminusermodel, int ID, IFormFile profile);

        public string UploadProfile(IFormFile imageFile);
    }
}
