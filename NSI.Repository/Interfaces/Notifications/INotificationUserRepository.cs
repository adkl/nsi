using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.Repository.Interfaces.Notifications {
    public interface INotificationUserRepository {
        ICollection<NotificationUserDomain> GetAll();
        NotificationUserDomain GetById(int notificationUserId);
        ICollection<NotificationUserDomain> GetByNotificationId(int notificationId);
        ICollection<NotificationUserDomain> GetByUserId(int userId);
        int Add(NotificationUserDomain notificationUser);
        void Update(NotificationUserDomain notificationUser);
        void Delete(int notificationUserId);
    }
}