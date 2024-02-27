using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface ITeamDAL
    {
        public Admin_UserModel PopulateTeamData(int ID);

        public Admin_UserModel GetDBImagesbyID(int ID);

        public bool CheckEmailExist(string emailId, int Id);

        public Admin_UserModel UpdateTeamData(Admin_UserModel adminusermodel, int ID);
    }
}
