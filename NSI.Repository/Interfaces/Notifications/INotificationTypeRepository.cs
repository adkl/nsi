using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface INotificationTypeRepository
    {
        ICollection<NotificationTypeDomain> GetAll();
        NotificationTypeDomain GetById(int notificationTypeId);
        NotificationTypeDomain GetByName(string notificationTypeName);
        NotificationTypeDomain GetByCode(string notificationTypeCode);
        ICollection<NotificationTypeDomain> GetAllByCode(string notificationTypeCode);
        int Add(NotificationTypeDomain domain);
        void Update(NotificationTypeDomain domain);
    }
}
