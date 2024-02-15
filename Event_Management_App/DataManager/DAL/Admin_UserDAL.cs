using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class Admin_UserDAL : IAdmin_UserDAL
    {

        readonly IDBManager _dBManager;

        public Admin_UserDAL(IDBManager dbManager)
        {
            _dBManager = dbManager;
        }

        public List<Admin_UserModel> GetAdmin_UserList()
        {
            List<Admin_UserModel> userList = new List<Admin_UserModel>();

            _dBManager.InitDbCommand("GetAllAdmin_User", CommandType.StoredProcedure);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Admin_UserModel model = new Admin_UserModel();

                model.Id = item["Id"].ConvertDBNullToInt();
                model.Username = item["Username"].ConvertDBNullToString();
                model.Email = item["Email"].ConvertDBNullToString();

                userList.Add(model);

            }

            return userList;

        }

        public Admin_UserModel AddAdmin_User(Admin_UserModel sign)
        {
            sign.SPassword = sign.SPassword + _dBManager.getSalt();

            _dBManager.InitDbCommand("InsertAdmin_User", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@Username", sign.Username);
            _dBManager.AddCMDParam("@Email", sign.Email);
            _dBManager.AddCMDParam("@SPassword", sign.SPassword);
            _dBManager.AddCMDParam("@u_RoleId", sign.Role);


            _dBManager.ExecuteNonQuery();

            return sign;
        }

        public bool CheckEmailExist(string emailId, int Id)
        {
            _dBManager.InitDbCommand("CheckEmailExist", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@p_EmailId", emailId);
            _dBManager.AddCMDParam("@p_Id", Id);

            var result = _dBManager.ExecuteScalar();

            bool emailExist = Convert.ToBoolean(result);

            return emailExist;
        }

        public Admin_UserModel PopulateAdmin_UserData(int ID)
        {
            _dBManager.InitDbCommand("GetAdmin_UserbyId", CommandType.StoredProcedure);

            Admin_UserModel adminusermodel = null;

            _dBManager.AddCMDParam("@p_id", ID);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                adminusermodel = new Admin_UserModel();

                adminusermodel.Id = item["Id"].ConvertDBNullToInt();
                adminusermodel.Username = item["Username"].ConvertDBNullToString();
                adminusermodel.Email = item["Email"].ConvertDBNullToString();
            }
            return adminusermodel;
        }

        public Admin_UserModel UpdateAdmin_UserData(Admin_UserModel adminusermodel, int ID)
        {
            _dBManager.InitDbCommand("Updateadmin_userById", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("u_Id", ID);
            _dBManager.AddCMDParam("u_Username", adminusermodel.Username);
            _dBManager.AddCMDParam("u_Email", adminusermodel.Email);

            _dBManager.ExecuteNonQuery();

            return adminusermodel;
        }

        public void DeleteAdmin_UserData(int ID)
        {
            _dBManager.InitDbCommand("Deleteadmin_userById", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@deleteId", ID);

            _dBManager.ExecuteNonQuery();
        }

    }
}
