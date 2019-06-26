using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.NotificationUser {
    public class UpdateNotificationUserRequest : BaseRequest {
        /// <summary>
        /// NotificationUser update request model
        /// </summary>
        /// 
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }
}
