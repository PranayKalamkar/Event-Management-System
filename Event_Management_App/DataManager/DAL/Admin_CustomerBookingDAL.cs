using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class Admin_CustomerBookingDAL : IAdmin_CustomerBookingDAL
    {
        readonly IDBManager _dBManager;

        public Admin_CustomerBookingDAL(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public List<GetAllBookedDetails> GetBookedEvents()
        {
            List<GetAllBookedDetails> bookedList = new List<GetAllBookedDetails>();

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

        public bool CheckEmailExist(string emailId, int Id)
        {
            _dBManager.InitDbCommand("CheckEmailExist", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@p_EmailId", emailId);
            _dBManager.AddCMDParam("@p_Id", Id);


            var result = _dBManager.ExecuteScalar();

            bool emailExist = Convert.ToBoolean(result);

            return emailExist;
        }

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData, int ID)
        {
            oData.SignUpModel.SPassword = oData.SignUpModel.SPassword + _dBManager.getSalt();

            _dBManager.InitDbCommand("InsertCustomerBookData", CommandType.StoredProcedure);

            _dBManager.AddCMDParam("@p_Username", oData.SignUpModel.Username);
            _dBManager.AddCMDParam("@p_Email", oData.SignUpModel.Email);
            _dBManager.AddCMDParam("@p_SPassword", oData.SignUpModel.SPassword);
            _dBManager.AddCMDParam("@p_RoleId", oData.SignUpModel.Role);
            _dBManager.AddCMDParam("@p_Deposit", oData.RequestedEventsModel.Deposit);
            _dBManager.AddCMDParam("@p_Balance", oData.RequestedEventsModel.Balance);
            _dBManager.AddCMDParam("@p_Date", oData.RequestedEventsModel.Date);
            _dBManager.AddCMDParam("@p_Time", oData.RequestedEventsModel.Time);
            _dBManager.AddCMDParam("@p_addevent_id", ID);
            _dBManager.AddCMDParam("@p_Status_id", oData.RequestedEventsModel.Status_Id);

            _dBManager.ExecuteNonQuery();

            return oData;
        }
    }
}
