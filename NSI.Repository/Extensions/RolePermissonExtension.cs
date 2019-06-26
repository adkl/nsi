using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.Membership;
using NSI.EF;

namespace NSI.Repository.Extensions
{
    public static class RolePermissonExtension
    {
        public static RolePermissionDomain ToDomainModel(this RolePermission obj)
        {
            return obj == null ? null : new RolePermissionDomain()
            {
                Id = obj.RolePermissionId,
                IsActive = obj.IsActive,
                RoleId = obj.RoleId,
                //Role = obj.Role.ToDomainModel(),
                PermissionId = obj.PermissionId,
                //Permission = obj.Permission.ToDomainModel(),
                TenantId = obj.TenantId,
                Code = obj.Permission.Code
            };
        }

        public static RolePermission FromDomainModel(this RolePermission obj, RolePermissionDomain domain)
        {
            if (obj == null)
            {
                obj = new RolePermission();
            }

            obj.RolePermissionId = domain.Id;
            obj.IsActive = domain.IsActive;
            obj.RoleId = domain.RoleId;
            obj.PermissionId = domain.PermissionId;
            obj.TenantId = domain.TenantId;

            return obj;
        }
    }
}
