using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.NotificationStatus
{
    public class UpdateNotificationStatusRequest:BaseRequest
    {
        /// <summary>
        /// NotificationStatus model
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
