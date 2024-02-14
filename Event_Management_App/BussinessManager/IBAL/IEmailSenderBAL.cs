namespace Event_Management_App.BussinessManager.IBAL
{
    public interface IEmailSenderBAL
    {
        Task<bool> EmailSendAsync(string email, string Subject, string message);
    }
}
