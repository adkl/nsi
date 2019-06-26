
using NSI.Common.Exceptions;
using NSI.Common.Resources.DocumentManagement;
using NSI.DocumentRepository.Interfaces;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System.Collections.Generic;

namespace NSI.DocumentRepository.Implementations
{
    public class FileTypeManipulation : IFileTypeManipulation
    {
        private readonly IFileTypeRepository _fileTypeRepository;

        public FileTypeManipulation(IFileTypeRepository fileTypeRepository)
        {
            _fileTypeRepository = fileTypeRepository;
        }

        public ICollection<FileTypeDomain> GetAllFileTypes()
        {
               return _fileTypeRepository.GetAllFileTypes();
        }

        public FileTypeDomain GetFileTypeById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            return _fileTypeRepository.GetFileTypeById(id);
        }

        public int CreateFileType(FileTypeDomain fileType)
        {
            if (fileType == null) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            return _fileTypeRepository.CreateFileType(fileType);
        }

        public int UpdateFileType(FileTypeDomain fileType)
        {
            if (fileType == null) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            return _fileTypeRepository.UpdateFileType(fileType);
        }

        public bool DeleteFileType(int fileTypeId)
        {
            if (fileTypeId <= 0) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            return _fileTypeRepository.DeleteFileType(fileTypeId);
        }

        public string GetFileExtensionById(int fileTypeId)
        {
            if (fileTypeId <= 0) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            return _fileTypeRepository.GetFileExtensionById(fileTypeId);
        }

        public int GetFileIdByExtension(string extension)
        {
            if (extension == null) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            return _fileTypeRepository.GetFileIdByExtension(extension);
        }

    }
}
