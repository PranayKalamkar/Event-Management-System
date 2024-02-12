using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class Admin_CustomerBookingBAL : IAdmin_CustomerBookingBAL
    {
        IAdmin_CustomerBookingDAL _IAdmin_CustomerBookingDAL;


        public Admin_CustomerBookingBAL(IDBManager dBManager)
        {
            _IAdmin_CustomerBookingDAL = new Admin_CustomerBookingDAL(dBManager);

        }

        public List<GetAllBookedDetails> GetBookedEvents()
        {
            return _IAdmin_CustomerBookingDAL.GetBookedEvents();
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            return _IAdmin_CustomerBookingDAL.PopulateEventData(ID);
        }

        public string AddbookEventData(GetAllBookedDetails oData, int ID)
        {
            bool emailExist = _IAdmin_CustomerBookingDAL.CheckEmailExist(oData.SignUpModel.Email, oData.SignUpModel.Id);

            if (emailExist)
            {
                return "Exist";
            }

            string amount = oData.AddEventModel.Amount;

            string deposit = oData.RequestedEventsModel.Deposit;

            double balance = double.Parse(amount) - double.Parse(deposit);

            oData.RequestedEventsModel.Balance = balance.ToString();

            oData.SignUpModel.Role = 2;

            oData.RequestedEventsModel.Status_Id = 3;

            _IAdmin_CustomerBookingDAL.AddbookEventData(oData, ID);

            return "Success";

        }
    }
}
