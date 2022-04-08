using BLL.Interfaces;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BLL.Utils
{
    public class SenderUtils : ISmsSender, ITrainigEmailSender
    {
        public string FromEmail { get; set; }
        public string SmsConnectionString { get; set; }
        public string FromPhone { get; set; }

        public SenderUtils(string fromEmail, string smsConnectionString, string fromPhone)
        {
            FromEmail = fromEmail;
            SmsConnectionString = smsConnectionString;
            FromEmail = fromEmail;
        }

        public SenderUtils() { }

        public async Task SendNotificationToStudent(string email, string name)
        {
            await SendEmailToUser(email, "Hi, " + name, "You missed 3 lectures.");
        }

        public async Task SendNotificationToLector(string email, string name)
        {
            await SendEmailToUser(email, "Hi, " + name, "Your students missed 3 lectures.");
        }

        public bool SendSMSNotification(string phone, string body)
        {
            if (SmsConnectionString == null || FromPhone == null)
            {
                throw new TrainingException("Can't send email. Arguments is null");
            }
            SmsClient smsClient = new SmsClient(SmsConnectionString);
            return smsClient.Send(FromPhone, phone, body).Successful;
        }

        private SmtpClient MakeClient()
        {
            return new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,

            };
        }

        private async Task SendEmailToUser(string email, string subject, string body)
        {
            if (FromEmail == null)
            {
                throw new TrainingException("FromEmail is Null");
            }
            try
            {
                using var client = MakeClient();
                MailMessage message = new MailMessage(FromEmail, email);
                message.Subject = subject;
                message.Body = body;
                await client.SendMailAsync(message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new TrainingException("Can't create an email client", ex);
            }
            catch (ArgumentException ex)
            {
                throw new TrainingException("Can't create a mail message", ex);
            }
        }
    }
}
