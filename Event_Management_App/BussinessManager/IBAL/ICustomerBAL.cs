﻿using Event_Management_App.Models;

namespace Event_Management_App.BussinessManager.IBAL
{
    public interface ICustomerBAL
    {
        public MessageModel AddMessage(MessageModel oModel);
    }
}
