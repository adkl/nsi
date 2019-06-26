using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.EmailRecipient{
    public class DeleteEmailRecipientRequest : BaseRequest {
        /// <summary>
        /// EmailRecipient model
        /// </summary>
        public EmailRecipientDomain Data { get; set; }
    }
}
