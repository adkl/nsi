using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.NotificationStatus
{
    /// <summary>
    /// Notification status model
    /// </summary>
    public class DeleteNotificationStatusRequest :BaseRequest
    {
        public int Id { get; set; }
    }
}
