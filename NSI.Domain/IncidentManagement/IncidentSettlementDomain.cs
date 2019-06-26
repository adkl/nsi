using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.IncidentManagement
{
    public class IncidentSettlementDomain : BaseDomain
    {
        public int IncidentSettlementId { get; set; }
        public string Description { get; set; }
        public string FullText { get; set; }
        public Nullable<System.DateTime> DateSettled { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int IncidentStatusId { get; set; }
    }
}
