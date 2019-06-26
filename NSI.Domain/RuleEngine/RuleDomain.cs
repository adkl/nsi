using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.RuleEngine
{
    public class RuleDomain : BaseDomain
    {
        public int RuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public ICollection<RuleConditionDomain> Conditions { get; set; }
        public ICollection<RuleActionDomain> Actions { get; set; }
    }
}
