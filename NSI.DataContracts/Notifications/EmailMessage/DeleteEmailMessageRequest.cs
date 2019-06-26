using NSI.DataContracts.Base;

namespace NSI.DataContracts.Notifications.EmailMessage
{
    public class DeleteEmailMessageRequest : BaseRequest
    {
        /// <summary>
        /// Email Message Id for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
