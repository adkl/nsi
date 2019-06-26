using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Notifications
{
    public interface INotificationTypeManipulation
    {
        ICollection<NotificationTypeDomain> GetAllNotificationTypes();
        NotificationTypeDomain GetNotificationTypeById(int notificationTypeId);
        NotificationTypeDomain GetNotificationTypeByName(string notificationTypeName);
        NotificationTypeDomain GetNotificationTypeByCode(string notificationTypeCode);
        int AddNotificationType(NotificationTypeDomain notificationType);
        void UpdateNotificationType(NotificationTypeDomain notificationType);
    }
}
