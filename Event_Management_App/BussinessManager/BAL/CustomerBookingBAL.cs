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

        public GetAllBookedDetails AddbookEventData(GetAllBookedDetails oData)
        {
            string amount = oData.AddEventModel.Amount;

            string deposit = oData.BookedEventsModel.Deposit;

            double balance = double.Parse(amount) - double.Parse(deposit);

            oData.BookedEventsModel.Balance = balance.ToString();

            oData.BookedEventsModel.Status_Id = 3;

            return _ICustomerBookingDAL.AddbookEventData(oData);
        }
    }
}
