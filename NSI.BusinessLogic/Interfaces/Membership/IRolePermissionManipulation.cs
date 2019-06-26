﻿using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Membership
{
    public interface IRolePermissionManipulation
    {
        ICollection<RolePermissionDomain> GetAllRolePermissions();
        RolePermissionDomain GetRolePermissionById(int rolePermissionId);
        int AddRolePermission(RolePermissionDomain rolePermission);
        void UpdateRolePermission(RolePermissionDomain rolePermission);
        void DeleteRolePermissionByRoleId(int roleId);
        void DeleteRolePermissionByPermissionId(int permissionId);
       
    }
}
