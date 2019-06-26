using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.DataContracts.Notifications.EmailRecipientType
{
    public class GetAllEmailRecipientTypesResponse : BaseResponse<ICollection<EmailRecipientTypeDomain>>
    {
    }
}
