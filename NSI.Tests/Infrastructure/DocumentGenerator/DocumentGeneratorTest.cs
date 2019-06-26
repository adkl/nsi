using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSI.DataContracts.Document;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.DocumentGenerator.Interfaces.Helpers;
using NSI.DocumentGenerator.Implementations.Generators;
using NSI.Repository.Interfaces.Document;
using NSI.EF;
using NSI.DocumentGenerator.Interfaces;
using NSI.Common.Exceptions;
using Nsi.TestsCore.Mocks.Document;
using Nsi.TestsCore.Extensions;
using NSI.Common.Enumerations;
using NSI.Domain.Document;
using System;
using Nsi.TestsCore.Mocks.DocumentGenerator;

namespace NSI.Tests.Infrastructure.DocumentGenerator
{
    [TestClass]
    public class DocumentGeneratorTest
    {
        private Mock<IDocumentTypeRepository> _documentTypeRepositoryMock;
        private Mock<IGeneratedDocumentRepository> _generatedDocumentRepositoryMock;
        private GenerateDocumentRequest jsonContent;
        private GenerateDocumentRequest htmlContent;
        private static NsiContext nsiContext;
        private static IDocumentTypeRepository documentTypeRepository;
        private static IGeneratedDocumentRepository generatedDocumentRepository;

        private static IGeneratedDocumentLogger generatedDocumentLogger;

        private static IHtmlGenerator htmlGenerator;
        private static IPdfGenerator pdfGenerator;
        private Mock<IDocxGenerator> _docxGeneratorMock;
        private Mock<IOdtGenerator> _odtGeneratorMock;
        private static ITemplateGenerator templateGenerator;

        private static IDocumentGenerator documentGenerator;
        private DocumentTypeDomain jsonDocumentTypeDomain;
        private DocumentTypeDomain htmlDocumentTypeDomain;

        [TestInitialize]
        public void Initialize()
        {
            _documentTypeRepositoryMock = DocumentTypeRepositoryMock.GetDocumentTypeRepositoryMock();
            _generatedDocumentRepositoryMock = GeneratedDocumentRepositoryMock.GetGeneratedDocumentRepositoryMock();
            jsonContent = new GenerateDocumentRequest();
            htmlContent = new GenerateDocumentRequest();
            nsiContext = new NsiContext();
            htmlGenerator = new NSI.DocumentGenerator.Implementations.HtmlGenerator(new NSI.DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper());
            pdfGenerator = new NSI.DocumentGenerator.Implementations.PdfGenerator();

            _docxGeneratorMock = DocxGeneratorMock.GetDocxGeneratorMock();
            _odtGeneratorMock = OdtGeneratorMock.GetOdtGeneratorMock();

            generatedDocumentLogger = new NSI.DocumentGenerator.Implementations.Helpers.GeneratedDocumentLogger(_generatedDocumentRepositoryMock.Object);
            templateGenerator = new TemplateGenerator(new NSI.DocumentGenerator.Implementations.PdfGenerator(), new NSI.DocumentGenerator.Implementations.HtmlGenerator(new NSI.DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper()));
            documentGenerator = new NSI.DocumentGenerator.Implementations.Generators.DocumentGenerator(_documentTypeRepositoryMock.Object, generatedDocumentLogger, htmlGenerator, pdfGenerator, _odtGeneratorMock.Object, _docxGeneratorMock.Object, templateGenerator);
            jsonDocumentTypeDomain = new DocumentTypeDomain()
            {
                Name = "json",
                Code = "json",
                Version = "1.0",
                Encoding = "utf-8"
            };
            htmlDocumentTypeDomain = new DocumentTypeDomain()
            {
                Name = "html",
                Code = "html",
                Version = "1.0",
                Encoding = "utf-8"
            };

        }

        [TestMethod]
        public void AddBasicDocTypesToDb_Success()
        {
            documentGenerator.AddBasicDocTypesToDb();
        }


        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Document content not found.", SeverityEnum.Error)]
        public void GenerateDocument_TypeIsNull()
        {
            // Input and output type are null
            documentGenerator.Generate("Test", "Test", null, null, null);
        }

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Document content not found.", SeverityEnum.Error)]
        public void GenerateDocument_ContentIsNull()
        {
            // Content is null
            documentGenerator.Generate(null, "Test", jsonDocumentTypeDomain, htmlDocumentTypeDomain, null);
        }

        [TestMethod]
        public void GenerateDocument_FileNameIsNull()
        {
            // Content is null
            string jsonString = "{\"Result\":[{\"IsEnabled\": true,\"Id\": 10015,\"Name\": \"Reena\"},{\"IsEnabled\": true,\"Id\": 10015,\"Name\": \"Reena\"},{\"IsEnabled\": true,\"Id\": 10015,\"Name\": \"Reena\"}]}";
            var result = documentGenerator.Generate(jsonString, null, jsonDocumentTypeDomain, htmlDocumentTypeDomain, null);
            Assert.IsNotNull(result);
            GeneratedDocumentDomain dummy = new GeneratedDocumentDomain();
            Assert.AreEqual(result.GetType(), dummy.GetType());
            Assert.IsNotNull(result.Name);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void GenerateDocument_UnsupportedTypes()
        {
            DocumentTypeDomain inputType = new DocumentTypeDomain()
            {
                Name = "Test",
                Code = "Test",
                Version = "1.0",
                Encoding = "utf-8"
            };
            var result = documentGenerator.Generate("Test", "TestFile", inputType, htmlDocumentTypeDomain, null);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(result.Content, "There isn't any generator for given input and output data type.");
            Assert.AreEqual(result.Name, "Undefined document");
        }

    }
}
