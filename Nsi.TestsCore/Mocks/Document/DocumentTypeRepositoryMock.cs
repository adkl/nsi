using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.Repository.Interfaces.Document;
using NSI.Domain.Document;
using Moq;


namespace Nsi.TestsCore.Mocks.Document
{
    public static class DocumentTypeRepositoryMock
    {
        public static Mock<IDocumentTypeRepository> GetDocumentTypeRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var documentTypeRepository = new Mock<IDocumentTypeRepository> { CallBase = false };

            #region Get By Name
            documentTypeRepository.Setup(x => x.GetByName(It.IsAny<string>())).Returns((string type) => new DocumentTypeDomain()
            {
                Name = type,
                Code = type,
                Version = "1.0",
                Encoding = "utf-8"
            });
            #endregion

            List<string> list = new List<string> { "pdf", "html", "json", "odt", "docx" };
            List<DocumentTypeDomain> toReturn = new List<DocumentTypeDomain>();
            foreach (string item in list)
            {
                toReturn.Add(new DocumentTypeDomain()
                    {
                        Name = item,
                        Code = item,
                        Version = "1.0",
                        Encoding = "utf-8"
                    });
            }

            #region Add
            documentTypeRepository.Setup(x => x.Add(It.IsAny<DocumentTypeDomain>()));
            #endregion

            #region Get all
            documentTypeRepository.Setup(x => x.GetAll()).Returns(toReturn);
            #endregion

            #region Get By Id
            documentTypeRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => toReturn[id]);
            #endregion

            return documentTypeRepository;
        }
    }
}
