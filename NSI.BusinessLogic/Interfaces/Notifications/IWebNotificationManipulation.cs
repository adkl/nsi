using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Notifications
{
    public interface IWebNotificationManipulation
    {
        ICollection<WebNotificationDomain> GetAllWebNotifications();
        WebNotificationDomain GetWebNotificationById(int webNotificationId);
        int AddWebNotification(WebNotificationDomain webNotification);
        void UpdateWebNotification(WebNotificationDomain webNotification);
        void UpdateWebNotificationsSeen(ICollection<WebNotificationDomain> webNotifications);
    }
}
