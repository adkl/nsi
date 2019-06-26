using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.RuleEngine
{
    public class AddRuleConditionDomain : BaseDomain
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int DeviceId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ParameterId { get; set; }

        [Required]
        [MinLength(1)]
        public string ComparisonOperator { get; set; }

        [Required]
        [MinLength(1)]
        public string ParameterValue { get; set; }
    }
}
