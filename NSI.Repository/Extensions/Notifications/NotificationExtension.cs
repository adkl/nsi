using NSI.Domain.Notifications;
using NSI.EF;
using System;

namespace NSI.Repository.Extensions.Notifications {
    public static class NotificationExtension {
        public static NotificationDomain ToDomainModel(this Notification obj) {
            return obj == null ? null : new NotificationDomain() {
                Id = obj.NotificationId,
                Title = obj.Title,
                Content = obj.Content,
                ExternalId = obj.ExternalId,
                DateCreated = obj.DateCreated,
                DateModified = obj.DateModified,
                NotificationStatusId = obj.NotificationStatusId,
                NotificationTypeId = obj.NotificationTypeId
            };
        }

        public static Notification AddFromDomainModel(this Notification obj, NotificationDomain domain) {
            if (obj == null) {
                obj = new Notification();
            }

            obj.NotificationId = domain.Id;
            obj.Title = domain.Title;
            obj.Content = domain.Content;
            obj.ExternalId = domain.ExternalId;
            obj.DateCreated = DateTime.Now;
            obj.DateModified = null;
            obj.NotificationTypeId = domain.NotificationTypeId;
            obj.NotificationStatusId = domain.NotificationStatusId;
            return obj;
        }

        public static Notification UpdateFromDomainModel(this Notification obj, NotificationDomain domain) {
         
            obj.DateModified = DateTime.Now;
            obj.Title = domain.Title;
            obj.Content = domain.Content;
            obj.ExternalId = domain.ExternalId;
            obj.NotificationTypeId = domain.NotificationTypeId;
            obj.NotificationStatusId = domain.NotificationStatusId;
            return obj;
        }
    }

}
