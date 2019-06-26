using NSI.Domain.Base;

namespace NSI.Domain.Notifications {
    public class NotificationUserDomain : BaseDomain {
        public int NotificationId { get; set; }
        public int UserInfoId { get; set; }
        public int UserTenantId { get; set; }
    }
}