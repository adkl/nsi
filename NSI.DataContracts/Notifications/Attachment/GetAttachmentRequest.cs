using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.Attachment
{
    public class GetAttachmentRequest : BaseRequest
    {
        /// <summary>
        /// Attachment Id for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
