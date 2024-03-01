using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class CommonDAL : ICommonDAL
    {

        readonly IDBManager _dBManager;

        public CommonDAL(IDBManager dbManager)
        {
            _dBManager = dbManager;
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

        public Admin_UserModel GetDBImagesbyID(int ID)
        {
            Admin_UserModel adminmodel = null;

            try
            {
                _dBManager.InitDbCommand("GetAdmin_UserImages", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@u_ID", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    adminmodel = new Admin_UserModel();

                    adminmodel.IdProofPath = item["Idproof"].ConvertDBNullToString();
                    adminmodel.ProfilePath = item["profile"].ConvertDBNullToString();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return adminmodel;
        }

        public bool CheckDateAvailable(string date, string location)
        {
            bool dateExist = false;

            try
            {
                _dBManager.InitDbCommand("CheckDateAvailable", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@newDate", date);
                _dBManager.AddCMDParam("@newLocation", location);

                var result = _dBManager.ExecuteScalar();

                dateExist = Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return dateExist;
        }

        public string GetDBAddEventImagebyID(int ID)
        {
            string existingImage = "";

            try
            {
                _dBManager.InitDbCommand("GetDBImagebyID", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@u_ID", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    existingImage = item["ImagePath"].ConvertJSONNullToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return existingImage;
        }
    }
}
