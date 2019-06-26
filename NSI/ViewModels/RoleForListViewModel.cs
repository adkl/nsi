using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSI.Api.ViewModels
{
    /// <summary>
    /// List view model for role
    /// </summary>
    public class RoleForListViewModel
    {
        /// <summary>
        /// Role Id
        /// </summary>
        public int RoleInfoId { get; set; }
        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Role is active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Manipulation log Id
        /// </summary>
        public int ManipulationLogId { get; set; }
        /// <summary>
        /// Role permissions
        /// </summary>
        public IList<Permission> Permissions { get; set; }

        /// <summary>
        /// Permission
        /// </summary>
        public class Permission
        {
            /// <summary>
            /// Permission Id
            /// </summary>
            public int PermissionId { get; set; }
            /// <summary>
            /// Permission code
            /// </summary>
            public String Code { get;set; }

            /// <summary>
            /// Map from DataBase RolePermission
            /// </summary>
            public static Permission MapFromDbPermissionObject(RolePermissionDomain rm)
            {
                return new Permission
                {
                    PermissionId = rm.PermissionId,
                    Code = rm.Code
                };
            }
        }

        /// <summary>
        /// Map from DataBase Role
        /// </summary>
        public static RoleForListViewModel MapFromDbObject(RoleDomain role)
        {
            return new RoleForListViewModel
            {
                RoleInfoId = role.Id,
                Name = role.Name,
                IsActive = role.IsActive,
                ManipulationLogId = role.ManipulationLogId,
                Permissions = role.RolePermission?.Select(r => Permission.MapFromDbPermissionObject(r)).ToList()
            };
        }

    }
}