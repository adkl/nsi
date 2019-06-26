using NSI.Common.Exceptions;
using NSI.Common.Resources.DocumentManagement;
using NSI.Domain.DocumentManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.DocumentManagement;
using System.Collections.Generic;
using System.Linq;

namespace NSI.Repository
{
    public class FileTypeRepository : IFileTypeRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public FileTypeRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<FileTypeDomain> GetAllFileTypes()
        {
            ICollection<FileTypeDomain> listToReturn = new List<FileTypeDomain>();

            var result = _context.FileType.ToList();

            if (result == null) throw new NsiProcessingException(FileTypeMessages.UnexpectedProblem);

            foreach (FileType FileType in result)
            {
                listToReturn.Add(FileType.ToDomainModel());
            }

            return listToReturn;
        }

        public FileTypeDomain GetFileTypeById(int id)
        {
            var result = _context.FileType.FirstOrDefault(x => x.FileTypeId == id).ToDomainModel();

            if (result == null) throw new NsiNotFoundException(FileTypeMessages.FileTypeNotFound);

            return result;
        }

        public string GetFileExtensionById(int id)
        {
            var result = _context.FileType.FirstOrDefault(x => x.FileTypeId == id).ToDomainModel().Extension;

            if (result == null) throw new NsiNotFoundException(FileTypeMessages.FileTypeNotFound);

            return result;
        }

        public int GetFileIdByExtension(string extension)
        {
            var result = _context.FileType.FirstOrDefault(x => x.Extension.Trim() == extension.Trim()).ToDomainModel().FileTypeId;

            if (result < 0) throw new NsiNotFoundException(FileTypeMessages.FileTypeNotFound);

            return result;
        }

        public int CreateFileType(FileTypeDomain fileType)
        {

            if (fileType == null) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            var fileTypeDb = new FileType().FromDomainModel(fileType);

            _context.FileType.Add(fileTypeDb);
            _context.SaveChanges();

            return fileTypeDb.FileTypeId;
        }

        public int UpdateFileType(FileTypeDomain fileType)
        {
            if (fileType == null) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            if (!_context.FileType.Any(x => x.FileTypeId == fileType.FileTypeId))
            {
                throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidId);
            }

            var fileTypeDb = _context.FileType.Where(x => x.FileTypeId == fileType.FileTypeId).FirstOrDefault().FromDomainModel(fileType);

            if (fileTypeDb == null) throw new NsiNotFoundException(FileTypeMessages.FileTypeNotFound);

            _context.SaveChanges();
            return fileTypeDb.FileTypeId;
        }

        public bool DeleteFileType(int fileTypeId)
        {
            if (fileTypeId <= 0) throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);

            var fileTypeDb = _context.FileType.Where(x => x.FileTypeId == fileTypeId).FirstOrDefault();

            if (fileTypeDb == null)
            {
                throw new NsiNotFoundException(FileTypeMessages.FileTypeInvalidId);
            }

            _context.FileType.Remove(fileTypeDb);
            _context.SaveChanges();
            return true;
        }
    }
}