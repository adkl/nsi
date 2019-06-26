using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.SmsService.Interfaces
{
    public interface ISmsService
    {
        IEnumerable<string> SendSms(string From, IEnumerable<string> ToPhoneNumbers, string Body, string AccountSid, string AuthToken, Uri StatusCallBackUri = null);
    }
}
