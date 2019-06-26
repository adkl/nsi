using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class PermissionExtension
    {
        public static PermissionDomain ToDomainModel(this Permission obj)
        {
            return obj == null ? null : new PermissionDomain()
            {
                Id = obj.PermissionId,
                Code = obj.Code,
                IsActive = obj.IsActive,
                //Module = obj.Module.ToDomainModel()
                ModuleId = obj.ModuleId
            };
        }

        public static Permission FromDomainModel(this Permission obj, PermissionDomain domain)
        {
            if (obj == null)
            {
                obj = new Permission();
            }

            obj.PermissionId = domain.Id;
            obj.IsActive = domain.IsActive;
            obj.ModuleId = domain.ModuleId;
            obj.Code = domain.Code;

            return obj;
        }
    }
}
