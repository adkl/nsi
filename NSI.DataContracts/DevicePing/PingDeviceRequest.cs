using NSI.DataContracts.Base;
using NSI.Domain.DevicePing;

namespace NSI.DataContracts.DevicePing
{
    public class PingDeviceRequest: BaseRequest
    {
        public PingDeviceDomain pingDeviceDomain { get; set; }
    }
}
