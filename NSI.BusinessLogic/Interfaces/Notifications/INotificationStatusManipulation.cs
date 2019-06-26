using NSI.Common.Models;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Notifications
{
    public interface INotificationStatusManipulation
    {
        int AddNotificationStatus(NotificationStatusDomain notificationStatus);
        void UpdateNotificationStatus(NotificationStatusDomain notificationStatusDomain);
        void DeleteNotificationStatus(int Id);
        ICollection<NotificationStatusDomain> GetAllNotificationStatuses();
        ICollection<NotificationStatusDomain> SearchNotificationStatus(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);
        NotificationStatusDomain GetNotificationStatusById(int id);
        NotificationStatusDomain GetNotificationStatusByName(String name);
        NotificationStatusDomain GetNotificationStatusByCode(String code);
    }
}
