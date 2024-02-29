using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class CustomerDAL : ICustomerDAL
    {
        readonly IDBManager _dBManager;

        public CustomerDAL(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public MessageModel AddMessage(MessageModel oModel)
        {

            try
            {
                _dBManager.InitDbCommand("sp_event_customerdal_insertmessage", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@a_location_in", oModel.Location);
                _dBManager.AddCMDParam("@a_capacity_in", oModel.Capacity);
                _dBManager.AddCMDParam("@a_budget_in", oModel.Budget);
                _dBManager.AddCMDParam("@a_occassion_in", oModel.Occassion);
                _dBManager.AddCMDParam("@a_description_in", oModel.Description);
                _dBManager.AddCMDParam("@a_signup_id_in", oModel.Signup_Id);
                _dBManager.AddCMDParam("@a_createdby_in", oModel.CreatedBy);
                _dBManager.AddCMDParam("@a_createdat_in", oModel.CreatedAt);

                _dBManager.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return oModel;
        }
    }
}
