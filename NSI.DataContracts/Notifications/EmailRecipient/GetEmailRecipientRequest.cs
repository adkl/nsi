using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.EmailRecipient
{
    public class GetEmailRecipientRequest : BaseRequest
    {
        /// <summary>
        /// Email recipient ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
