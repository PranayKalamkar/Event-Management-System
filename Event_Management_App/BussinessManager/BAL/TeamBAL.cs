using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class TeamBAL : ITeamBAL
    {
        ITeamDAL _ITeamDAL;

        public TeamBAL(IDBManager dBManager)
        {
            _ITeamDAL = new TeamDAL(dBManager);
        }

        public Admin_UserModel PopulateTeamData(int ID)
        {
            return _ITeamDAL.PopulateTeamData(ID);
        }

        public string UpdateProfileData(Admin_UserModel adminusermodel, int ID, IFormFile profile)
        {

            bool emailExist = _ITeamDAL.CheckEmailExist(adminusermodel.Email, ID);

            if (emailExist)
            {
                return "Exist";
            }


            adminusermodel.ProfileFile = profile;

            Admin_UserModel model = _ITeamDAL.GetDBImagesbyID(ID);

            string existingProfile = model.ProfilePath.ConvertDBNullToString();

            if (adminusermodel.ProfileFile != null)
            {
                if (!string.IsNullOrEmpty(existingProfile))
                {
                    string oldProfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin_userimage", existingProfile);

                    if (System.IO.File.Exists(oldProfilePath))
                    {
                        System.IO.File.Delete(oldProfilePath);
                    }
                }
                adminusermodel.ProfilePath = UploadProfile(adminusermodel.ProfileFile);
            }
            else
            {
                // If no new image is provided, use the existing image
                adminusermodel.ProfilePath = existingProfile;
            }

            _ITeamDAL.UpdateTeamData(adminusermodel, ID);

            return "Success";
        }

        public string UploadProfile(IFormFile imageFile)
        {

            try
            {
                string uniqueFileName = null;

                if (imageFile != null)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin_userimage");

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
