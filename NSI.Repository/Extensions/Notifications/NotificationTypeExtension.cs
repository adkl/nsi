using NSI.Domain.Notifications;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions.Notifications
{
    public static class NotificationTypeExtension
    {
        public static NotificationTypeDomain ToDomainModel(this NotificationType obj)
        {
            return obj == null ? null : new NotificationTypeDomain()
            {
                Id = obj.NotificationTypeId,
                Name = obj.Name,
                Code = obj.Code
            };
        }

        public static NotificationType FromDomainModel(this NotificationType obj, NotificationTypeDomain domain)
        {
            if(obj == null)
            {
                obj = new NotificationType();
            }

            obj.NotificationTypeId = domain.Id;
            obj.Name = domain.Name;
            obj.Code = domain.Code;

            return obj;
        }
    }
}
