using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Membership
{
    public class LanguageDomain : BaseDomain
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
