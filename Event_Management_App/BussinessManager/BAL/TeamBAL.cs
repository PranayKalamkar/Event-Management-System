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
        ICommonDAL _ICommonDAL;

        public TeamBAL(IDBManager dBManager)
        {
            _ITeamDAL = new TeamDAL(dBManager);
            _ICommonDAL = new CommonDAL(dBManager);
        }

        public Admin_UserModel PopulateTeamData(int ID)
        {
            return _ITeamDAL.PopulateTeamData(ID);
        }

        public string UpdateProfileData(Admin_UserModel adminusermodel, int ID, IFormFile profile)
        {

            bool emailExist = _ICommonDAL.CheckEmailExist(adminusermodel.Email, ID);

            if (emailExist)
            {
                return "Exist";
            }


            adminusermodel.ProfileFile = profile;

            Admin_UserModel model = _ICommonDAL.GetDBImagesbyID(ID);

            string existingIdProof = model.IdProofPath.ConvertDBNullToString();

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
                adminusermodel.ProfilePath = FileUpload.UploadProfile(adminusermodel.ProfileFile);
            }
            else
            {
                adminusermodel.ProfilePath = existingProfile;
            }

            adminusermodel.IdProofPath = existingIdProof;

            _ITeamDAL.UpdateTeamData(adminusermodel, ID);

            return "Success";
        }

    }
}
