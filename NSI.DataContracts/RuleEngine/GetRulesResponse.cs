using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Common.Interfaces;
using NSI.Common.Models;
using NSI.DataContracts.Base;
using NSI.Domain.RuleEngine;

namespace NSI.DataContracts.RuleEngine
{
    public class GetRulesResponse : BaseResponse<ICollection<RuleDomain>>, IPageable
    {
        /// <summary>
        /// Paging criteria
        /// </summary>
        public Paging Paging { get; set; }
    }
}
