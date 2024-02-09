using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IAddEventBAL
    {
        public List<GetAllBookedDetails> AddEventList();

        public AddEventModel AddEvent(AddEventModel addeventmodel, IFormFile ImageFile);

        public GetAllBookedDetails PopulateEventData(int ID);

        public GetAllBookedDetails UpdateEventData(GetAllBookedDetails addeventmodel, int ID, IFormFile file);

        public void DeleteEventData(int ID);

        public string UploadImage(IFormFile imageFile);
    }
}
