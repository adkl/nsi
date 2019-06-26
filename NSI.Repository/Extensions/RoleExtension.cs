using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class RoleExtension
    {

        public static RoleDomain ToDomainModel(this Role obj)
        {
            return obj == null ? null : new RoleDomain()
            {
                Id = obj.RoleId,
                TenantId = obj.TenantId,
                Name = obj.Name,
                IsActive = obj.IsActive,
                ManipulationLogId = obj.ManipulationLogId,
                RolePermission = obj.RolePermission.Select(rp => rp.ToDomainModel()).ToList()
            };
        }

        public static Role FromDomainModel(this Role obj, RoleDomain roleDomain)
        {         
            if (obj == null)
            {
                obj = new Role();
            }
            obj.RoleId = roleDomain.Id;
            obj.TenantId = roleDomain.TenantId;
            obj.Name = roleDomain.Name;
            obj.IsActive = roleDomain.IsActive;
            obj.ManipulationLogId = roleDomain.ManipulationLogId;

            return obj;         
        }
        
    }
}
