using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.DeviceManagement
{
   public class PropertyDomain 
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

}
