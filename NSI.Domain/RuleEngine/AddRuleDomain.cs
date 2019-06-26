using NSI.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NSI.Domain.RuleEngine
{
    public class AddRuleDomain : BaseDomain
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 3)]
        public string Description { get; set; }
        
        [Required]
        public List<AddRuleConditionDomain> Conditions { get; set; }

        [Required]
        public List<AddRuleActionDomain> Actions { get; set; }
    }
}
