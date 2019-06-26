using NSI.Domain.Base;
using System;

namespace NSI.Domain.Notifications {
    public class NotificationDomain : BaseDomain {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public int NotificationStatusId { get; set; }
        public int NotificationTypeId { get; set; }
    }
}
