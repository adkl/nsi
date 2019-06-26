using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.EmailMessage
{
    public class UpdateEmailMessageRequest : BaseRequest
    {
        /// <summary>
        /// Email Message model
        /// </summary>
        public int Id { get; set; }
        public string From { get; set; }
        public int NotificationId { get; set; }
    }
}
