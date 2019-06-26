using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;

namespace NSI.BusinessLogic.Interfaces.Notifications {
    public interface INotificationManipulation {
        ICollection<NotificationDomain> GetAllNotifications();
        NotificationDomain GetNotificationById(int notificationId);
        ICollection<NotificationDomain> GetNotificationByExternalId(Guid externalId);
        ICollection<NotificationDomain> GetNotificationByCreatedDate(DateTime createdDate);
        int AddNotification(NotificationDomain notification);
        void UpdateNotification(NotificationDomain notification);
        void DeleteNotification(int notificationId);
    }
}
