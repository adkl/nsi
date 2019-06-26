using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSI.DocumentRepository.Implementations;
using NSI.Repository.Interfaces.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nsi.TestsCore.Mocks.DocumentManagement;
using NSI.Domain.DocumentManagement;
using Nsi.TestsCore.Extensions;
using NSI.Common.Exceptions;
using NSI.Common.Enumerations;

namespace NSI.Tests.Business.DocumentManagement
{
    [TestClass]
    public class FileTypeTests
    {
        private Mock<IFileTypeRepository> _fileTypeRepositoryMock;
        private FileTypeManipulation _fileTypeManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _fileTypeRepositoryMock = FileTypeRepositoryMock.GetFileTypeRepositoryMock();
            _fileTypeManipulation = new FileTypeManipulation(
                _fileTypeRepositoryMock.Object
                );
        }

        #region Get File Type By ID - Tests
        [TestMethod, TestCategory("File Types - Get File Type By Id")]
        public void GetFileTypeById_Success()
        {
            FileTypeDomain fileType = _fileTypeManipulation.GetFileTypeById(1);
            Assert.AreEqual(1, fileType.FileTypeId);
            Assert.AreEqual("pdf", fileType.Name);
            Assert.AreEqual("pdf", fileType.Code);
            Assert.AreEqual("pdf", fileType.Extension);
            Assert.AreEqual(0, fileType.Documents.Count);
        }

        [TestMethod, TestCategory("File Typess - Get File Type By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetFileTypeById_Fail_InvalidFileTypeId()
        {
            _fileTypeManipulation.GetFileTypeById(-1);
        }
        #endregion

        #region Get All File Types - Tests
        [TestMethod, TestCategory("File Types - Get All File Types")]
        public void GetAllFileTypes_Success()
        {
            _fileTypeManipulation.GetAllFileTypes();
        }
        #endregion

        #region Get File Type Extension By Id - Tests
        [TestMethod, TestCategory("File Types - Get File Type Extension By Id")]
        public void GetFileTypeExtensionById_Success()
        {
            string extension = _fileTypeManipulation.GetFileExtensionById(1);
            Assert.AreEqual("pdf", extension);
        }
        #endregion

        #region Get File Type Extension By Id - Tests
        [TestMethod, TestCategory("File Types - Get File Type Extension By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetFileTypeExtensionById_Fail()
        {
            string extension = _fileTypeManipulation.GetFileExtensionById(-1);
        }
        #endregion

        #region Get File Type Id By Extension - Tests
        [TestMethod, TestCategory("File Types - Get File Type Id By Extension")]
        public void GetFileTypeIdByExtension_Success()
        {
            int id = _fileTypeManipulation.GetFileIdByExtension("pdf");
            Assert.AreEqual(1, id);
        }
        #endregion

        #region Get File Type Extension By Id - Tests
        [TestMethod, TestCategory("File Types - Get File Type Id By Extension")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetFileTypeIdByExtension_Fail()
        {
            int id = _fileTypeManipulation.GetFileIdByExtension(null);
        }
        #endregion

        #region Create File Type - Tests
        [TestMethod, TestCategory("File Types - Create File Type")]
        public void CreateFileType_Success()
        {
            int fileTypeId = _fileTypeManipulation.CreateFileType(GetValidFileTypeDomain());
            Assert.AreEqual(1, fileTypeId);
        }
        #endregion

        #region Create File Type - Tests
        [TestMethod, TestCategory("File Types - Create File Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void CreateFileType_Fail()
        {
           _fileTypeManipulation.CreateFileType(null);
        }
        #endregion

        #region Update File Type - Tests
        [TestMethod, TestCategory("File Types - Update File Type")]
        public void UpdateFileType_Success()
        {
            int fileTypeId = _fileTypeManipulation.UpdateFileType(GetValidFileTypeDomain());
            Assert.AreEqual(1, fileTypeId);
        }

        [TestMethod, TestCategory("File Types - Update File Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateFileType_Fail_InvalidFileType()
        {
            _fileTypeManipulation.UpdateFileType(null);
        }
        #endregion

        #region Delete File Type - Tests
        [TestMethod, TestCategory("File Types - Delete File Type")]
        public void DeleteFileType_Success()
        {
            bool isDeleted = _fileTypeManipulation.DeleteFileType(1);
            Assert.AreEqual(true, isDeleted);
        }

        [TestMethod, TestCategory("File Types - Delete File Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteFileType_Fail_InvalidFileTypeId()
        {
            _fileTypeManipulation.DeleteFileType(-1);
        }
        #endregion

        #region Valid File Type Models
        private FileTypeDomain GetValidFileTypeDomain()
        {
            return new FileTypeDomain()
            {
                FileTypeId = 1,
                Name = "pdf",
                Code = "pdf",
                Extension = "pdf",
                Documents = new List<DocumentDomain>()
            };
        }
        #endregion
    }
}
