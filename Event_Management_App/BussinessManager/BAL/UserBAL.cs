using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class UserBAL : IUserBAL
    {
        IUserDAL _IEventDAL;

        public UserBAL(IDBManager dBManager)
        {
            _IEventDAL = new UserDAL(dBManager);
        }

        public List<SignUpModel> GetUserList()
        {
            return _IEventDAL.GetUserList();
        }

        public string SignUp(SignUpModel sign)
        {
            // sign.SPassword = PasswordHash.ComputeMD5(sign.SPassword);

            bool emailExist = _IEventDAL.CheckEmailExist(sign.Email, sign.Id);

            if (emailExist)
            {
                return "Exist";
            }

            _IEventDAL.AddUser(sign);
            return "Success";

        }

       
    }
}

