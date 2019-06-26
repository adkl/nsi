using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.EmailMessage
{
    public class SendEmailRequest: BaseRequest
    {
        public IEnumerable<string> ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> CcEmails { get; set; }
        public IEnumerable<string> BccEmails { get; set; }
        public IEnumerable<byte[]> Attachments { get; set; }
    }
}
