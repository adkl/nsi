using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Membership
{
    public class ModuleDomain : BaseDomain
    {
        public string Name { get; set; } 
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
