using NSI.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Interfaces
{
    public interface ISortable
    {
        IList<SortCriteria> SortCriteria { get; set; }
    }
}
