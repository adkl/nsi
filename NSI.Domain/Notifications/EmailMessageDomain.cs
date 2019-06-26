using NSI.Domain.Base;
using System.Collections.Generic;

namespace NSI.Domain.Notifications
{
    public class EmailMessageDomain : BaseDomain
    {
        public string From { get; set; }
        public int NotificationId { get; set; }
       
    }
}
