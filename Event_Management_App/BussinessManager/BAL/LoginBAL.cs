using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class LoginBAL : ILoginBAL
    {
        ILoginDAL _ILoginDAL;
        ICommonDAL _ICommonDAL;

        public LoginBAL(IDBManager dBManager)
        {
            _ILoginDAL = new LoginDAL(dBManager);
            _ICommonDAL = new CommonDAL(dBManager);
        }

        public LoginModel LoginUser(string email, string pass, int Id)
        {
            LoginModel login = new LoginModel();

            login.EmailExist = _ICommonDAL.CheckEmailExist(email, Id);

            login.GetPassword = _ILoginDAL.GetPassword(pass);

            login.ExistingPassword = _ILoginDAL.LoginUser(email);

            login.GetRole = _ILoginDAL.GetRole(email);

            login.GetId = _ILoginDAL.GetId(email);

            return login;

        }
    }
}
