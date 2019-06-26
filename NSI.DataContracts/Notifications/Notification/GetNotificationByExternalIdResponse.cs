using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.DataContracts.Notification {
    public class GetNotificationByExternalIdResponse : BaseResponse<ICollection<NotificationDomain>> {
    }
}
