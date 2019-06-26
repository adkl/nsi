using NSI.Domain.Base;

namespace NSI.Domain.DevicePing
{
    public class PingDeviceDomain : BaseAuditDomain
    {
        public int DeviceId { get; set; }
        public int ActionId { get; set; }
        public string Content { get; set; }
    }
}
