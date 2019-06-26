using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.DeviceManagement
{
    public class DeviceTypeDomain : BaseDomain
    {
        public int DeviceTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public List<PropertyDomain> Properties { get; set; }
        public List<ActionDomain> Actions { get; set; }
    }
}
