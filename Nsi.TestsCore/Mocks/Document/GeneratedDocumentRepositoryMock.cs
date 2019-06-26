using System;
using NSI.Repository.Interfaces.Document;
using NSI.Domain.Document;
using Moq;
using NSI.Common.Models;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.Document
{
    public static class GeneratedDocumentRepositoryMock
    {
        public static Mock<IGeneratedDocumentRepository> GetGeneratedDocumentRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var generatedDocumentRepository = new Mock<IGeneratedDocumentRepository> { CallBase = false };

            #region Add 
            generatedDocumentRepository.Setup(x => x.Add(It.IsAny<GeneratedDocumentDomain>()))
                .Returns(1);
            #endregion

            List<GeneratedDocumentDomain> toReturn = new List<GeneratedDocumentDomain>();
            toReturn.Add(new GeneratedDocumentDomain()
            {
                Id = 1,
                Name = "GenaretedDocumentDomainMockName",
                ExternalId = Guid.NewGuid(),
                Success = true,
                DateCreated = DateTime.Now,
                Content = null,
                TemplateVersionId = null,
                DocumentTypeId = 1,
                DocumentType = new DocumentTypeDomain()
                {
                    Name = "html",
                    Code = "html",
                    Version = "1.0",
                    Encoding = "utf-8"
                }
            });

            #region Get All 
            generatedDocumentRepository.Setup(x => x.GetAll(It.IsAny<List<FilterCriteria>>(), It.IsAny<List<SortCriteria>>(), It.IsAny<Paging>()))
                .Returns(toReturn);
            #endregion

            return generatedDocumentRepository;
        }
    }
}
