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
    
    public partial class DevicePing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DevicePing()
        {
            this.DevicePropertyValue = new HashSet<DevicePropertyValue>();
        }
    
        public int DevicePingId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int TenantId { get; set; }
        public Nullable<int> RuleId { get; set; }
        public int ActionId { get; set; }
        public int DeviceId { get; set; }
    
        public virtual Action Action { get; set; }
        public virtual Device Device { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DevicePropertyValue> DevicePropertyValue { get; set; }
        public virtual Rule Rule { get; set; }
    }
}
