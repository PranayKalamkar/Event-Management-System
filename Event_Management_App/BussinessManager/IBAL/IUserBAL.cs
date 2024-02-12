using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IUserBAL
    {
        public List<SignUpModel> GetUserList();
        public string SignUp(SignUpModel user);

        //public LoginModel LoginUser(string email, string pass, int Id);

        // public bool CheckEmailExist(string emailId);
    }
}
