using NSI.Common.Interfaces;
using NSI.Common.Models;
using NSI.DataContracts.Base;
using System.Collections.Generic;
namespace NSI.DataContracts.Documents
{
    public class SearchRequest : BaseRequest, ISortable, IFilterable, IPageable
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
