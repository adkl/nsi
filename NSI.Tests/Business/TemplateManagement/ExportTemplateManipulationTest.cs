using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.Document;
using Nsi.TestsCore.Mocks.TemplateManagement;
using NSI.Api.Controllers;
using NSI.BusinessLogic.Interfaces.TemplateManagement;
using NSI.BusinessLogic.TemplateManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.DataContracts.Document;
using NSI.DocumentGenerator.Implementations.Generators;
using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.DocumentGenerator.Interfaces.Helpers;
using NSI.Repository.Interfaces.Document;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.TemplateManagement
{
    [TestClass]
    public class ExportTemplateManipulationTest
    {
        private Mock<ITemplateVersionRepository> _templateVersionRepositoryMock;
        private Mock<ITemplateRepository> _templateRepositoryMock;
        private Mock<IFolderRepository> _folderRepositoryMock;
        private TemplateManagementController controller;
        private IFolderManipulation folderManipulation;
        private ITemplateManipulation templateManipulation;
        private ITemplateVersionManipulation templateVersionManipulation;
        private IExportTemplateManipulation exportTemplateManipulation;

        private Mock<IDocumentTypeRepository> _documentTypeRepositoryMock;
        private Mock<IGeneratedDocumentRepository> _generatedDocumentRepositoryMock;
        private GenerateDocumentRequest jsonContent;
        private GenerateDocumentRequest htmlContent;
        private static IGeneratedDocumentLogger generatedDocumentLogger;

        private static IHtmlGenerator htmlGenerator;
        private static IPdfGenerator pdfGenerator;
        private static IOdtGenerator odtGenerator;
        private static IDocxGenerator docxGenerator;
        private static ITemplateGenerator templateGenerator;
        private IDocumentGenerator documentGenerator;
        [TestInitialize]
        public void Initialize()
        {
            _documentTypeRepositoryMock = DocumentTypeRepositoryMock.GetDocumentTypeRepositoryMock();
            _generatedDocumentRepositoryMock = GeneratedDocumentRepositoryMock.GetGeneratedDocumentRepositoryMock();
            jsonContent = new GenerateDocumentRequest();
            htmlContent = new GenerateDocumentRequest();
            htmlGenerator = new DocumentGenerator.Implementations.HtmlGenerator(new DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper());
            pdfGenerator = new DocumentGenerator.Implementations.PdfGenerator();
            odtGenerator = new DocumentGenerator.Implementations.OdtGenerator();
            docxGenerator = new DocumentGenerator.Implementations.DocxGenerator();
            generatedDocumentLogger = new DocumentGenerator.Implementations.Helpers.GeneratedDocumentLogger(_generatedDocumentRepositoryMock.Object);
            templateGenerator = new TemplateGenerator(new DocumentGenerator.Implementations.PdfGenerator(), new DocumentGenerator.Implementations.HtmlGenerator(new DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper()));
            documentGenerator = new DocumentGenerator.Implementations.Generators.DocumentGenerator(_documentTypeRepositoryMock.Object, generatedDocumentLogger, htmlGenerator, pdfGenerator, odtGenerator, docxGenerator, templateGenerator);

            _templateVersionRepositoryMock = TemplateVersionRepositoryMock.GetTemplateVersionRepositoryMock();
            _folderRepositoryMock = FolderRepositoryMock.GetFolderRepositoryMock();
            _templateRepositoryMock = TemplateRepositoryMock.GetTemplateRepositoryMock();

            templateVersionManipulation = new TemplateVersionManipulation(_templateVersionRepositoryMock.Object);
            folderManipulation = new FolderManipulation(_folderRepositoryMock.Object);
            templateManipulation = new TemplateManipulation(_templateRepositoryMock.Object, templateVersionManipulation, folderManipulation);

            templateVersionManipulation = new TemplateVersionManipulation(_templateVersionRepositoryMock.Object);
            exportTemplateManipulation = new ExportTemplateManipulation(templateManipulation, templateVersionManipulation, documentGenerator);
        }

        [TestMethod]
        public void TestException_TemplateVersionNull()
        {
            
            //exportTemplateManipulation.ExportTemplate(5, Common.Enumerations.DocumentTypeEnum.Html);
        }
    }
}
