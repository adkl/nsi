using NSI.DataContracts.Base;
using System;

namespace NSI.DataContracts.Notification {
    public class AddNotificationRequest : BaseRequest {
        /// <summary>
        /// Notification add request model
        /// </summary>
      
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ExternalId { get; set; }
        public int NotificationStatusId { get; set; }
        public int NotificationTypeId { get; set; }
    }
}
