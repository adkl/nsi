using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.BusinessLogic.Interfaces.Notifications {
    public interface INotificationUserManipulation {
        ICollection<NotificationUserDomain> GetAllNotificationUsers();
        NotificationUserDomain GetNotificationUserById(int notificationUserId);
        ICollection<NotificationUserDomain> GetNotificationUserByNotificationId(int notificationId);
        ICollection<NotificationUserDomain> GetNotificationUserByUserId(int userId);
        int AddNotificationUser(NotificationUserDomain notificationUser);
        void UpdateNotificationUser(NotificationUserDomain notificationUser);
        void DeleteNotificationUser(int notificationUserId);
    }
}