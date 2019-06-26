using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.EmailRecipient
{
    public class AddEmailRecipientRequest : BaseRequest
    {
        /// <summary>
        /// Email recipient address
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Id of email message
        /// </summary>
        public int EmaiMessagelId { get; set; }
        /// <summary>
        /// Type of email recipient
        /// </summary>
        public int EmailRecipientTypeId { get; set; }
    }
}
