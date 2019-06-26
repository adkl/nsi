using NSI.DataContracts.Base;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.WebNotification
{
    public class GetAllWebNotificationResponse: BaseResponse<ICollection<WebNotificationDomain>>
    {
    }
}
