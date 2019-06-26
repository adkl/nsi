using NSI.Common.Exceptions;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Models
{
    public class Paging
    {
        public int Page { get; set; }
        public int TotalRecords { get; set; }
        public int RecordsPerPage { get; set; }
        public int Pages
        {
            get
            {
                var pages = 1;
                if (RecordsPerPage > 0)
                {
                    pages = (TotalRecords / RecordsPerPage) + (TotalRecords % RecordsPerPage > 0 ? 1 : 0);
                }

                return pages;
            }
            set { }
            
        }
    }

    public static class PagingExtension
    {
        /// <summary>
        /// Performs sanity validation of paging criteria. Does not validate if paging object is null.
        /// </summary>
        /// <param name="paging"><see cref="Paging"/></param>
        public static void ValidatePagingCriteria(this Paging paging)
        {
            if (paging != null)
            {
                if (paging.Page <= 0)
                {
                    throw new NsiArgumentException(ExceptionMessages.InvalidPage);
                }

                if (paging.RecordsPerPage <= 0)
                {
                    throw new NsiArgumentException(ExceptionMessages.InvalidRecordsPerPage);
                }
            }
        }
    }
}
