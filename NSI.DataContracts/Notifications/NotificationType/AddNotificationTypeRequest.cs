using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.NotificationType
{
    public class AddNotificationTypeRequest: BaseRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
