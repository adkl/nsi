using NSI.Common.Exceptions;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Models
{
    public class SortCriteria
    {
        public string ColumnName { get; set; }
        public SortOrder Order { get; set; }
        public int Priority { get; set; }
    }

    public static class SortCriteriaExtension
    {
        /// <summary>
        /// Performs sanity validation of sort criteria. Does not validate if sort criteria object is null.
        /// </summary>
        /// <param name="sortCriteria"><see cref="SortCriteria"/></param>
        public static void ValidateSortCriteria(this SortCriteria sortCriteria)
        {
            if (sortCriteria != null && string.IsNullOrWhiteSpace(sortCriteria.ColumnName))
            {
                    throw new NsiArgumentException(ExceptionMessages.SortColumnNameEmpty);               
            }
        }
    }
}
