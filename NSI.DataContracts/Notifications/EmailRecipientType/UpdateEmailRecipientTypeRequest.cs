using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.EmailRecipientType
{
    public class UpdateEmailRecipientTypeRequest : BaseRequest
    {
        /// <summary>
        /// Email recipient type model
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }        

    }
}
