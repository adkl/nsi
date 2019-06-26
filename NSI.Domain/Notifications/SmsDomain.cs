using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Notifications
{
    public class SmsDomain:BaseDomain
    {
        public String PhoneNumber { get; set; }
        public String From { get; set; }
        public int NotificationId { get; set; }

    }
}
