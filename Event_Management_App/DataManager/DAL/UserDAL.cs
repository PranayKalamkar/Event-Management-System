using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class UserDAL : IUserDAL
    {
        //readonly string salt = "Strange User";

        readonly IDBManager _dBManager;

        public UserDAL(IDBManager dbManager)
        {
            _dBManager = dbManager;
        }

        public List<SignUpModel> GetUserList()
        {
            List<SignUpModel> userList = new List<SignUpModel>();

            //_dBManager.InitDbCommandText("select * from SignUp;");
            _dBManager.InitDbCommand("GetAllUser",CommandType.StoredProcedure);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach(DataRow item in ds.Tables[0].Rows)
            {
                SignUpModel model = new SignUpModel();

                model.Id = item["Id"].ConvertDBNullToInt();
                model.Username = item["Username"].ConvertDBNullToString();
                model.Email = item["Email"].ConvertDBNullToString();
                model.SPassword = item["SPassword"].ConvertDBNullToString();
                model.Role = item["RoleId"].ConvertDBNullToInt();

                userList.Add(model);

            }

            return userList;

        }

        public SignUpModel AddUser(SignUpModel sign)
        {
            sign.SPassword = sign.SPassword + _dBManager.getSalt();

            //_dBManager.InitDbCommandText("Insert into SignUp(Username,Email,SPassword) values (@Username,@Email,@SPassword);");
            _dBManager.InitDbCommand("InsertUser", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@Username", sign.Username);
            _dBManager.AddCMDParam("@Email", sign.Email);
            _dBManager.AddCMDParam("@SPassword", sign.SPassword);
            _dBManager.AddCMDParam("@u_RoleId", sign.Role);


            _dBManager.ExecuteNonQuery();

            return sign;
        }

        

        public bool CheckEmailExist(string emailId, int ID)
        {
            _dBManager.InitDbCommand("CheckEmailExist", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@p_EmailId", emailId);
            _dBManager.AddCMDParam("@p_Id", ID);

            var result = _dBManager.ExecuteScalar();

            bool emailExist = Convert.ToBoolean(result);

            return emailExist;
        }
    }
}
