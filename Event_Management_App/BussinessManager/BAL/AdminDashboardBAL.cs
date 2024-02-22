using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.BAL
{
	public class AdminDashboardBAL : IAdminDashboardBAL
	{
		IAdminDashboardDAL _IAdminDashboardDAL;

		public AdminDashboardBAL(IDBManager dBManager)
		{
			_IAdminDashboardDAL = new AdminDashboardDAL(dBManager);
		}

		public GetAllBookedDetails Populate()
		{
			return _IAdminDashboardDAL.Populate();
		}
	}
}
