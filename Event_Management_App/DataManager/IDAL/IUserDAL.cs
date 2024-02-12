using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IUserDAL
    {
        public List<SignUpModel> GetUserList();
        public SignUpModel AddUser(SignUpModel sign);
        public bool CheckEmailExist(string emailId, int ID);
    }
}
