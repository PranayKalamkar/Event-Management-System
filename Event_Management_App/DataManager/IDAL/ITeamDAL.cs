﻿using Event_Management_App.Models;

namespace Event_Management_App.DataManager.IDAL
{
    public interface ITeamDAL
    {
        public Admin_UserModel PopulateTeamData(int ID);
    }
}
