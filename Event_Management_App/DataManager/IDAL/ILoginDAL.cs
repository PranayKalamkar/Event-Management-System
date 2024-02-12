namespace Event_Management_App.DataManager.IDAL
{
    public interface ILoginDAL
    {
        public string GetPassword(string pass);

        public int GetRole(string email);

        public int GetId(string email);

        public string LoginUser(string email);

        public bool CheckEmailExist(string emailId, int ID);
    }
}
