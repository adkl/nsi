using NSI.Domain.TemplateManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class TemplateVersionExtension
    {
        public static TemplateVersionDomain ToDomainModel(this TemplateVersion obj)
        {
            return obj == null ? null : new TemplateVersionDomain()
            {
                Id = obj.TemplateVersionId,
                IsDefault = obj.IsDefault,
                DateCreated = obj.DateCreated,
                TemplateId = obj.TemplateId,
                Content = obj.Content,
                Code = obj.Code
            };
        }

        public static TemplateVersion FromDomainModel(this TemplateVersion obj, TemplateVersionDomain domain)
        {
            if (obj == null)
            {
                obj = new TemplateVersion();
            }

            obj.TemplateVersionId = domain.Id;
            obj.IsDefault = domain.IsDefault;
            obj.DateCreated = domain.DateCreated;
            obj.TemplateId = domain.TemplateId;
            obj.Content = domain.Content;
            obj.Code = domain.Code;
            return obj;
        }

        public static ICollection<TemplateVersionDomain> ToDomainModelsCollection(this ICollection<TemplateVersion> templateVersionsDb)
        {
            return templateVersionsDb == null ? 
                null : templateVersionsDb.Select(templateVersionDb => {
                    TemplateVersionDomain tvd = templateVersionDb.ToDomainModel();
                    return tvd;
                    }).ToList();

        }

        public static ICollection<TemplateVersion> FromDomainModelsCollection(this ICollection<TemplateVersion> templateVersionsDb,
            ICollection<TemplateVersionDomain> templateVersions)
        {
            if (templateVersionsDb == null)
            {
                templateVersionsDb = new List<TemplateVersion>();
            }
            templateVersionsDb = templateVersions.Select(templateVersion => 
                new TemplateVersion().FromDomainModel(templateVersion)).ToList();
            return templateVersionsDb;
        }
    }
}
