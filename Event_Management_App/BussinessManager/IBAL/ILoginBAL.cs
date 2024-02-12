using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface ILoginBAL
    {
        public LoginModel LoginUser(string email, string pass, int Id);
    }
}
