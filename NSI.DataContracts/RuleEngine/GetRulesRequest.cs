using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Common.Interfaces;
using NSI.Common.Models;
using NSI.DataContracts.Base;

namespace NSI.DataContracts.RuleEngine
{
    public class GetRulesRequest : BaseRequest, IFilterable, IPageable
    {
        // <summary>
        /// Filter criteria
        /// </summary>
        public IList<FilterCriteria> FilterCriteria { get; set; }

        /// <summary>
        /// Paging criteria
        /// </summary>
        public Paging Paging { get; set; }
    }
}
