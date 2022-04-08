namespace BLL.Interfaces
{
    public interface ISmsSender
    {
        bool SendSMSNotification(string phone, string body);
    }
}
