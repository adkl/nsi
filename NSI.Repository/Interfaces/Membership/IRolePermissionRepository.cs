using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Membership
{
    public interface IRolePermissionRepository
    {
        ICollection<RolePermissionDomain> GetAll();
        RolePermissionDomain GetById(int rolePermissionId);
        int Add(RolePermissionDomain rolePermission);
        void Update(RolePermissionDomain rolePermission);
        void DeleteByRoleId(int roleId);
        void DeleteByPermissionId(int permissionId);
    }
}
