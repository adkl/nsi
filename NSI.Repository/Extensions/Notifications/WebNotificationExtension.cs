using NSI.Domain.Notifications;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions.Notifications
{
    public static class WebNotificationExtension
    {
        public static WebNotificationDomain ToDomainModel(this WebNotification obj, NotificationDomain domain)
        {
            return obj == null ? null : new WebNotificationDomain()
            {
                Id = obj.WebNotificationId,
                Seen = obj.Seen,
                DateSeen = obj.DateSeen,
                NotificationId = obj.NotificationId,
                UserInfoId = obj.UserInfoId,
                UserTenantId = obj.UserTenantId,
                Title = domain.Title,
                Content = domain.Content,
                ExternalId = domain.ExternalId,
                DateCreated = domain.DateCreated,
                DateModified = domain.DateModified,
                NotificationStatusId = domain.NotificationStatusId,
                NotificationTypeId = domain.NotificationTypeId
            };
        }

        public static WebNotification FromDomainModel(this WebNotification obj, WebNotificationDomain domain)
        {
            if (obj == null)
            {
                obj = new WebNotification();
            }

            obj.WebNotificationId = domain.Id;
            obj.Seen = domain.Seen;
            obj.DateSeen = domain.DateSeen;
            obj.NotificationId = domain.NotificationId;
            obj.UserInfoId = domain.UserInfoId;
            obj.UserTenantId = domain.UserTenantId;

            return obj;
        }
    }
}
