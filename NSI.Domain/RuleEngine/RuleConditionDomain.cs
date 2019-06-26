using NSI.Domain.Base;
using NSI.Domain.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.RuleEngine
{
    public class RuleConditionDomain : BaseDomain
    {
        public int RuleConditionId { get; set; }
        public string ComparisonOperator { get; set; }
        public string ComparisonValue { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public DeviceDomain Device { get; set; }
        public PropertyDomain Property { get; set; }
    }
}
