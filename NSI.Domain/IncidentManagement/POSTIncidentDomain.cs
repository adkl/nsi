using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.IncidentManagement
{
    public class POSTIncidentDomain : BaseDomain
    {
        public int IncidentId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int IncidentStatus { get; set; }
        public int DeviceId { get; set; }
        public int Priority { get; set; }
        public int IncidentType { get; set; }
        public int ReporterId { get; set; }
        public int AssigneeId { get; set; }
    }
}
