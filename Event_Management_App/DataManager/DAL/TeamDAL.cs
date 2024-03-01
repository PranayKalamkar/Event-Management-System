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
                _dBManager.AddCMDParam("u_updateby", adminusermodel.UpdatedBy);
                _dBManager.AddCMDParam("u_updatedat", adminusermodel.UpdatedAt);

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
