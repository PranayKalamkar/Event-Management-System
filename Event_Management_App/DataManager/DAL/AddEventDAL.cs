using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class AddEventDAL : IAddEventDAL
    {
        readonly IDBManager _dBManager;

        public AddEventDAL(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public List<GetAllBookedDetails> AddEventList()
        {
            List<GetAllBookedDetails> eventList = new List<GetAllBookedDetails>();

            try
            {
                _dBManager.InitDbCommand("GetAllEvent", CommandType.StoredProcedure);

                DataSet ds = _dBManager.ExecuteDataSet();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    GetAllBookedDetails addeventmodel = new GetAllBookedDetails();
                    addeventmodel.AddEventModel = new AddEventModel();
                    addeventmodel.EventStatusModel = new EventStatusModel();

                    addeventmodel.AddEventModel.Id = item["Id"].ConvertDBNullToInt();
                    addeventmodel.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Description = item["Description"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Address = item["Address"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Contact = item["Contact"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();
                    addeventmodel.EventStatusModel.Status = item["status_name"].ConvertDBNullToString();

                    eventList.Add(addeventmodel);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return eventList;
        }

        public AddEventModel AddEvent(AddEventModel addeventmodel)
        {

            try
            {
                _dBManager.InitDbCommand("AddEventInsert", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@a_Category", addeventmodel.Category);
                _dBManager.AddCMDParam("@a_Location", addeventmodel.Location);
                _dBManager.AddCMDParam("@a_Capacity", addeventmodel.Capacity);
                _dBManager.AddCMDParam("@a_Amount", addeventmodel.Amount);
                _dBManager.AddCMDParam("@a_Description", addeventmodel.Description);
                _dBManager.AddCMDParam("@a_Address", addeventmodel.Address);
                _dBManager.AddCMDParam("@a_Contact", addeventmodel.Contact);
                _dBManager.AddCMDParam("@a_ImagePath", addeventmodel.ImagePath);
                _dBManager.AddCMDParam("@a_Status_Id", addeventmodel.Status_Id);
                _dBManager.AddCMDParam("@a_createdby_in", addeventmodel.CreatedBy);
                _dBManager.AddCMDParam("@a_createdat_in", addeventmodel.CreatedAt);


                _dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return addeventmodel;
        }

        public string GetDBImagebyID(int ID)
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return existingImage;
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            GetAllBookedDetails addeventmodel = null;

            try
            {
                _dBManager.InitDbCommand("PopulateAddEventbyId", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_id", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    addeventmodel = new GetAllBookedDetails();

                    addeventmodel.AddEventModel = new AddEventModel();
                    addeventmodel.EventStatusModel = new EventStatusModel();

                    addeventmodel.AddEventModel.Id = item["Id"].ConvertDBNullToInt();
                    addeventmodel.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Description = item["Description"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Address = item["Address"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.Contact = item["Contact"].ConvertDBNullToString();
                    addeventmodel.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();
                    addeventmodel.EventStatusModel.Status = item["status_name"].ConvertDBNullToString();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            return addeventmodel;
        }

        public AddEventModel UpdateEventData(AddEventModel addeventmodel, int ID)
        {
            try
            {
                _dBManager.InitDbCommand("UpdateaddEventById", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("u_Id", ID);
                _dBManager.AddCMDParam("u_Category", addeventmodel.Category);
                _dBManager.AddCMDParam("u_Location", addeventmodel.Location);
                _dBManager.AddCMDParam("u_Capacity", addeventmodel.Capacity);
                _dBManager.AddCMDParam("u_Amount", addeventmodel.Amount);
                _dBManager.AddCMDParam("u_Description", addeventmodel.Description);
                _dBManager.AddCMDParam("u_Address", addeventmodel.Address);
                _dBManager.AddCMDParam("u_Contact", addeventmodel.Contact);
                _dBManager.AddCMDParam("u_ImagePath", addeventmodel.ImagePath);
                _dBManager.AddCMDParam("u_Status_id", addeventmodel.Status_Id);
                _dBManager.AddCMDParam("u_updatedby_in", addeventmodel.UpdatedBy);
                _dBManager.AddCMDParam("u_updatedat_in", addeventmodel.UpdatedAt);

                _dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return addeventmodel;
        }

        public void DeleteEventData(int ID)
        {
            int isDelete = 1;

            try
            {
                _dBManager.InitDbCommand("DeleteAddEventById", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@deleteId_in", ID);
                _dBManager.AddCMDParam("@deleteValue_in", isDelete);

                _dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
