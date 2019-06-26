using NSI.Domain.Base;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.IncidentManagement
{
    public class IncidentDomain : BaseDomain
    {
        public int IncidentId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public TenantDomain Tenant { get; set; }
        public IncidentStatusDomain IncidentStatus { get; set; }
        public DeviceDomain Device { get; set; }
        public IncidentPriorityDomain Priority { get; set; }
        public IncidentTypeDomain IncidentType { get; set; }
        public UserDomain Reporter { get; set; }
        public UserDomain Assignee { get; set; }
    }
}
