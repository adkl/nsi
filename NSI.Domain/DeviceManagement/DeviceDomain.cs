using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.DeviceManagement
{
    public class DeviceDomain : BaseAuditDomain
    {
        public int DeviceId { get; set; }
        public Guid Identifier { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int DeviceTypeId { get; set; }
        public int DeviceStatusId { get; set; }
        public DeviceTypeDomain DeviceType { get; set; }
        public DeviceStatusDomain DeviceStatus { get; set; }
        public string DeviceImage { get; set; }
    }
}
