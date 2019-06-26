using NSI.Domain.Notifications;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions.Notifications
{
    public static class NotificationStatusExtension
    {
        public static NotificationStatusDomain ToDomainModel(this NotificationStatus obj)
        {
            return obj == null ? null : new NotificationStatusDomain()
            {
                Id = obj.NotificationStatusId,
                Code = obj.Code,
                Name = obj.Name,   
            };
        }

        public static NotificationStatus FromDomainModel(this NotificationStatus obj, NotificationStatusDomain domain)
        {
            if(obj == null)
            {
                obj = new NotificationStatus();
            }

            obj.NotificationStatusId = domain.Id;
            obj.Code = domain.Code;
            obj.Name = domain.Name;
          
            return obj;
        }
    }
}
