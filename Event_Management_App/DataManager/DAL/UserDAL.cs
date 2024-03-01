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

            try
            {
                _dBManager.InitDbCommand("GetAllUser", CommandType.StoredProcedure);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    SignUpModel model = new SignUpModel();

                    model.Id = item["Id"].ConvertDBNullToInt();
                    model.Username = item["Username"].ConvertDBNullToString();
                    model.Email = item["Email"].ConvertDBNullToString();
                    model.SPassword = item["SPassword"].ConvertDBNullToString();
                    model.Role = item["RoleId"].ConvertDBNullToInt();

                    userList.Add(model);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return userList;

        }

        public SignUpModel AddUser(SignUpModel sign)
        {
            int isDelete = 0;

            try
            {
                sign.SPassword = sign.SPassword + _dBManager.getSalt();

                _dBManager.InitDbCommand("InsertUser", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@UserName_in", sign.Username);
                _dBManager.AddCMDParam("@Email_in", sign.Email);
                _dBManager.AddCMDParam("@SPassword_in", sign.SPassword);
                _dBManager.AddCMDParam("@RoleId_in", sign.Role);
                _dBManager.AddCMDParam("@createdat_in", sign.CreatedAt);
                _dBManager.AddCMDParam("@deletevalue_in", isDelete);


                _dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return sign;
        }

    }
}
