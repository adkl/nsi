using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.DataContracts.Notifications.Attachment
{
    public class GetAllAttachmentsResponse : BaseResponse<ICollection<AttachmentDomain>>
    {
    }
}
