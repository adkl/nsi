using NSI.Common.Models;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface INotificationStatusRepository
    {
        ICollection<NotificationStatusDomain> GetAll();
        NotificationStatusDomain GetById(int id);
        NotificationStatusDomain GetByName(String name);
        NotificationStatusDomain GetByCode(String code);
        int AddNotificationStatus(NotificationStatusDomain notificationStatus);
        void UpdateNotificationStatus(NotificationStatusDomain notificationStatus);
        void DeleteNotificationStatus(NotificationStatusDomain notificationStatus);
        ICollection<NotificationStatusDomain> SearchNotificationStatus(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
    }
}
