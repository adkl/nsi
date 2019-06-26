using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.Sms
{
    public class GetSmsByNotificationIdRequest:BaseRequest
    {
        /// <summary>
        /// Notification ID of Sent sms
        /// </summary>
        public int Id { get; set; }
    }
}
