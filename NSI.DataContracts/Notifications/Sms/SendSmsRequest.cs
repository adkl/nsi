using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.Sms
{

    //TODO Remove this class - it's only for testing
    public class SendSmsRequest:BaseRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string MessageBody { get; set; }
    }
}
