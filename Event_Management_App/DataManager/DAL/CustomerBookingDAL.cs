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

            _dBManager.InitDbCommand("GetAllEvent", CommandType.StoredProcedure);

            DataSet ds = _dBManager.ExecuteDataSet();
            foreach (DataRow item in ds.Tables[0].Rows)
            {

                GetAllBookedDetails bookedEvents = new GetAllBookedDetails();

                bookedEvents.SignUpModel = new SignUpModel();
                bookedEvents.AddEventModel = new AddEventModel();
                bookedEvents.BookedEventsModel = new BookedEventsModel();

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
                bookmodel.BookedEventsModel = new BookedEventsModel();

                bookmodel.BookedEventsModel.Id = item["Id"].ConvertDBNullToInt();
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

            _dBManager.AddCMDParam("@a_Deposit", oData.BookedEventsModel.Deposit);
            _dBManager.AddCMDParam("@a_Balance", oData.BookedEventsModel.Balance);
            _dBManager.AddCMDParam("@a_Date", oData.BookedEventsModel.Date);
            _dBManager.AddCMDParam("@a_Time", oData.BookedEventsModel.Time);
            _dBManager.AddCMDParam("@a_Status", oData.BookedEventsModel.Status);
            _dBManager.AddCMDParam("@a_addEvent", oData.BookedEventsModel.Addevent_id);
            _dBManager.AddCMDParam("@a_signup", oData.BookedEventsModel.Signup_id);


            _dBManager.ExecuteNonQuery();

            return oData;
        }
    }
}
