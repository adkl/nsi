using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Notifications
{
    public class EmailRecipientTypeDomain : BaseDomain
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<EmailRecipientDomain> EmailRecipients { get; set; }

    }
}
