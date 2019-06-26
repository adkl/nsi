using NSI.Domain.DocumentManagement;
using System.Collections.Generic;

namespace NSI.Repository.Interfaces.DocumentManagement
{
    public interface IFileTypeRepository
    {
        ICollection<FileTypeDomain> GetAllFileTypes();
        FileTypeDomain GetFileTypeById(int id);
        int CreateFileType(FileTypeDomain fileType);
        int UpdateFileType(FileTypeDomain fileType);
        bool DeleteFileType(int fileTypeId);
        string GetFileExtensionById(int id);
        int GetFileIdByExtension(string extension);

    }
}
