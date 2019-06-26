using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.DataContracts.Notifications.EmailRecipient
{
    public class SearchEmailRecipientResponse : BaseResponse<ICollection<EmailRecipientDomain>>
    {
    }
}
