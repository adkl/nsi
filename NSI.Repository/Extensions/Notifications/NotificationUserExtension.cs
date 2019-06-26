using NSI.Domain.Notifications;
using NSI.EF;

namespace NSI.Repository.Extensions.Notifications {
    public static class NotificationUserExtenstion {
        public static NotificationUserDomain ToDomainModel(this NotificationUser obj) {
            return obj == null ? null : new NotificationUserDomain() {
                Id = obj.NotificationUserId,
                NotificationId = obj.NotificationId,
                UserInfoId = obj.UserInfoId,
                UserTenantId = obj.UserTenantId,
            };
        }

        public static NotificationUser FromDomainModel(this NotificationUser obj, NotificationUserDomain domain) {
            if (obj == null) {
                obj = new NotificationUser();
            }

            obj.NotificationUserId = domain.Id;
            obj.NotificationId = domain.NotificationId;
            obj.UserInfoId = domain.UserInfoId;
            obj.UserTenantId = domain.UserTenantId;

            return obj;
        }
    }
}
