using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class LanguageExtension
    {
        public static LanguageDomain ToDomainModel(this Language obj)
        {
            return obj == null ? null : new LanguageDomain()
            {
                Id = obj.LanguageId,
                IsoCode = obj.ISOCode,
                Name = obj.Name,
                IsActive=obj.IsActive,
                IsDefault=obj.IsDefault
            };
        }
        public static Language FromDomainModel(this Language obj, LanguageDomain domain)
        {
            if (obj == null)
            {
                obj = new Language();
            }

            obj.LanguageId = domain.Id;
            obj.IsActive = domain.IsActive;
            obj.ISOCode = domain.IsoCode;
            obj.IsDefault = domain.IsDefault;
            obj.Name = domain.Name;
            return obj;
        }
    }
}
