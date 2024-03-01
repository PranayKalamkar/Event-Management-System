using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface ICommonDAL
    {
        public bool CheckEmailExist(string emailId, int Id);

        public Admin_UserModel GetDBImagesbyID(int ID);

        public bool CheckDateAvailable(string date, string location);

        public string GetDBAddEventImagebyID(int ID);
    }
}
