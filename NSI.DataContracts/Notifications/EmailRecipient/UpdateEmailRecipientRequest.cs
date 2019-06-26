using NSI.DataContracts.Base;
using NSI.Domain.Notifications;

namespace NSI.DataContracts.Notifications.EmailRecipient
{
    public class UpdateEmailRecipientRequest : BaseRequest
    {
        /// <summary>
        /// Email recipient model
        /// </summary>
        public int Id { get; set; }
        public string EmailAddress { get; set; }        

    }
}
