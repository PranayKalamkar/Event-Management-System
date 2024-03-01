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
            string getPassword = "";

            try
            {
                pass = pass + _dBManager.getSalt();

                _dBManager.InitDbCommand("GetPassword", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@user_pass", pass);

                var result = _dBManager.ExecuteScalar();

                getPassword = Convert.ToString(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return getPassword;
        }

        public int GetRole(string email)
        {
            int role = 0;

            try
            {
                _dBManager.InitDbCommand("GetRole", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@IEmail", email);

                var result = _dBManager.ExecuteScalar();

                role = Convert.ToInt32(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return role;
        }

        public int GetId(string email)
        {
            int id = 0;

            try
            {
                _dBManager.InitDbCommand("GetId", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@IEmail", email);

                var result = _dBManager.ExecuteScalar();

                id = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return id;
        }

        public string LoginUser(string email)
        {
            string existingPassword = "";

            try
            {
                _dBManager.InitDbCommand("GetUserPassword", CommandType.StoredProcedure);


                _dBManager.AddCMDParam("@IEmail", email);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    existingPassword = item["SPassword"].ConvertDBNullToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return existingPassword;
        }

    }
}
