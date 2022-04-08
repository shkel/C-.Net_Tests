using System;

namespace BLL.Utils
{
    internal class SmsClient
    {
        private string smsConnectionString;

        public SmsClient(string smsConnectionString)
        {
            this.smsConnectionString = smsConnectionString;
        }

        internal SmsSendResult Send(string fromPhone, string phone, string v)
        {
            throw new NotImplementedException();
        }
    }
}