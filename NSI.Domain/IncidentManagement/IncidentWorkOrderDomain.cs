using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.IncidentManagement
{
    public class IncidentWorkOrderDomain : BaseDomain
    {
        public int WorkOrderId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int IncidentId { get; set; }
        public int IncidentSettlementId { get; set; }
       
    }
}
