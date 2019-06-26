using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSI.Api.ViewModels
{
    /// <summary>
    /// List view model for permission
    /// </summary>
    public class PermissionForListViewModel
    {
        /// <summary>
        /// Permission Id
        /// </summary>
        public int PermissionId { get; set; }
        /// <summary>
        /// Permission code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Permission isActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Map from DataBase Permission
        /// </summary>
        public static PermissionForListViewModel MapFromDbObject(PermissionDomain permission)
        {
            return new PermissionForListViewModel
            {
                PermissionId = permission.Id,
                Code = permission.Code,
                IsActive = permission.IsActive

            };
        }
    }
}