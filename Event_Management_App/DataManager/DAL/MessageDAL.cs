using Event_Management_App.CommonCode;
using Event_Management_App.DataManager.IDAL;
using Event_Management_App.Models;
using System.Data;

namespace Event_Management_App.DataManager.DAL
{
    public class MessageDAL : IMessageDAL
    {
        readonly IDBManager _dBManager;

        public MessageDAL(IDBManager dBManager)
        {
            _dBManager = dBManager;
        }

        public List<GetAllBookedDetails> GetMessageList()
        {
            List<GetAllBookedDetails> messageList = new List<GetAllBookedDetails>();

            try
            {
                _dBManager.InitDbCommand("sp_event_messagedal_getallmessage", CommandType.StoredProcedure);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    GetAllBookedDetails oModel = new GetAllBookedDetails();

                    oModel.MessageModel = new MessageModel();

                    oModel.MessageModel.Id = item["Id"].ConvertDBNullToInt();
                    oModel.MessageModel.Location = item["Location"].ConvertDBNullToString();
                    oModel.MessageModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    oModel.MessageModel.Budget = item["Budget"].ConvertDBNullToString();
                    oModel.MessageModel.Occassion = item["Occassion"].ConvertDBNullToString();
                    oModel.MessageModel.Description = item["Description"].ConvertDBNullToString();

                    messageList.Add(oModel);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return messageList;
        }

        public GetAllBookedDetails PopulateMessageData(int ID)
        {
            GetAllBookedDetails oModel = null;

            try
            {
                _dBManager.InitDbCommand("sp_event_messagedal_populatemessagebyid", CommandType.StoredProcedure);

                _dBManager.AddCMDParam("@p_message_id_in", ID);

                DataSet ds = _dBManager.ExecuteDataSet();

                foreach (DataRow item in ds.Tables[0].Rows)
                {

                    oModel = new GetAllBookedDetails();
                    oModel.Admin_UserModel = new Admin_UserModel();
                    oModel.MessageModel = new MessageModel();

                    oModel.MessageModel.Id = item["Id"].ConvertDBNullToInt();
                    oModel.Admin_UserModel.Username = item["Username"].ConvertDBNullToString();
                    oModel.Admin_UserModel.Email = item["Email"].ConvertDBNullToString();
                    oModel.MessageModel.Location = item["Location"].ConvertDBNullToString();
                    oModel.MessageModel.Capacity = item["Capacity"].ConvertDBNullToString();
                    oModel.MessageModel.Budget = item["Budget"].ConvertDBNullToString();
                    oModel.MessageModel.Occassion = item["Occassion"].ConvertDBNullToString();
                    oModel.MessageModel.Description = item["Description"].ConvertDBNullToString();
                    oModel.MessageModel.Signup_Id = item["Signup_Id"].ConvertDBNullToInt();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return oModel;
        }
    }
}
