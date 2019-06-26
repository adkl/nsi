using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.EmailMessage
{
    public class GetEmailMessageRequest : BaseRequest
    {
        /// <summary>
        /// Email Message Id for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
