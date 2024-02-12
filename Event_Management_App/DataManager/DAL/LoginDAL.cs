using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class LoginDAL : ILoginDAL
    {
        readonly IDBManager _dBManager;

        public LoginDAL(IDBManager dbManager)
        {
            _dBManager = dbManager;
        }

        public string GetPassword(string pass)
        {
            pass = pass + _dBManager.getSalt();

            _dBManager.InitDbCommand("GetPassword", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@user_pass", pass);

            var result = _dBManager.ExecuteScalar();

            string getPassword = Convert.ToString(result);

            return getPassword;
        }

        public int GetRole(string email)
        {
            _dBManager.InitDbCommand("GetRole", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@IEmail", email);

            var result = _dBManager.ExecuteScalar();

            int role = Convert.ToInt32(result);

            return role;
        }

        public int GetId(string email)
        {
            _dBManager.InitDbCommand("GetId", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@IEmail", email);

            var result = _dBManager.ExecuteScalar();

            int id = Convert.ToInt32(result);

            return id;
        }

        public string LoginUser(string email)
        {
            string existingPassword = null;

            //_dBManager.InitDbCommandText("select SPassword from SignUp where Email = @Email;");
            _dBManager.InitDbCommand("GetUserPassword", CommandType.StoredProcedure);


            _dBManager.AddCMDParam("@IEmail", email);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                existingPassword = item["SPassword"].ConvertDBNullToString();
            }

            return existingPassword;
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
