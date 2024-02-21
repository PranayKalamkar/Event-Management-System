using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
    public class AdminBAL : IAdminBAL
    {
        IAdminDAL _IAdminDAL;

        public AdminBAL(IDBManager dBManager)
        {
            _IAdminDAL = new AdminDAL(dBManager);
        }

        public List<Admin_UserModel> GetAdminList()
        {
            return _IAdminDAL.GetAdminList();
        }

        public string AddAdmin(Admin_UserModel sign, IFormFile idproof, IFormFile profile)
        {
            sign.Role = 1;

            bool emailExist = _IAdminDAL.CheckEmailExist(sign.Email, sign.Id);

            if (emailExist)
            {
                return "Exist";
            }

            sign.IdProofFile = idproof;

            sign.IdProofPath = UploadIdProof(sign.IdProofFile);

            sign.ProfileFile = profile;

            sign.ProfilePath = UploadProfile(sign.ProfileFile);

            _IAdminDAL.AddAdmin(sign);

            return "Success";
        }

        public Admin_UserModel PopulateAdminData(int ID)
        {
            return _IAdminDAL.PopulateAdminData(ID);
        }

        public string UpdateAdminData(Admin_UserModel adminmodel, int ID, IFormFile idproof, IFormFile profile)
        {

            bool emailExist = _IAdminDAL.CheckEmailExist(adminmodel.Email, ID);

            if (emailExist)
            {
                return "Exist";
            }

            adminmodel.IdProofFile = idproof;

            adminmodel.ProfileFile = profile;

            Admin_UserModel model = _IAdminDAL.GetDBImagesbyID(ID);

            string existingIdProof = model.IdProofPath.ConvertDBNullToString();

            string existingProfile = model.ProfilePath.ConvertDBNullToString();

            if(adminmodel.IdProofFile != null)
            {
                if (!string.IsNullOrEmpty(existingIdProof))
                {
                    string oldIdPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin_useridproof", existingIdProof);

                    if (System.IO.File.Exists(oldIdPath))
                    {
                        System.IO.File.Delete(oldIdPath);
                    }

                }
                adminmodel.IdProofPath = UploadIdProof(adminmodel.IdProofFile);
            }
            if(adminmodel.ProfileFile != null)
            {
                if (!string.IsNullOrEmpty(existingProfile))
                {
                    string oldProfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin_userimage", existingProfile);

                    if (System.IO.File.Exists(oldProfilePath))
                    {
                        System.IO.File.Delete(oldProfilePath);
                    }
                }
                adminmodel.ProfilePath = UploadProfile(adminmodel.ProfileFile);
            }
            else
            {
                // If no new image is provided, use the existing image
                adminmodel.IdProofPath = existingIdProof;
                adminmodel.ProfilePath = existingProfile;
            }


            _IAdminDAL.UpdateAdminData(adminmodel, ID);

            return "Success";
        }

        public void DeleteAdminData(int ID)
        {
            _IAdminDAL.DeleteAdminData(ID);
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
