using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

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
			_dBManager.InitDbCommand("DashboardData", CommandType.StoredProcedure);

			GetAllBookedDetails dashboard = null;

			DataSet ds = _dBManager.ExecuteDataSet();

			foreach (DataRow item in ds.Tables[0].Rows)
			{
				dashboard = new GetAllBookedDetails();

				dashboard.SignUpModel = new SignUpModel();
				dashboard.RequestedEventsModel = new RequestedEventsModel();

				dashboard.SignUpModel.Id = item["Id"].ConvertDBNullToInt();
				dashboard.RequestedEventsModel.Id = item["Id"].ConvertDBNullToInt();
				dashboard.RequestedEventsModel.Deposit = item["Deposit"].ConvertDBNullToString();
				
			}
			return dashboard;
		}
	}
}
