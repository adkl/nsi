//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NSI.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkOrder
    {
        public int WorkOrderId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public int TenantId { get; set; }
        public int IncidentId { get; set; }
        public int IncidentSettlementId { get; set; }
    
        public virtual Incident Incident { get; set; }
        public virtual IncidentSettlement IncidentSettlement { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}