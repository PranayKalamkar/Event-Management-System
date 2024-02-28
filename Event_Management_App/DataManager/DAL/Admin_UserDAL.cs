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

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return userList;
        }

        public Admin_UserModel AddAdmin_User(Admin_UserModel sign)
        {

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


                _dBManager.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return sign;
        }

        public Admin_UserModel GetDBImagesbyID(int ID)
        {
            Admin_UserModel adminusermodel = null;

            try
            {
                _dBManager.InitDbCommand("GetAdmin_UserImages", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@u_ID", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    adminusermodel = new Admin_UserModel();

                    adminusermodel.IdProofPath = item["Idproof"].ConvertJSONNullToString();
                    adminusermodel.ProfilePath = item["profile"].ConvertJSONNullToString();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return adminusermodel;
        }

        public bool CheckEmailExist(string emailId, int Id)
        {
            bool emailExist = false;

            try
            {
                _dBManager.InitDbCommand("CheckEmailExist", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_EmailId", emailId);
                _dBManager.AddCMDParam("@p_Id", Id);

                var result = _dBManager.ExecuteScalar();

                emailExist = Convert.ToBoolean(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return emailExist;
        }

        public Admin_UserModel PopulateAdmin_UserData(int ID)
        {
            Admin_UserModel adminusermodel = null;

            try
            {
                _dBManager.InitDbCommand("PopulateAdmin_UserbyId", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_id", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    adminusermodel = new Admin_UserModel();

                    adminusermodel.Id = item["Id"].ConvertDBNullToInt();
                    adminusermodel.Username = item["Username"].ConvertDBNullToString();
                    adminusermodel.Email = item["Email"].ConvertDBNullToString();
                    adminusermodel.Contact = item["Contact"].ConvertDBNullToString();
                    adminusermodel.Address = item["Address"].ConvertDBNullToString();
                    adminusermodel.IdProofPath = item["Idproof"].ConvertDBNullToString();
                    adminusermodel.ProfilePath = item["profile"].ConvertDBNullToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return adminusermodel;
        }

        public Admin_UserModel UpdateAdmin_UserData(Admin_UserModel adminusermodel, int ID)
        {
            try
            {
                _dBManager.InitDbCommand("Updateadmin_userById", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("u_Id", ID);
                _dBManager.AddCMDParam("u_Username", adminusermodel.Username);
                _dBManager.AddCMDParam("u_Email", adminusermodel.Email);
                _dBManager.AddCMDParam("u_Contact", adminusermodel.Contact);
                _dBManager.AddCMDParam("u_Address", adminusermodel.Address);
                _dBManager.AddCMDParam("u_IdProof", adminusermodel.IdProofPath);
                _dBManager.AddCMDParam("u_Profile", adminusermodel.ProfilePath);

                _dBManager.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return adminusermodel;
        }

        public void DeleteAdmin_UserData(int ID)
        {
            try
            {
                _dBManager.InitDbCommand("Deleteadmin_userById", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@deleteId", ID);

                _dBManager.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
