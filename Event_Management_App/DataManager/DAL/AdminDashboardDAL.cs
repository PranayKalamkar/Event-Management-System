using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Event_Management_App.DataManager.DAL
{
	public class AdminDashboardDAL :IAdminDashboardDAL
	{
		readonly IDBManager _dBManager;

		public AdminDashboardDAL(IDBManager dBManager)
		{
			_dBManager = dBManager;
		}

		public GetAllBookedDetails Populate()
		{
			GetAllBookedDetails oModel = new GetAllBookedDetails();

			_dBManager.InitDbCommand("DashboardData", CommandType.StoredProcedure);

			_dBManager.AddCMDOutParam("@total_events", DbType.Int32, 0);
			_dBManager.AddCMDOutParam("@total_users", DbType.Int32, 0);
			_dBManager.AddCMDOutParam("@total_deposit", DbType.String, 30);

			_dBManager.ExecuteNonQuery();

			oModel.Total_Events = _dBManager.GetOutParam<Int32>("@total_events");
			oModel.Total_Users = _dBManager.GetOutParam<Int32>("@total_users");
			oModel.Total_Deposit = _dBManager.GetOutParam<string>("@total_deposit");

			return oModel;

		}
	}
}
