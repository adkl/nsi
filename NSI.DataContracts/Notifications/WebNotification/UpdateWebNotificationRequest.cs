﻿using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.WebNotification
{
    public class UpdateWebNotificationRequest: BaseRequest
    {
        public int Id { get; set; }
    }
}
