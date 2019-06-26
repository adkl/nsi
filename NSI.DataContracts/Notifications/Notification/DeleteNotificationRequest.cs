using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notification {
    public class DeleteNotificationRequest : BaseRequest {
        /// <summary>
        /// Notification model
        /// </summary>
        public NotificationDomain Data { get; set; }
    }
}
