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

            //_dBManager.InitDbCommand("GetAllEvent", CommandType.StoredProcedure);
            _dBManager.InitDbCommand("GetAvailableEvent", CommandType.StoredProcedure);

            DataSet ds = _dBManager.ExecuteDataSet();
            foreach (DataRow item in ds.Tables[0].Rows)
            {

                GetAllBookedDetails bookedEvents = new GetAllBookedDetails();

                bookedEvents.SignUpModel = new SignUpModel();
                bookedEvents.AddEventModel = new AddEventModel();
                bookedEvents.RequestedEventsModel = new RequestedEventsModel();

                try
                {
                    bookedEvents.AddEventModel.Id = item["Id"].ConvertDBNullToInt();
                    bookedEvents.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();

                    bookedList.Add(bookedEvents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return bookedList;
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            _dBManager.InitDbCommand("GetbookEventbyId", CommandType.StoredProcedure);

            GetAllBookedDetails bookmodel = new GetAllBookedDetails();

            _dBManager.AddCMDParam("@p_id", ID);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                bookmodel.AddEventModel = new AddEventModel();
                bookmodel.RequestedEventsModel = new RequestedEventsModel();

                bookmodel.RequestedEventsModel.Id = item["Id"].ConvertDBNullToInt();
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
            return bookmodel;
        }

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData)
        {
            _dBManager.InitDbCommand("InsertbookEvent", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@a_Deposit", oData.RequestedEventsModel.Deposit);
            _dBManager.AddCMDParam("@a_Balance", oData.RequestedEventsModel.Balance);
            _dBManager.AddCMDParam("@a_Date", oData.RequestedEventsModel.Date);
            _dBManager.AddCMDParam("@a_Time", oData.RequestedEventsModel.Time);
            _dBManager.AddCMDParam("@a_addEvent_id", oData.RequestedEventsModel.Addevent_id);
            _dBManager.AddCMDParam("@a_signup_id", oData.RequestedEventsModel.Signup_id);
            _dBManager.AddCMDParam("@a_Status_id", oData.RequestedEventsModel.Status_Id);


            _dBManager.ExecuteNonQuery();

            return oData;
        }
    }
}
