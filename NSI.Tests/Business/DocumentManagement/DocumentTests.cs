using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.DocumentManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.DocumentRepository.Implementations;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.DocumentManagement
{
    [TestClass]
    public class DocumentTests
    {
        private Mock<IDocumentRepository> _documentRepositoryMock;
        private DocumentManipulation _documentManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _documentRepositoryMock = DocumentMock.GetDocumentRepositoryMock();
            _documentManipulation = new DocumentManipulation(
                _documentRepositoryMock.Object
                );
        }

        #region Get Document By ID - Tests
        [TestMethod, TestCategory("Documents - Get Document By Id")]
        public void GetDocumentById_Success()
        {
            DocumentDomain document = _documentManipulation.GetDocumentById(1,1);
            Assert.AreEqual(1, document.DocumentId);
            Assert.AreEqual(1, document.FileTypeId);
            Assert.AreEqual(1, document.StorageTypeId);
            Assert.AreEqual("Test Document", document.Name);
            Assert.AreEqual("Test test test test", document.Description);
            Assert.AreEqual(100, document.FileSize);
            Assert.AreEqual("path", document.Path);
            Assert.AreEqual("location id test", document.LocationExternalId);
        }

        [TestMethod, TestCategory("Documents - Get Document By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetDocumentById_Fail_InvalidDocumentId()
        {
            _documentManipulation.GetDocumentById(-1,1);
        }
        #endregion

        #region Get Documents by Storage type - Tests
        [TestMethod, TestCategory("Documents - Get Document with Storage Type")]
        public void GetDocumentsWithStorageType_Success()
        {
            _documentManipulation.GetAllDocumentsWithStorageTypeId(1);
        }

        [TestMethod, TestCategory("Documents - Get Document with Storage Type")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetDocumentsWithStorageType_Fail_InvalidDocumentId()
        {
            _documentManipulation.GetAllDocumentsWithStorageTypeId(-1);
        }
        #endregion

        #region Get All Documents - Tests
        [TestMethod, TestCategory("Documents - Get All Documents")]
        public void GetAllDocuments_Success()
        {
            _documentManipulation.GetAllDocuments();
        }
        #endregion

        #region Create Document - Tests
        [TestMethod, TestCategory("Documents - Create Document")]
        public void CreateDocument_Success()
        {
            int documentId = _documentManipulation.CreateDocument(GetValidDocumentDomain());
            Assert.AreEqual(1, documentId);
        }
        #endregion

        #region Create Document - Tests
        [TestMethod, TestCategory("Documents - Create Document")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void CreateDocument_Fail()
        {
            _documentManipulation.CreateDocument(null);
        }
        #endregion

        #region Update Document - Tests
        [TestMethod, TestCategory("Documents - Update Document")]
        public void UpdateDocument_Success()
        {
            int documentId = _documentManipulation.UpdateDocument(GetValidDocumentDomain());
            Assert.AreEqual(1, documentId);
        }

        [TestMethod, TestCategory("Documents - Update Document")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateDocument_Fail_InvalidDocument()
        {
            _documentManipulation.UpdateDocument(null);
        }
        #endregion

        #region Delete Document - Tests
        [TestMethod, TestCategory("Documents - Delete Document")]
        public void DeleteDocument_Success()
        {
            bool isDeleted = _documentManipulation.DeleteDocument(1);
            Assert.AreEqual(true, isDeleted);
        }

        [TestMethod, TestCategory("Documents - Delete Document")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteDocument_Fail_InvalidDocumentId()
        {
            _documentManipulation.DeleteDocument(-1);
        }
        #endregion

        #region Search Documents - Tests
        [TestMethod, TestCategory("Documents - Search Documents")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided page is not valid.", SeverityEnum.Error)]
        public void SearchDocuments_Fail()
        {
            ICollection<DocumentDomain> documents = (Collection<DocumentDomain>)_documentManipulation.Search(new Paging()
            {
                Page = -5,
                TotalRecords = 1,
                RecordsPerPage = 1,
                Pages = 1
            },

                new List<FilterCriteria>() { new FilterCriteria()
                {
                    ColumnName = "Name",
                    FilterTerm = "Test Document",
                    IsExactMatch = false
                } }
                , new List<SortCriteria>() {
                    new SortCriteria()
                {
                    ColumnName = "Name",
                    Order = 0,
                    Priority = 0
                }});

        }

        #endregion


        #region Search Documents - Tests
        [TestMethod, TestCategory("Documents - Search Documents")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided records per page count is not valid.", SeverityEnum.Error)]
        public void SearchDocuments_FailRecordsPerPage()
        {
            ICollection<DocumentDomain> documents = (Collection<DocumentDomain>)_documentManipulation.Search(new Paging()
            {
                Page = 1,
                TotalRecords = 1,
                RecordsPerPage = -5,
                Pages = 1
            },

                new List<FilterCriteria>() { new FilterCriteria()
                {
                    ColumnName = "Name",
                    FilterTerm = "Test Document",
                    IsExactMatch = false
                } }
                , new List<SortCriteria>() {
                    new SortCriteria()
                {
                    ColumnName = "Name",
                    Order = 0,
                    Priority = 0
                }});

        }

        #endregion


        #region Search Documents - Tests
        [TestMethod, TestCategory("Documents - Search Documents")]
        public void SearchDocuments_Success()
        {
            ICollection<DocumentDomain> documents = (Collection<DocumentDomain>)_documentManipulation.Search(new Paging()
            {
                Page = 1,
                TotalRecords = 1,
                RecordsPerPage = 1,
                Pages = 1
            },

                new List<FilterCriteria>() { new FilterCriteria()
                {
                    ColumnName = "Name",
                    FilterTerm = "Test",
                    IsExactMatch = false
                } }
                , new List<SortCriteria>() {
                    new SortCriteria()
                {
                    ColumnName = "Name",
                    Order = 0,
                    Priority = 0
                }});

        }

        #endregion


        #region Search Documents - Tests
        [TestMethod, TestCategory("Documents - Search Documents")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Column name for filtering is not provided.", SeverityEnum.Error)]
        public void SearchDocuments_FailColumnName()
        {
            ICollection<DocumentDomain> documents = (Collection<DocumentDomain>)_documentManipulation.Search(new Paging()
            {
                Page = 1,
                TotalRecords = 1,
                RecordsPerPage = 1,
                Pages = 1
            },

                new List<FilterCriteria>() { new FilterCriteria()
                {
                    ColumnName = "",
                    FilterTerm = "Test Document",
                    IsExactMatch = false
                } }
                , new List<SortCriteria>() {
                    new SortCriteria()
                {
                    ColumnName = "Name",
                    Order = 0,
                    Priority = 0
                }});

        }

        #endregion

        #region Search Documents - Tests
        [TestMethod, TestCategory("Documents - Search Documents")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Column name for sorting is not provided.", SeverityEnum.Error)]
        public void SearchDocuments_FailColumnNameSortCriteria()
        {
            ICollection<DocumentDomain> documents = (Collection<DocumentDomain>)_documentManipulation.Search(new Paging()
            {
                Page = 1,
                TotalRecords = 1,
                RecordsPerPage = 1,
                Pages = 1
            },

                new List<FilterCriteria>() { new FilterCriteria()
                {
                    ColumnName = "Name",
                    FilterTerm = "Test Document",
                    IsExactMatch = false
                } }
                , new List<SortCriteria>() {
                    new SortCriteria()
                {
                    ColumnName = "",
                    Order = 0,
                    Priority = 0
                }});

        }

        #endregion

   
        #region Valid Document Models
        private DocumentDomain GetValidDocumentDomain()
        {
            return new DocumentDomain()
            {
                DocumentId = 1,
                Name = "Test Document",
                Path = "path",
                FileSize = 100,
                ExternalId = Guid.NewGuid(),
                LocationExternalId = "location id test",
                DateCreated = DateTime.Now,
                StorageTypeId = 1,
                FileTypeId = 1,
                Description = "Test test test test"
            };
        }
        #endregion
    }
}
