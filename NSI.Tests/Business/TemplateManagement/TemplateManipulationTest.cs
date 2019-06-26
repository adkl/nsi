using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.TemplateManagement;
using NSI.BusinessLogic.Interfaces.TemplateManagement;
using NSI.BusinessLogic.TemplateManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.TemplateManagement;
using NSI.Repository.Interfaces.TemplateManagement;

namespace NSI.Tests.Business.TemplateManagement
{
    [TestClass]
    public class TemplateManipulationTest
    {

        private Mock<ITemplateVersionRepository> _templateVersionRepositoryMock;
        private ITemplateVersionManipulation templateVersionManipulation;
        private ITemplateManipulation templateManipulation;
        private Mock<ITemplateRepository> _templateRepositoryMock;
        private Mock<IFolderRepository> _folderRepositoryMock;
        private IFolderManipulation folderManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _templateVersionRepositoryMock = TemplateVersionRepositoryMock.GetTemplateVersionRepositoryMock();
            _folderRepositoryMock = FolderRepositoryMock.GetFolderRepositoryMock();
            _templateRepositoryMock = TemplateRepositoryMock.GetTemplateRepositoryMock();

            templateVersionManipulation = new TemplateVersionManipulation(_templateVersionRepositoryMock.Object);
            folderManipulation = new FolderManipulation(_folderRepositoryMock.Object);

            templateManipulation = new TemplateManipulation(_templateRepositoryMock.Object, templateVersionManipulation, folderManipulation);
        }

        [TestMethod]
        public void Exists()
        {
            var result = templateManipulation.Exists(1);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllByTemplateId()
        {
            var result = templateVersionManipulation.GetAllByTemplateId(1, null);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetAllByTemplateId_Failed()
        {
            var result = templateVersionManipulation.GetAllByTemplateId(-1, null);
        }

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetAllByFolderId_Failed()
        {
            var result = templateManipulation.GetAllByFolderId(-1, null);
        }

        [TestMethod]
        public void GetAllByFolderId()
        {
            var result = templateManipulation.GetAllByFolderId(1, null);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void GetById()
        {
            var result = templateManipulation.GetById(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TemplateDomain));
        }

        /*[TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Template with provided id does not exist.", SeverityEnum.Error)]
        public void GetTemplateNameById()
        {
            var result = templateManipulation.GetTemplateNameById(5);
        }*/


    }
}