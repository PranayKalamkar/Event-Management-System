﻿using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IAddEventBAL
    {
        public List<AddEventModel> AddEventList();

        public AddEventModel AddEvent(AddEventModel addeventmodel, IFormFile ImageFile);

        public AddEventModel PopulateEventData(int ID);

        public AddEventModel UpdateEventData(AddEventModel addeventmodel, int ID, IFormFile file);

        public void DeleteEventData(int ID);

        public string UploadImage(IFormFile imageFile);
    }
}
