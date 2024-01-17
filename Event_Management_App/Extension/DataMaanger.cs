﻿using Event_Management_App.BussinessManager.BAL;
using Event_Management_App.BussinessManager.IBAL;
using Event_Management_App.DataManager.DAL;
using Event_Management_App.DataManager.IDAL;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Event_Management_App.Extension
{
    public static class DataMaanger
    {
        public static IServiceCollection AddAppSetting(this IServiceCollection services)
        {
            services.AddScoped<IDBManager>(AddDBManager);
            //services.AddScoped<IEmployeeBAL, EmployeeBAL>;
            services.AddScoped<IEventBAL, EventBAL>();
            services.AddScoped<IAddEventBAL, AddEventBAL>();
            services.AddScoped<IAdmin_UserBAL, Admin_UserBAL>();
            services.AddScoped<IAdminBAL, AdminBAL>();

            return services;
        }
        internal static IDBManager AddDBManager(IServiceProvider serviceProvider)
        {
            IConfiguration Configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string dbconstr = Configuration.GetConnectionString("DefaultConnection");
            string salt = Configuration.GetValue<string>("salt");
            
            return GetDBManager(dbconstr, salt);

        }
        public static IDBManager GetDBManager(string connectionString, string salt)
        {
            DbConnection dbconn = new MySqlConnection(connectionString);
            return new DBManager(dbconn, salt);
        }
    }
}
