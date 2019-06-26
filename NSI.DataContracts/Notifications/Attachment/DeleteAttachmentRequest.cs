using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.Attachment
{
    public class DeleteAttachmentRequest : BaseRequest
    {
        /// <summary>
        /// Attachment model
        /// </summary>
        public AttachmentDomain Data { get; set; }
    }
}
