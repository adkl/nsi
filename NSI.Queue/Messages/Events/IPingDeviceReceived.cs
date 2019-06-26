using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Queue.Messages.Events
{
    public interface IPingDeviceReceived
    {
        Guid PingDeviceId { get; }
        int TenantId { get; set; }
        int DeviceId { get; set; }
        int ActionId { get; set; }
        string Content { get; }
        DateTime Timestamp { get; }
    }
}
