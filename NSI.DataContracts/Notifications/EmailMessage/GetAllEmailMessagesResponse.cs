using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.DataContracts.Notifications.EmailMessage
{
    public class GetAllEmailMessagesResponse : BaseResponse<ICollection<EmailMessageDomain>>
    {
    }
}
