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
            return eventList;
        }

        public AddEventModel AddEvent(AddEventModel addeventmodel)
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


            _dBManager.ExecuteNonQuery();

            return addeventmodel;
        }

        public string GetDBImagebyID(int ID)
        {
            string existingImage = null;

            _dBManager.InitDbCommand("GetDBImagebyID", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@u_ID", ID);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                existingImage = item["ImagePath"].ConvertJSONNullToString();
            }

            return existingImage;
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            _dBManager.InitDbCommand("PopulateAddEventbyId", CommandType.StoredProcedure);

            GetAllBookedDetails addeventmodel = null;

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
            return addeventmodel;
        }

        public GetAllBookedDetails UpdateEventData(GetAllBookedDetails addeventmodel, int ID)
        {
            _dBManager.InitDbCommand("UpdateaddEventById", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("u_Id", ID);
            _dBManager.AddCMDParam("u_Category", addeventmodel.AddEventModel.Category);
            _dBManager.AddCMDParam("u_Location", addeventmodel.AddEventModel.Location);
            _dBManager.AddCMDParam("u_Capacity", addeventmodel.AddEventModel.Capacity);
            _dBManager.AddCMDParam("u_Amount", addeventmodel.AddEventModel.Amount);
            _dBManager.AddCMDParam("u_Description", addeventmodel.AddEventModel.Description);
            _dBManager.AddCMDParam("u_Address", addeventmodel.AddEventModel.Address);
            _dBManager.AddCMDParam("u_Contact", addeventmodel.AddEventModel.Contact);
            _dBManager.AddCMDParam("u_ImagePath", addeventmodel.AddEventModel.ImagePath);
            _dBManager.AddCMDParam("u_Status_id", addeventmodel.AddEventModel.Status_Id);

            _dBManager.ExecuteNonQuery();

            return addeventmodel;
        }

        public void DeleteEventData(int ID)
        {
            _dBManager.InitDbCommand("DeleteAddEventById", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@deleteId", ID);

            _dBManager.ExecuteNonQuery();
        }
    }
}
