using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;
using System.Diagnostics;

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
                Console.WriteLine(ex.StackTrace);
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
                    bookmodel.Admin_UserModel = new Admin_UserModel();

                    //bookmodel.RequestedEventsModel.Id = item["Id"].ConvertDBNullToInt();
                    //bookmodel.Admin_UserModel.Id = item["Id"].ConvertDBNullToInt();
                    bookmodel.AddEventModel.Id = item["Id"].ConvertDBNullToInt();
                    //bookmodel.Admin_UserModel.Email = item["Email"].ConvertDBNullToString();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return bookmodel;
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return dateExist;
        }


        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData, int ID, int Signup_Id)
        {
            try
            {

                _dBManager.InitDbCommand("InsertCustomerBookData", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_Deposit", oData.RequestedEventsModel.Deposit);
                _dBManager.AddCMDParam("@p_Balance", oData.RequestedEventsModel.Balance);
                _dBManager.AddCMDParam("@p_Date", oData.RequestedEventsModel.Date);
                _dBManager.AddCMDParam("@p_Time", oData.RequestedEventsModel.Time);
                _dBManager.AddCMDParam("@p_addevent_id", ID);
                _dBManager.AddCMDParam("@p_signup_id_in", Signup_Id);
                _dBManager.AddCMDParam("@p_Status_id", oData.RequestedEventsModel.Status_Id);

                _dBManager.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return oData;
        }

        public List<GetAllBookedDetails> GetAdmin_UserList()
        {
            List<GetAllBookedDetails> userList = new List<GetAllBookedDetails>();

            try
            {
                _dBManager.InitDbCommand("GetAllAdmin_User", CommandType.StoredProcedure);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    GetAllBookedDetails bookedEvents = new GetAllBookedDetails();

                    bookedEvents.Admin_UserModel = new Admin_UserModel();

                    bookedEvents.Admin_UserModel.Id = item["Id"].ConvertDBNullToInt();
                    bookedEvents.Admin_UserModel.Email = item["Email"].ConvertDBNullToString();

                    userList.Add(bookedEvents);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return userList;

        }
    }
}
