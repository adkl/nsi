using NSI.Domain.DocumentManagement;
using System.Collections.Generic;

namespace NSI.DocumentRepository.Interfaces
{
    public interface IFileTypeManipulation
    {
        ICollection<FileTypeDomain> GetAllFileTypes();
        FileTypeDomain GetFileTypeById(int id);
        int CreateFileType(FileTypeDomain fileType);
        int UpdateFileType(FileTypeDomain FileType);
        bool DeleteFileType(int FileTypeId);
        string GetFileExtensionById(int fileTypeId);
        int GetFileIdByExtension(string extension);

    }
}
