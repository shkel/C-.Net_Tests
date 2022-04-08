using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITrainigEmailSender
    {
        Task SendNotificationToStudent(string email, string name);
        Task SendNotificationToLector(string email, string name);
    }
}
