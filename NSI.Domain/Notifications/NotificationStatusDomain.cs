using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Notifications
{
    public class NotificationStatusDomain : BaseDomain
    {
        public String Name { get; set; }
        public String Code { get; set; }
    }
}
