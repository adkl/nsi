using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;

namespace NSI.Repository.Interfaces.Notifications {
    public interface INotificationRepository {
        ICollection<NotificationDomain> GetAll();
        NotificationDomain GetById(int notificationId);
        ICollection<NotificationDomain> GetByExternalId(Guid externalId);
        ICollection<NotificationDomain> GetByCreatedDate(DateTime dateCreated);
        int Add(NotificationDomain notification);
        void Update(NotificationDomain notification);
        void Delete(int notificationId);
    }
}
