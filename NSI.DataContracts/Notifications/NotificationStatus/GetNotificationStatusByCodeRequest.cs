using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.NotificationStatus
{
    public class GetNotificationStatusByCodeRequest:BaseRequest
    {
        /// <summary>
        /// Notification status code for retrieval
        /// </summary>
        public string Code { get; set; }
    }
}
