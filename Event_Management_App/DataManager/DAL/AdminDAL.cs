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

            try
            {
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
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return userList;
        }

        public Admin_UserModel AddAdmin(Admin_UserModel sign)
        {
            int isDelete = 0;

            try
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
                _dBManager.AddCMDParam("@a_createdby", sign.CreatedBy);
                _dBManager.AddCMDParam("@a_createdat", sign.CreatedAt);
                _dBManager.AddCMDParam("@a_deletevalue", isDelete);


                _dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return sign;
        }

       

        public Admin_UserModel PopulateAdminData(int ID)
        {
            Admin_UserModel adminmodel = null;

            try
            {
                _dBManager.InitDbCommand("PopulateAdmin_UserbyId", CommandType.StoredProcedure);


                _dBManager.AddCMDParam("@p_id", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    adminmodel = new Admin_UserModel();

                    adminmodel.Id = item["Id"].ConvertDBNullToInt();
                    adminmodel.Username = item["Username"].ConvertDBNullToString();
                    adminmodel.Email = item["Email"].ConvertDBNullToString();
                    adminmodel.Role = item["RoleId"].ConvertDBNullToInt();
                    adminmodel.Contact = item["Contact"].ConvertDBNullToString();
                    adminmodel.Address = item["Address"].ConvertDBNullToString();
                    adminmodel.IdProofPath = item["Idproof"].ConvertDBNullToString();
                    adminmodel.ProfilePath = item["profile"].ConvertDBNullToString();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return adminmodel;
        }

        public Admin_UserModel UpdateAdminData(Admin_UserModel adminmodel, int ID)
        {
            try
            {
                _dBManager.InitDbCommand("Updateadmin_userById", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("u_Id", ID);
                _dBManager.AddCMDParam("u_Username", adminmodel.Username);
                _dBManager.AddCMDParam("u_Email", adminmodel.Email);
                _dBManager.AddCMDParam("u_Role", adminmodel.Role);
                _dBManager.AddCMDParam("u_Contact", adminmodel.Contact);
                _dBManager.AddCMDParam("u_Address", adminmodel.Address);
                _dBManager.AddCMDParam("u_IdProof", adminmodel.IdProofPath);
                _dBManager.AddCMDParam("u_Profile", adminmodel.ProfilePath);
                _dBManager.AddCMDParam("u_updateby", adminmodel.UpdatedBy);
                _dBManager.AddCMDParam("u_updatedat", adminmodel.UpdatedAt);

                _dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return adminmodel;
        }

        public void DeleteAdminData(Admin_UserModel oModel, int ID)
        {
            int isDelete = 1;

            try
            {
                _dBManager.InitDbCommand("Deleteadmin_userById", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@deleteId_in", ID);
                _dBManager.AddCMDParam("@deleteValue_in", isDelete);
                _dBManager.AddCMDParam("@deletedby_in", oModel.DeletedBy);
                _dBManager.AddCMDParam("@deletedat_in", oModel.DeletedAt);

				_dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
