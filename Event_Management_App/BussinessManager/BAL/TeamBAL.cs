using Event_Management_App.BussinessManager.IBAL;
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
    }
}
