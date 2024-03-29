﻿using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class BookedEventsDAL : IBookedEventsDAL
    {
        readonly IDBManager _dBManager;

        public BookedEventsDAL(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public List<GetAllBookedDetails> AllBookedEvents()
        {
            List<GetAllBookedDetails> bookedList = new List<GetAllBookedDetails>();

            try
            {
                _dBManager.InitDbCommand("GetAllBookedEvents", CommandType.StoredProcedure);

                DataSet ds = _dBManager.ExecuteDataSet();
                foreach (DataRow item in ds.Tables[0].Rows)
                {

                    GetAllBookedDetails bookedEvents = new GetAllBookedDetails();

                    bookedEvents.SignUpModel = new SignUpModel();
                    bookedEvents.AddEventModel = new AddEventModel();
                    bookedEvents.RequestedEventsModel = new RequestedEventsModel();
                    bookedEvents.EventStatusModel = new EventStatusModel();


                    bookedEvents.RequestedEventsModel.Id = item["Id"].ConvertDBNullToInt();
                    bookedEvents.SignUpModel.Username = item["Username"].ConvertDBNullToString();
                    bookedEvents.SignUpModel.Email = item["Email"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.Contact = item["Contact"].ConvertDBNullToString();
                    bookedEvents.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();
                    bookedEvents.RequestedEventsModel.Deposit = item["Deposit"].ConvertDBNullToString();
                    bookedEvents.RequestedEventsModel.Balance = item["Balance"].ConvertDBNullToString();
                    bookedEvents.RequestedEventsModel.Date = item["Date"].ConvertDBNullToString();
                    bookedEvents.RequestedEventsModel.Time = item["Time"].ConvertDBNullToString();
                    bookedEvents.EventStatusModel.Status = item["status_name"].ConvertDBNullToString();

                    bookedList.Add(bookedEvents);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return bookedList;
        }

        public string GetDBImagebyID(int ID)
        {
            string existingImage = "";

            try
            {
                _dBManager.InitDbCommand("GetDBImagebyID", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@ID", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    existingImage = item["ImagePath"].ConvertJSONNullToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return existingImage;
        }

        public GetAllBookedDetails PopulateBookedEventData(int ID)
        {
            GetAllBookedDetails bookeventmodel = null;

            try
            {
                _dBManager.InitDbCommand("PopulateBookedEvents", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_Id", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    bookeventmodel = new GetAllBookedDetails();
                    bookeventmodel.SignUpModel = new SignUpModel();
                    bookeventmodel.AddEventModel = new AddEventModel();
                    bookeventmodel.RequestedEventsModel = new RequestedEventsModel();
                    bookeventmodel.EventStatusModel = new EventStatusModel();

                    bookeventmodel.RequestedEventsModel.Id = item["Id"].ConvertDBNullToInt();
                    bookeventmodel.SignUpModel.Username = item["Username"].ConvertDBNullToString();
                    bookeventmodel.SignUpModel.Email = item["Email"].ConvertDBNullToString();
                    bookeventmodel.AddEventModel.Category = item["Category"].ConvertDBNullToString();
                    bookeventmodel.AddEventModel.Location = item["Location"].ConvertDBNullToString();
                    bookeventmodel.AddEventModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    bookeventmodel.AddEventModel.Amount = item["Amount"].ConvertDBNullToString();
                    bookeventmodel.AddEventModel.Contact = item["Contact"].ConvertDBNullToString();
                    bookeventmodel.AddEventModel.ImagePath = item["ImagePath"].ConvertDBNullToString();
                    bookeventmodel.RequestedEventsModel.Deposit = item["Deposit"].ConvertDBNullToString();
                    bookeventmodel.RequestedEventsModel.Balance = item["Balance"].ConvertDBNullToString();
                    bookeventmodel.RequestedEventsModel.Date = item["Date"].ConvertDBNullToString();
                    bookeventmodel.RequestedEventsModel.Time = item["Time"].ConvertDBNullToString();
                    bookeventmodel.RequestedEventsModel.Status_Id = item["status_id"].ConvertDBNullToInt();
                    bookeventmodel.EventStatusModel.Status = item["status_name"].ConvertDBNullToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return bookeventmodel;
        }

        public int UpdateBookedEventData(GetAllBookedDetails oData, int Status_Id, int Id)
        {
            try
            {
                _dBManager.InitDbCommand("UpdateBookedStatusById", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@u_Id_in", Id);
                _dBManager.AddCMDParam("@u_status_id_in", Status_Id);
                _dBManager.AddCMDParam("@u_deposit_in", oData.RequestedEventsModel.Deposit);
                _dBManager.AddCMDParam("@u_balance_in", oData.RequestedEventsModel.Balance);
                _dBManager.AddCMDParam("@u_upadtedby_in", oData.RequestedEventsModel.UpdatedBy);
                _dBManager.AddCMDParam("@u_updatedat_in", oData.RequestedEventsModel.UpdatedAt);

                _dBManager.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Status_Id;
        }

        public List<GetAllBookedDetails> GetStatus()
        {
            List<GetAllBookedDetails> status = new List<GetAllBookedDetails>();

            try
            {
                _dBManager.InitDbCommand("GetAllStatus", CommandType.StoredProcedure);

                DataSet ds = _dBManager.ExecuteDataSet();
                foreach (DataRow item in ds.Tables[0].Rows)
                {

                    GetAllBookedDetails bookedEvents = new GetAllBookedDetails();

                    bookedEvents.EventStatusModel = new EventStatusModel();

                    bookedEvents.EventStatusModel.Id = item["status_id"].ConvertDBNullToInt();
                    bookedEvents.EventStatusModel.Status = item["status_name"].ConvertDBNullToString();

                    status.Add(bookedEvents);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return status;
        }
    }
}
