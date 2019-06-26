using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.DeviceManagement
{
    public class UpdateDeviceDomain
    {
        public int DeviceId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int DeviceTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string DeviceImage { get; set; }
    }
}
