using NSI.Domain.TemplateManagement;
using NSI.EF;
using System;

namespace NSI.Repository.Extensions
{
    public static class TemplateExtension
    {
        public static TemplateDomain ToDomainModel(this Template obj)
        {
            return obj == null ? null : new TemplateDomain()
            {
                Id = obj.TemplateId,
                Name = obj.Name,
                DateCreated = obj.DateCreated,
                FolderId = obj.FolderId,
                Folder = obj.Folder.ToDomainModel(),
                Versions = obj.TemplateVersion.ToDomainModelsCollection()
            };
        }

        public static Template FromDomainModel(this Template obj, TemplateDomain domain)
        {
            if (obj == null)
            {
                obj = new Template();
            }

            obj.TemplateId = domain.Id;
            obj.Name = domain.Name;
            obj.DateCreated = domain.DateCreated;
            obj.FolderId = domain.FolderId;
            return obj;
        }
    }
}
