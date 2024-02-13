using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface IAddEventDAL
    {
        public List<GetAllBookedDetails> AddEventList();

        public AddEventModel AddEvent(AddEventModel addeventmodel);

        public GetAllBookedDetails PopulateEventData(int ID);

        public AddEventModel UpdateEventData(AddEventModel addeventmodel, int ID);

        public string GetDBImagebyID(int ID);

        public void DeleteEventData(int ID);
    }
}
