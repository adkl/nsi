using NSI.Domain.DevicePing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Queue.Messages.Events
{
    public interface IDevicePingReceived
    {
        Guid MessageId { get; }
        DateTime Timestamp { get; }
        DevicePingDomain DevicePing { get; }
    }
}
