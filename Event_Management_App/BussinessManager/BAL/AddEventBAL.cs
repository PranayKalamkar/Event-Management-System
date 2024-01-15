﻿using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class AddEventBAL : IAddEventBAL
    {
        IAddEventDAL _IAddEventDAL;

        public AddEventBAL(IDBManager dBManager)
        {
            _IAddEventDAL = new AddEventDAL(dBManager);
        }

        List<AddEventModel> IAddEventBAL.AddEventList()
        {
            return _IAddEventDAL.AddEventList();
        }

        public AddEventModel AddEvent(AddEventModel addeventmodel, IFormFile ImageFile)
        {
            addeventmodel.ImageFile = ImageFile;

            addeventmodel.ImagePath = UploadImage(addeventmodel.ImageFile);

            return _IAddEventDAL.AddEvent(addeventmodel);
        }

        public AddEventModel PopulateEventData(int ID)
        {
            return _IAddEventDAL.PopulateEventData(ID);
        }

        public AddEventModel UpdateEventData(AddEventModel addeventmodel, int ID, IFormFile file)
        {
            addeventmodel.Id = ID;

            addeventmodel.ImageFile = file;

            string existingImage = _IAddEventDAL.GetDBImagebyID(ID);

            if(addeventmodel.ImageFile != null)
            {
                addeventmodel.ImagePath = UploadImage(addeventmodel.ImageFile);
            }
            else
            {
                addeventmodel.ImagePath = existingImage;
            }

            return _IAddEventDAL.UpdateEventData(addeventmodel);

        }

        public void DeleteEventData(int ID)
        {
            string existingImage = _IAddEventDAL.GetDBImagebyID(ID);

            if(!string.IsNullOrEmpty(existingImage))
            {
                string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "addeventimages", existingImage);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _IAddEventDAL.DeleteEventData(ID);
        }

        public string UploadImage(IFormFile imageFile)
        {

            try
            {
                string uniqueFileName = null;

                if (imageFile != null)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "addeventimages");

                    Console.WriteLine(Directory.GetCurrentDirectory());

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                }
                else
                {
                    Console.WriteLine("Image file path is null");
                }

                Console.WriteLine(uniqueFileName);

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
        }
    }
}
