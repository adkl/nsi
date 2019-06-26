using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class TenantExtension
    {
        public static TenantDomain ToDomainModel(this Tenant obj)
        {
            return obj == null ? null : new TenantDomain()
            {
                Id = obj.TenantId,
                Identifier = obj.Identifier,
                Name = obj.Name,
                DefaultLanguageId = obj.LanguageId,
                IsActive = obj.IsActive
            };
        }

        public static Tenant FromDomainModel (this Tenant obj, TenantDomain domain)
        {
            if (obj == null)
            {
                obj = new Tenant();
            }

            obj.TenantId = domain.Id;
            obj.Name = domain.Name;
            obj.Identifier = domain.Identifier;
            obj.LanguageId = domain.DefaultLanguageId;
            obj.IsActive = domain.IsActive;

            return obj;
        }
    }
}
