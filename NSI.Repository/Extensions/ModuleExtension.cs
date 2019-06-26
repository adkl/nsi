using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class ModuleExtension
    {
        public static ModuleDomain ToDomainModel(this Module obj)
        {
            return obj == null ? null : new ModuleDomain()
            {
                Id = obj.ModuleId,
                Code = obj.Code,
                IsActive = obj.IsActive != 0,
                Name = obj.Name
            };
        }

        public static Module FromDomainModel(this Module obj, ModuleDomain domain)
        {
            if (obj == null)
            {
                obj = new Module();
            }

            obj.ModuleId = domain.Id;
            obj.Code = domain.Code;
            obj.Name = domain.Name;
            obj.IsActive = domain.IsActive ? 1 : 0;
            return obj;
        }
    }
}
