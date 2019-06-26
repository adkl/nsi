using Moq;
using NSI.Common.Models;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.DocumentManagement
{
    public class DocumentMock
    {
        public static Mock<IDocumentRepository> GetDocumentRepositoryMock()
        {
            var documentRepository = new Mock<IDocumentRepository> { CallBase = false };

            #region Get Document By Id
            documentRepository.Setup(x => x.GetDocumentById(1, 1)).Returns(
                new DocumentDomain()
                {
                    DocumentId = 1,
                    Name = "Test Document",
                    Path = "path",
                    FileSize = 100,
                    ExternalId = Guid.NewGuid(),
                    LocationExternalId  = "location id test",
                    DateCreated = DateTime.Now,
                    StorageTypeId = 1,
                    FileTypeId = 1,
                    Description = "Test test test test"
                });
            #endregion

            #region Get all Documents
            documentRepository.Setup(x => x.GetAllDocuments()).Returns(new List<DocumentDomain>() {
                new DocumentDomain()
                {
                    DocumentId = 1,
                    Name = "Test Document",
                    Path = "path",
                    FileSize = 100,
                    ExternalId = Guid.NewGuid(),
                    LocationExternalId  = "location id test",
                    DateCreated = DateTime.Now,
                    StorageTypeId = 1,
                    FileTypeId = 1,
                    Description = "Test test test test"
                },
                new DocumentDomain()
                {
                    DocumentId = 2,
                    Name = "Test Document 2",
                    Path = "path",
                    FileSize = 200,
                    ExternalId = Guid.NewGuid(),
                    LocationExternalId  = "location id test",
                    DateCreated = DateTime.Now,
                    StorageTypeId = 1,
                    FileTypeId = 1,
                    Description = "Test test test test test test test"
                }
            });

            #endregion

            #region Create Document
            documentRepository.Setup(x => x.CreateDocument(It.IsAny<DocumentDomain>()))
                .Returns(1);
            #endregion

            #region Update Document
            documentRepository.Setup(x => x.UpdateDocument(It.IsAny<DocumentDomain>()))
                .Returns(1);
            #endregion

            #region Delete Document
            documentRepository.Setup(x => x.DeleteDocument(1))
                .Returns(true);
            #endregion

            #region Get Document
            documentRepository.Setup(x => x.GetAllDocumentsWithStorageTypeId(1)).Returns(

                new List<DocumentDomain>(){
                     new DocumentDomain()
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
                     }
                }
            );
            #endregion

            #region Search Documents
            documentRepository.Setup(x => x.Search(new Paging()
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
                }})).Returns(

                new List<DocumentDomain>(){
                     new DocumentDomain()
                     {
                         DocumentId = 1,
                         Name = "Test",
                         Path = "path",
                         FileSize = 100,
                         ExternalId = Guid.NewGuid(),
                         LocationExternalId = "location id test",
                         DateCreated = DateTime.Now,
                         StorageTypeId = 1,
                         FileTypeId = 1,
                         Description = "Test test test test"
                     }
                }
            );
            #endregion


            return documentRepository;
        }

    }
}
