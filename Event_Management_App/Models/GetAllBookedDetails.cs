namespace Event_Management_App.Models
{
    public class GetAllBookedDetails
    {
        public SignUpModel? SignUpModel { get; set; }

        public AddEventModel? AddEventModel { get; set; }

        public BookedEventsModel? BookedEventsModel { get; set; }

        public EventStatusModel? EventStatusModel { get; set; }
    }
}
