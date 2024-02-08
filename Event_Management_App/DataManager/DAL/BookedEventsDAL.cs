using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class BookedEventsDAL : IBookedEventsDAL
    {
        readonly IDBManager _dBManager;

        public BookedEventsDAL(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public List<GetAllBookedDetails> GetBookedEvents() 
        {
            List<GetAllBookedDetails> bookedList = new List<GetAllBookedDetails>();

            _dBManager.InitDbCommand("GetAllBookedEvents", CommandType.StoredProcedure);

            DataSet ds = _dBManager.ExecuteDataSet();
            foreach (DataRow item in ds.Tables[0].Rows)
            {

                GetAllBookedDetails bookedEvents = new GetAllBookedDetails();

                bookedEvents.SignUpModel = new SignUpModel();
                bookedEvents.AddEventModel = new AddEventModel();
                bookedEvents.BookedEventsModel = new BookedEventsModel();

                try
                {
                    bookedEvents.BookedEventsModel.Id = item["Id"].ConvertDBNullToInt();
                    bookedEvents.SignUpModel.Username = item["Username"].ConvertDBNullToString();
                    bookedEvents.SignUpModel.Email = item["Email"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Contact = item["Contact"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();
                    bookedEvents.BookedEventsModel.Deposit = item["Deposit"].ConvertDBNullToString();
                    bookedEvents.BookedEventsModel.Balance = item["Balance"].ConvertDBNullToString();
                    bookedEvents.BookedEventsModel.Date = item["Date"].ConvertDBNullToString();
                    bookedEvents.BookedEventsModel.Time = item["Time"].ConvertDBNullToString();
                    bookedEvents.BookedEventsModel.Status = item["Status"].ConvertDBNullToString();

                    bookedList.Add(bookedEvents);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return bookedList;
        }

        public string GetDBImagebyID(int ID)
        {
            string existingImage = null;

            _dBManager.InitDbCommand("GetDBImagebyID", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@ID", ID);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                existingImage = item["ImagePath"].ConvertJSONNullToString();
            }

            return existingImage;
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            _dBManager.InitDbCommand("PopulateBookedEvents", CommandType.StoredProcedure);

            GetAllBookedDetails bookeventmodel = null;

            _dBManager.AddCMDParam("@p_Id", ID);

            DataSet ds = _dBManager.ExecuteDataSet();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                bookeventmodel = new GetAllBookedDetails();
                bookeventmodel.SignUpModel = new SignUpModel();
                bookeventmodel.AddEventModel = new AddEventModel();
                bookeventmodel.BookedEventsModel = new BookedEventsModel();

                bookeventmodel.SignUpModel.Username = item["Username"].ConvertDBNullToString();
                bookeventmodel.SignUpModel.Email = item["Email"].ConvertDBNullToString();
                bookeventmodel.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                bookeventmodel.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                bookeventmodel.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                bookeventmodel.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                bookeventmodel.AddEventModel.Contact = item["Contact"].ConvertDBNullToString();
                bookeventmodel.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();
                bookeventmodel.BookedEventsModel.Deposit = item["Deposit"].ConvertDBNullToString();
                bookeventmodel.BookedEventsModel.Balance = item["Balance"].ConvertDBNullToString();
                bookeventmodel.BookedEventsModel.Date = item["Date"].ConvertDBNullToString();
                bookeventmodel.BookedEventsModel.Time = item["Time"].ConvertDBNullToString();
                bookeventmodel.BookedEventsModel.Status = item["Status"].ConvertDBNullToString();
            }
            return bookeventmodel;
        }

        public GetAllBookedDetails UpdateEventData(GetAllBookedDetails bookevent, int Id)
        {

            GetAllBookedDetails bookeventmodel = null;

            return bookeventmodel;
        }

    }
}
