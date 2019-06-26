using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.EmailRecipientType{
    public class DeleteEmailRecipientTypeRequest : BaseRequest {
        /// <summary>
        /// EmailRecipientType model
        /// </summary>
        public EmailRecipientTypeDomain Data { get; set; }
    }
}
