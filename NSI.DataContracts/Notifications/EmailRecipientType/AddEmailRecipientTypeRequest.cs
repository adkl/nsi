using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.EmailRecipientType
{
    public class AddEmailRecipientTypeRequest : BaseRequest
    {
        /// <summary>
        /// Email recipient type model
        /// </summary>
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
