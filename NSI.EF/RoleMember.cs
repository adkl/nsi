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
    
    public partial class RoleMember
    {
        public int RoleMemberId { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public int UserInfoId { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}