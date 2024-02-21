﻿namespace Event_Management_App.Models
{
    public class GetAllBookedDetails
    {
        public SignUpModel? SignUpModel { get; set; }

        public AddEventModel? AddEventModel { get; set; }

        public RequestedEventsModel? RequestedEventsModel { get; set; }

        public EventStatusModel? EventStatusModel { get; set; }

        public Admin_UserModel? Admin_UserModel { get; set; }
    }
}
