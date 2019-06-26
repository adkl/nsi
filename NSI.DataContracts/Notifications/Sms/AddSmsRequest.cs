using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.Sms
{
    public class AddSmsRequest:BaseRequest
    {
        /// <summary>
        /// Sms model
        /// </summary>
        public string PhoneNumber { get; set; }
        public string From { get; set; }
        public int NotificationId { get; set; }
    }
}
