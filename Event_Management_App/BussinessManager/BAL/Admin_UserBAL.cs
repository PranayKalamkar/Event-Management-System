using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class Admin_UserBAL : IAdmin_UserBAL
    {
        IAdmin_UserDAL _IAdmin_UserDAL;

        public Admin_UserBAL(IDBManager dBManager)
        {
            _IAdmin_UserDAL = new Admin_UserDAL(dBManager);
        }

        public List<Admin_UserModel> GetAdmin_UserList()
        {
            return _IAdmin_UserDAL.GetAdmin_UserList();
        }

        public string AddAdmin_User(Admin_UserModel sign, IFormFile idproof, IFormFile profile)
        {
            sign.Role = 2;

            bool emailExist = _IAdmin_UserDAL.CheckEmailExist(sign.Email, sign.Id);

            if (emailExist)
            {
                return "Exist";
            }

            sign.IdProofFile = idproof;

            sign.IdProofPath = UploadIdProof(sign.IdProofFile);

            sign.ProfileFile = profile;

            sign.ProfilePath = UploadProfile(sign.ProfileFile);

            _IAdmin_UserDAL.AddAdmin_User(sign);

            return "Success";
        }

        public Admin_UserModel PopulateAdmin_UserData(int ID)
        {
            return _IAdmin_UserDAL.PopulateAdmin_UserData(ID);
        }

        public string UpdateAdmin_UserData(Admin_UserModel adminusermodel, int ID, IFormFile idproof, IFormFile profile)
        {

            bool emailExist = _IAdmin_UserDAL.CheckEmailExist(adminusermodel.Email, ID);

            if (emailExist)
            {
                return "Exist";
            }

            adminusermodel.IdProofFile = idproof;

            adminusermodel.ProfileFile = profile;

            Admin_UserModel model = _IAdmin_UserDAL.GetDBImagesbyID(ID);

            string existingIdProof = model.IdProofPath.ConvertDBNullToString();

            string existingProfile = model.ProfilePath.ConvertDBNullToString();

            if (adminusermodel.IdProofFile != null)
            {
                if (!string.IsNullOrEmpty(existingIdProof))
                {
                    string oldIdPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin_useridproof", existingIdProof);

                    if (System.IO.File.Exists(oldIdPath))
                    {
                        System.IO.File.Delete(oldIdPath);
                    }

                }
                adminusermodel.IdProofPath = UploadIdProof(adminusermodel.IdProofFile);
            }
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
                adminusermodel.IdProofPath = existingIdProof;
                adminusermodel.ProfilePath = existingProfile;
            }

            _IAdmin_UserDAL.UpdateAdmin_UserData(adminusermodel, ID);

            return "Success";
        }

        public void DeleteAdmin_UserData(int ID)
        {
            _IAdmin_UserDAL.DeleteAdmin_UserData(ID);
        }


        public string UploadIdProof(IFormFile imageFile)
        {

            try
            {
                string uniqueFileName = null;

                if (imageFile != null)
                {
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin_useridproof");

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
