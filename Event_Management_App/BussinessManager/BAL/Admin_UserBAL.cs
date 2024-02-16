using Event_Management_App.BussinessManager.IBAL;
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

            if (adminusermodel.IdProofFile != null)
            {
                adminusermodel.IdProofPath = UploadIdProof(adminusermodel.IdProofFile);
            }

            adminusermodel.ProfileFile = profile;

            if (adminusermodel.ProfileFile != null)
            {
                adminusermodel.ProfilePath = UploadProfile(adminusermodel.ProfileFile);
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
