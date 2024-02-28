using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class TeamDAL : ITeamDAL
    {
        readonly IDBManager _dBManager;

        public TeamDAL(IDBManager dbManager)
        {
            _dBManager = dbManager;
        }

        public Admin_UserModel PopulateTeamData(int ID)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return emailExist;
        }

        public Admin_UserModel UpdateTeamData(Admin_UserModel adminusermodel, int ID)
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
    }
}
