using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.Attachment
{
    public class AddAttachmentRequest : BaseRequest
    {
        /// <summary>
        /// Attachment model
        /// </summary>
        public byte[] File { get; set; }
    }
}
