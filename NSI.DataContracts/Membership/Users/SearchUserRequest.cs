using NSI.Common.Interfaces;
using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Common.Models;

namespace NSI.DataContracts.Membership.Users
{
    public class SearchUserRequest : BaseRequest, ISortable, IFilterable, IPageable
    {
        /// <summary>
        /// Filter criteria
        /// </summary>
        public IList<FilterCriteria> FilterCriteria { get; set; }

        /// <summary>
        /// Sort criteria
        /// </summary>
        public IList<SortCriteria> SortCriteria { get; set; }

        /// <summary>
        /// Paging criteria
        /// </summary>
        public Paging Paging { get; set; }
    }
}
