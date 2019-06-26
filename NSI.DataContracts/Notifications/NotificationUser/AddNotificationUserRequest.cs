using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.NotificationUser {
    public class AddNotificationUserRequest : BaseRequest {
        /// <summary>
        /// NotificationUser add request model
        /// </summary>

        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }
    }
}
