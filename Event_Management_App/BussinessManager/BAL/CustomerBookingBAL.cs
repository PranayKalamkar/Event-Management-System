using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class CustomerBookingBAL : ICustomerBookingBAL
    {
        ICustomerBookingDAL _ICustomerBookingDAL;


        public CustomerBookingBAL(IDBManager dBManager)
        {
            _ICustomerBookingDAL = new CustomerBookingDAL(dBManager);

        }

        public List<GetAllBookedDetails> GetBookedEvents()
        {
            return _ICustomerBookingDAL.GetBookedEvents();
        }

        public GetAllBookedDetails PopulateEventData(int ID)
        {
            return _ICustomerBookingDAL.PopulateEventData(ID);
        }

        public string AddbookEventData(GetAllBookedDetails oData)
        {
            bool dateExist = _ICustomerBookingDAL.CheckDateAvailable(oData.RequestedEventsModel.Date, oData.AddEventModel.Location);

            if (dateExist)
            {
                return "Exist";
            }

            string amount = oData.AddEventModel.Amount;

            string deposit = oData.RequestedEventsModel.Deposit;

            double balance = double.Parse(amount) - double.Parse(deposit);

            oData.RequestedEventsModel.Balance = balance.ToString();

            oData.RequestedEventsModel.Status_Id = 3;

            _ICustomerBookingDAL.AddbookEventData(oData);

            return "Success";
        }
    }
}
