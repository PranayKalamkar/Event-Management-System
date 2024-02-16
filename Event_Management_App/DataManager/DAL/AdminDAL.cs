using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class AdminDAL : IAdminDAL
    {
        readonly IDBManager _dBManager;

        public AdminDAL(IDBManager dbManager)
        {
            _dBManager = dbManager;
        }

        public List<Admin_UserModel> GetAdminList()
        {
            List<Admin_UserModel> userList = new List<Admin_UserModel>();

            _dBManager.InitDbCommand("GetAllAdmin", CommandType.StoredProcedure);

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

        public Admin_UserModel AddAdmin(Admin_UserModel sign)
        {
            sign.SPassword = sign.SPassword + _dBManager.getSalt();

            _dBManager.InitDbCommand("InsertAdmin_User", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@a_UserName", sign.Username);
            _dBManager.AddCMDParam("@a_Email", sign.Email);
            _dBManager.AddCMDParam("@a_SPassword", sign.SPassword);
            _dBManager.AddCMDParam("@a_RoleId", sign.Role);
            _dBManager.AddCMDParam("@a_Contact", sign.Contact);
            _dBManager.AddCMDParam("@a_Address", sign.Address);
            _dBManager.AddCMDParam("@a_Idproof", sign.IdProofPath);
            _dBManager.AddCMDParam("@a_profile", sign.ProfilePath);


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

        public Admin_UserModel PopulateAdminData(int ID)
        {
            _dBManager.InitDbCommand("PopulateAdmin_UserbyId", CommandType.StoredProcedure);

            Admin_UserModel adminmodel = null;

            _dBManager.AddCMDParam("@p_id", ID);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                adminmodel = new Admin_UserModel();

                adminmodel.Id = item["Id"].ConvertDBNullToInt();
                adminmodel.Username = item["Username"].ConvertDBNullToString();
                adminmodel.Email = item["Email"].ConvertDBNullToString();
                adminmodel.Contact = item["Contact"].ConvertDBNullToString();
                adminmodel.Address = item["Address"].ConvertDBNullToString();
                adminmodel.IdProofPath = item["Idproof"].ConvertDBNullToString();
                adminmodel.ProfilePath = item["profile"].ConvertDBNullToString();
            }
            return adminmodel;
        }

        public Admin_UserModel UpdateAdminData(Admin_UserModel adminmodel, int ID)
        {
            _dBManager.InitDbCommand("Updateadmin_userById", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("u_Id", ID);
            _dBManager.AddCMDParam("Username", adminmodel.Username);
            _dBManager.AddCMDParam("Email", adminmodel.Email);
            _dBManager.AddCMDParam("u_Contact", adminmodel.Contact);
            _dBManager.AddCMDParam("u_Address", adminmodel.Address);
            _dBManager.AddCMDParam("u_IdProof", adminmodel.IdProofPath);
            _dBManager.AddCMDParam("u_Profile", adminmodel.ProfilePath);

            _dBManager.ExecuteNonQuery();

            return adminmodel;
        }

        public void DeleteAdminData(int ID)
        {
            _dBManager.InitDbCommand("DeleteadminById", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@deleteId", ID);

            _dBManager.ExecuteNonQuery();
        }
    }
}
