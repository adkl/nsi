using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.EmailRecipientType
{
    public class GetEmailRecipientTypeRequest : BaseRequest
    {
        /// <summary>
        /// Email recipient type ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
