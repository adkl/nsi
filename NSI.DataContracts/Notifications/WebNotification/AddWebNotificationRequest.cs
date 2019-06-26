using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.WebNotification
{
    public class AddWebNotificationRequest: BaseRequest
    {
        public int NotificationId { get; set; }
        public int UserInfoId { get; set; }
        public int UserTenantId { get; set; }
    }
}
