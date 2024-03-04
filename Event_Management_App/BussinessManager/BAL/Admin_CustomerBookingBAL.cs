using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class Admin_CustomerBookingBAL : IAdmin_CustomerBookingBAL
    {
        IAdmin_CustomerBookingDAL _IAdmin_CustomerBookingDAL;
        ICommonDAL _ICommonDAL;

        public Admin_CustomerBookingBAL(IDBManager dBManager)
        {
            _IAdmin_CustomerBookingDAL = new Admin_CustomerBookingDAL(dBManager);
            _ICommonDAL = new CommonDAL(dBManager);
        }

        public List<GetAllBookedDetails> GetBookedEvents()
        {
            return _IAdmin_CustomerBookingDAL.GetBookedEvents();
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            return _IAdmin_CustomerBookingDAL.PopulateEventData(ID);
        }

        public string AddbookEventData(GetAllBookedDetails oData, int ID, int Signup_Id)
        {

            bool dateExist = _ICommonDAL.CheckDateAvailable(oData.RequestedEventsModel.Date, oData.AddEventModel.Location);

            if(dateExist)
            {
                return "Exist";
            }

            string amount = oData.AddEventModel.Amount;

            string deposit = oData.RequestedEventsModel.Deposit;

            if(double.Parse(deposit) > 0.4 * (double.Parse(amount)))
            {
                return "Less";
            }

            double balance = double.Parse(amount) - double.Parse(deposit);

            oData.RequestedEventsModel.Balance = balance.ToString();

            //oData.SignUpModel.Role = 2;

            oData.RequestedEventsModel.Status_Id = 3;

            _IAdmin_CustomerBookingDAL.AddbookEventData(oData, ID, Signup_Id);

            return "Success";

        }

        public List<GetAllBookedDetails> GetAdmin_UserList()
        {
            return _IAdmin_CustomerBookingDAL.GetAdmin_UserList();
        }
    }
}
