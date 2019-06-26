using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.Sms
{
    public class UpdateSmsRequest:BaseRequest
    {
        /// <summary>
        /// Sms update
        /// </summary>
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string From { get; set; }
        public int NotificationId { get; set; }
    }
}
