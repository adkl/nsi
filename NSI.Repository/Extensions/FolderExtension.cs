using NSI.Domain.TemplateManagement;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class FolderExtension
    {
        public static FolderDomain ToDomainModel(this Folder obj)
        {
            return obj == null ? null : new FolderDomain()
            {
                Id = obj.FolderId,
                Name = obj.Name,
                DateCreated = obj.DateCreated,
                ParentFolderId = obj.ParentFolderId
            };
        }

        public static Folder FromDomainModel(this Folder obj, FolderDomain domain)
        {
            if (obj == null)
            {
                obj = new Folder();
            }

            obj.FolderId = domain.Id;
            obj.Name = domain.Name;
            obj.DateCreated = domain.DateCreated;
            obj.ParentFolderId = domain.ParentFolderId;
            return obj;
        }
    }
}
