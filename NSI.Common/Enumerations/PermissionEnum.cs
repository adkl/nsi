using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Enumerations
{
    public enum PermissionEnum
    {
        /* Permissions */
        CanSeePermissions,
        CanUpdatePermissions,

        /* Modules */
        CanSeeModules, 
        CanAssignModulesToTenant,
        CanUpdateModules
    }
}
