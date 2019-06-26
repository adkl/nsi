using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System.Collections.Generic;

namespace NSI.DataContracts.Notifications.NotificationUser {
    public class GetNotificationUserByNotificationIdResponse : BaseResponse<ICollection<NotificationUserDomain>> {
    }
}