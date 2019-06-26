using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Notifications
{
    public interface IWebNotificationRepository
    {
        ICollection<WebNotificationDomain> GetAll();
        WebNotificationDomain GetById(int webNotificationId);
        int Add(WebNotificationDomain domain);
        void Update(WebNotificationDomain domain);
        void Update(ICollection<WebNotificationDomain> domains);
    }
}
