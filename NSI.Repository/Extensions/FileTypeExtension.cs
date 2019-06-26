using NSI.Domain.DocumentManagement;
using NSI.EF;
using System.Linq;

namespace NSI.Repository.Extensions
{
    public static class FileTypeExtension
    {
        public static FileTypeDomain ToDomainModel(this FileType obj)
        {
            return obj == null ? null : new FileTypeDomain()
            {
                FileTypeId = obj.FileTypeId,
                Name = obj.Name,
                Code = obj.Code,
                Extension = obj.Extension,
                Documents = obj.Document.Select(x => x.ToDomainModel()).ToList(),
          };
        }

        public static FileType FromDomainModel(this FileType obj, FileTypeDomain domain)
        {
            if (obj == null)
            {
                obj = new FileType();
            }

            obj.FileTypeId = domain.FileTypeId;
            obj.Name = domain.Name;
            obj.Code = domain.Code;
            obj.Extension = domain.Extension;
            return obj;

        }
    }
}
