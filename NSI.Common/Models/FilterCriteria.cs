using NSI.Common.Exceptions;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Models
{
    public class FilterCriteria
    {
        public string ColumnName { get; set; }
        public string FilterTerm { get; set; }
        public bool IsExactMatch { get; set; }
    }

    public static class FilterCriteriaExtension
    {
        /// <summary>
        /// Performs sanity validation of filter criteria. Does not validate if filter criteria object is null.
        /// </summary>
        /// <param name="filterCriteria"><see cref="FilterCriteria"/></param>
        public static void ValidateFilterCriteria(this FilterCriteria filterCriteria)
        {
            if (filterCriteria != null)
            {
                if (string.IsNullOrWhiteSpace(filterCriteria.ColumnName))
                {
                    throw new NsiArgumentException(ExceptionMessages.FilterColumnNameEmpty);
                }

                // Do not validate filter term, it can be empty and should be ignored
            }
        }
    }
}
