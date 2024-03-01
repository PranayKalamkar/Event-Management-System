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
        ICommonDAL _ICommonDAL;

        public AdminBAL(IDBManager dBManager)
        {
            _IAdminDAL = new AdminDAL(dBManager);
            _ICommonDAL = new CommonDAL(dBManager);
        }

        public List<Admin_UserModel> GetAdminList()
        {
            return _IAdminDAL.GetAdminList();
        }

        public string AddAdmin(Admin_UserModel sign, IFormFile idproof, IFormFile profile)
        {
            sign.Role = 1;

            bool emailExist = _ICommonDAL.CheckEmailExist(sign.Email, sign.Id);

            if (emailExist)
            {
                return "Exist";
            }

            sign.IdProofFile = idproof;

            sign.IdProofPath = FileUpload.UploadIdProof(sign.IdProofFile);

            sign.ProfileFile = profile;

            sign.ProfilePath = FileUpload.UploadProfile(sign.ProfileFile);

            _IAdminDAL.AddAdmin(sign);

            return "Success";
        }

        public Admin_UserModel PopulateAdminData(int ID)
        {
            return _IAdminDAL.PopulateAdminData(ID);
        }

        public string UpdateAdminData(Admin_UserModel adminmodel, int ID, IFormFile idproof, IFormFile profile)
        {

            bool emailExist = _ICommonDAL.CheckEmailExist(adminmodel.Email, ID);

            if (emailExist)
            {
                return "Exist";
            }

            adminmodel.IdProofFile = idproof;

            adminmodel.ProfileFile = profile;

            Admin_UserModel model = _ICommonDAL.GetDBImagesbyID(ID);

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
                adminmodel.IdProofPath = FileUpload.UploadIdProof(adminmodel.IdProofFile);
            }
            else
            {
                adminmodel.IdProofPath = existingIdProof;
            }

            if (adminmodel.ProfileFile != null)
            {
                if (!string.IsNullOrEmpty(existingProfile))
                {
                    string oldProfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin_userimage", existingProfile);

                    if (System.IO.File.Exists(oldProfilePath))
                    {
                        System.IO.File.Delete(oldProfilePath);
                    }
                }
                adminmodel.ProfilePath = FileUpload.UploadProfile(adminmodel.ProfileFile);
            }
            else
            {
                adminmodel.ProfilePath = existingProfile;
            }

            _IAdminDAL.UpdateAdminData(adminmodel, ID);

            return "Success";
        }

        public void DeleteAdminData(Admin_UserModel oModel, int ID)
        {
            _IAdminDAL.DeleteAdminData(oModel, ID);
        }

    }
}
