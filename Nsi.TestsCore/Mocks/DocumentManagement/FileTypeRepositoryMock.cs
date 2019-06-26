using Moq;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.DocumentManagement
{
    public class FileTypeRepositoryMock
    {
        public static Mock<IFileTypeRepository> GetFileTypeRepositoryMock()
        {
            var fileTypeRepository = new Mock<IFileTypeRepository> { CallBase = false };

            #region Get File Type By ID
            fileTypeRepository.Setup(x => x.GetFileTypeById(1)).Returns(
                new FileTypeDomain()
                {
                    FileTypeId = 1,
                    Name = "pdf",
                    Code = "pdf",
                    Extension = "pdf",
                    Documents = new List<DocumentDomain>()
                });
            #endregion

            #region Get all File Types
            fileTypeRepository.Setup(x => x.GetAllFileTypes()).Returns(new List<FileTypeDomain>() {
                new FileTypeDomain()
                {
                    FileTypeId = 1,
                    Name = "pdf",
                    Code = "pdf",
                    Extension = "pdf",
                    Documents = new List<DocumentDomain>()
                },
                new FileTypeDomain()
                {
                    FileTypeId = 2,
                    Name = "txt",
                    Code = "txt",
                    Extension = "txt",
                    Documents = new List<DocumentDomain>()
                }
            });

            #endregion

            #region Create File Type
            fileTypeRepository.Setup(x => x.CreateFileType(It.IsAny<FileTypeDomain>()))
                .Returns(1);
            #endregion

            #region Update File Type
            fileTypeRepository.Setup(x => x.UpdateFileType(It.IsAny<FileTypeDomain>()))
                .Returns(1);
            #endregion

            #region Delete File Type
            fileTypeRepository.Setup(x => x.DeleteFileType(1))
                .Returns(true);
            #endregion

            #region Get File Type Id by Extension
            fileTypeRepository.Setup(x => x.GetFileIdByExtension("pdf")).Returns(1);
            #endregion

            #region Get File Type Extension by Id
            fileTypeRepository.Setup(x => x.GetFileExtensionById(1)).Returns("pdf");
            #endregion

            return fileTypeRepository;
        }
    
    }
}
