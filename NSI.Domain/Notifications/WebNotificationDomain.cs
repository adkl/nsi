using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Notifications
{
    public class WebNotificationDomain: NotificationDomain
    {
        public bool Seen { get; set; }
        public DateTime? DateSeen { get; set; }
        public int NotificationId { get; set; }
        public int UserInfoId { get; set; }
        public int UserTenantId { get; set; }
    }
}
