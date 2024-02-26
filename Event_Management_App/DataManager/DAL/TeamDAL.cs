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


    }
}
