using NSI.Domain.Base;
using NSI.Domain.DeviceManagement;
using System;

namespace NSI.Domain.DevicePing
{
    public class DevicePropertyValue : BaseAuditDomain
    {
        public DateTime DateCreated { get; set; }
        public int DeviceId { get; set; }
        public DeviceDomain Device { get; set; }
        public int PropertyId { get; set; }
        public PropertyDomain Property { get; set; }
        public int DevicePingId { get; set; }
        public string Value { get; set; }
    }
}
