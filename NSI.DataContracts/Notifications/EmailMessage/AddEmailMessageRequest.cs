using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.EmailMessage
{
    public class AddEmailMessageRequest : BaseRequest
    {
        /// <summary>
        /// Email Message model
        /// </summary>
        public string From { get; set; }
        public int NotificationId { get; set; }
    }
}
