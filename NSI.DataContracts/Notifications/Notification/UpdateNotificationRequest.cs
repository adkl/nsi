using NSI.DataContracts.Base;
using System;

namespace NSI.DataContracts.Notification {
    public class UpdateNotificationRequest : BaseRequest {
        /// <summary>
        /// Notification update request model
        /// </summary>
        /// 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ExternalId { get; set; }
        public int NotificationStatusId { get; set; }
        public int NotificationTypeId { get; set; }
    }
}
