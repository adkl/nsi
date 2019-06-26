using NSI.Domain.Base;
using NSI.Domain.DeviceManagement;
using System;
using System.Collections.Generic;

namespace NSI.Domain.DevicePing
{
    public class DevicePingDomain : BaseAuditDomain
    {
        public DateTime DateCreated { get; set; }
        public int? RuleId { get; set; }
        public int ActionId { get; set; }
        public ActionDomain Action { get; set; }
        public int DeviceId { get; set; }
        public DeviceDomain Device { get; set; }
        public List<DevicePropertyValue> DevicePropertyValues { get; set; }
    }
}
