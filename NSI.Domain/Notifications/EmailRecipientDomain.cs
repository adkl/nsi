using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Notifications
{
    public class EmailRecipientDomain : BaseDomain
    {
        public string EmailAddress { get; set; }
        public int EmaiMessagelId { get; set; }
        public int EmailRecipientTypeId { get; set; }
        public EmailMessageDomain EmailMessage { get; set; } 
        public EmailRecipientTypeDomain EmailRecipientType { get; set; }
    }
}
