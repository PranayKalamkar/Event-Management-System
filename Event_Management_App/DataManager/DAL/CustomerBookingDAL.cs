using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class CustomerBookingDAL : ICustomerBookingDAL
    {
        readonly IDBManager _dBManager;

        public CustomerBookingDAL(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public List<GetAllBookedDetails> GetBookedEvents()
        {
            List<GetAllBookedDetails> bookedList = new List<GetAllBookedDetails>();

            try
            {
                _dBManager.InitDbCommand("GetAvailableEvent", CommandType.StoredProcedure);

                DataSet ds = _dBManager.ExecuteDataSet();
                foreach (DataRow item in ds.Tables[0].Rows)
                {

                    GetAllBookedDetails bookedEvents = new GetAllBookedDetails();

                    bookedEvents.SignUpModel = new SignUpModel();
                    bookedEvents.AddEventModel = new AddEventModel();
                    bookedEvents.RequestedEventsModel = new RequestedEventsModel();

                    bookedEvents.AddEventModel.Id = item["Id"].ConvertDBNullToInt();
                    bookedEvents.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();

                    bookedList.Add(bookedEvents);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return bookedList;
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            GetAllBookedDetails bookmodel = new GetAllBookedDetails();

            try
            {
                _dBManager.InitDbCommand("GetbookEventbyId", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_id", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    bookmodel.AddEventModel = new AddEventModel();
                    bookmodel.RequestedEventsModel = new RequestedEventsModel();

                    //bookmodel.RequestedEventsModel.Id = item["Id"].ConvertDBNullToInt();
                    bookmodel.AddEventModel.Id = item["Id"].ConvertDBNullToInt();
                    bookmodel.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    bookmodel.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    bookmodel.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    bookmodel.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    bookmodel.AddEventModel.Contact = item["Contact"].ConvertDBNullToString();
                    bookmodel.AddEventModel.Address = item["Address"].ConvertDBNullToString();
                    bookmodel.AddEventModel.Description = item["Description"].ConvertDBNullToString();
                    bookmodel.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return bookmodel;
        }

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData)
        {
            try
            {
                _dBManager.InitDbCommand("InsertCustomerBookData", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_Deposit_in", oData.RequestedEventsModel.Deposit);
                _dBManager.AddCMDParam("@p_Balance_in", oData.RequestedEventsModel.Balance);
                _dBManager.AddCMDParam("@p_Date_in", oData.RequestedEventsModel.Date);
                _dBManager.AddCMDParam("@p_Time_in", oData.RequestedEventsModel.Time);
                _dBManager.AddCMDParam("@p_addevent_id_in", oData.RequestedEventsModel.Addevent_id);
                _dBManager.AddCMDParam("@p_signup_id_in", oData.RequestedEventsModel.Signup_id);
                _dBManager.AddCMDParam("@p_Status_id_in", oData.RequestedEventsModel.Status_Id);
                _dBManager.AddCMDParam("@p_createdby_in", oData.RequestedEventsModel.CreatedBy);
                _dBManager.AddCMDParam("@p_createdat_in", oData.RequestedEventsModel.CreatedAt);


                _dBManager.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return oData;
        }
    }
}
