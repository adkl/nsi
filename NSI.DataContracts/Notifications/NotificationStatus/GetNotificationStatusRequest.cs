﻿using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.NotificationStatus
{
    public class GetNotificationStatusRequest:BaseRequest
    {
        /// <summary>
        /// Notification status ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
