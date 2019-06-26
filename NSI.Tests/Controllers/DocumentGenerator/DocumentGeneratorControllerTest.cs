using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSI.Api.Controllers;
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
using Nsi.TestsCore.Mocks;
using System.Collections.Generic;
using NSI.Domain.Document;
using NSI.Common.Models;
using NSI.Common.Enumerations;
using Nsi.TestsCore.Mocks.DocumentGenerator;

namespace NSI.Tests.Controllers
{

    [TestClass]
    public class DocumentGeneratorControllerTest
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
        private static DocumentGeneratorController controller;

        private static IDocumentGenerator documentGenerator;
        public GenerateDocumentRequest setJsonContent()
        {
            jsonContent.Content = "{\"Result\":[{\"IsEnabled\": true,\"Id\": 10015,\"Name\": \"Reena\"},{\"IsEnabled\": true,\"Id\": 10015,\"Name\": \"Reena\"},{\"IsEnabled\": true,\"Id\": 10015,\"Name\": \"Reena\"}]}";
            jsonContent.Filename = "testJson";
            return jsonContent;
        }

        public GenerateDocumentRequest setHtmlContent()
        {
            htmlContent.Content = "<html> Test </html>";
            htmlContent.Filename = "testHtml";
            return htmlContent;
        }

        [TestInitialize]
        public void Initialize()
        {
            _documentTypeRepositoryMock = DocumentTypeRepositoryMock.GetDocumentTypeRepositoryMock();
            _generatedDocumentRepositoryMock = GeneratedDocumentRepositoryMock.GetGeneratedDocumentRepositoryMock();
            jsonContent = new GenerateDocumentRequest();
            htmlContent = new GenerateDocumentRequest();
            htmlGenerator = new DocumentGenerator.Implementations.HtmlGenerator(new DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper());
            pdfGenerator = new DocumentGenerator.Implementations.PdfGenerator();
            _docxGeneratorMock = DocxGeneratorMock.GetDocxGeneratorMock();
            _odtGeneratorMock = OdtGeneratorMock.GetOdtGeneratorMock();

            generatedDocumentLogger = new DocumentGenerator.Implementations.Helpers.GeneratedDocumentLogger(_generatedDocumentRepositoryMock.Object);
            templateGenerator = new TemplateGenerator(new DocumentGenerator.Implementations.PdfGenerator(), new DocumentGenerator.Implementations.HtmlGenerator(new DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper()));
            documentGenerator = new DocumentGenerator.Implementations.Generators.DocumentGenerator(_documentTypeRepositoryMock.Object, generatedDocumentLogger, htmlGenerator, pdfGenerator, _odtGeneratorMock.Object, _docxGeneratorMock.Object, templateGenerator);
            setJsonContent();
            setHtmlContent();
            controller = new DocumentGeneratorController(documentGenerator, generatedDocumentLogger);
        }

        [TestMethod, TestCategory("Document Generator - Convert JSON to Html")]
        public void JsonToHtml_Success()
        {
            var httpResponse = controller.GenerateHtmlFromJson(this.jsonContent);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("text/html");
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, mediaType);
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(httpResponse.Content.Headers.ContentDisposition.Equals(new ContentDispositionHeaderValue("attachment") { FileName = jsonContent.Filename + ".html" }));
        }

        [TestMethod, TestCategory("Document Generator - Convert JSON to Html")]
        public void JsonToHtml_EmptyContent()
        {
            jsonContent.Content = "";
            DocumentGeneratorController controller = new DocumentGeneratorController(documentGenerator, generatedDocumentLogger);
            Assert.ThrowsException<NsiArgumentNullException>(() => controller.GenerateHtmlFromJson(jsonContent));
        }

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiBaseException), "HTML file could not be generated.", SeverityEnum.Error)]
        public void HtmlFromJson_ParseError()
        {
            var httpResponse = controller.GenerateHtmlFromJson(htmlContent);
        }

        [TestMethod]
        public void HtmlToPdf_FilenameIsNull_Success()
        {
            jsonContent.Filename = null;
            var httpResponse = controller.GeneratePdfFromHtml(htmlContent);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/pdf"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(httpResponse.Content.Headers.ContentDisposition.Equals(new ContentDispositionHeaderValue("attachment") { FileName = htmlContent.Filename + ".pdf" }));
        }

        [TestMethod]
        public void JsonToPDF_Success()
        {
            var httpResponse = controller.GeneratePdfFromJson(jsonContent);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/pdf"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(httpResponse.Content.Headers.ContentDisposition.Equals(new ContentDispositionHeaderValue("attachment") { FileName = jsonContent.Filename + ".pdf" }));
        }

        [TestMethod]
        public void HtmlToOdt_Success()
        {
            var httpResponse = controller.GenerateOdtFromHtml(htmlContent);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/vnd.oasis.opendocument.text"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(httpResponse.Content.Headers.ContentDisposition.Equals(new ContentDispositionHeaderValue("attachment") { FileName = htmlContent.Filename + ".odt" }));
        }

        [TestMethod]
        public void JsonToOdt_Success()
        {
            var httpResponse = controller.GenerateOdtFromJson(jsonContent);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/vnd.oasis.opendocument.text"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(httpResponse.Content.Headers.ContentDisposition.Equals(new ContentDispositionHeaderValue("attachment") { FileName = jsonContent.Filename + ".odt" }));
        }

        [TestMethod]
        public void HtmlToDocx_Success()
        {
            var httpResponse = controller.GenerateDocxFromHtml(htmlContent);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(httpResponse.Content.Headers.ContentDisposition.Equals(new ContentDispositionHeaderValue("attachment") { FileName = htmlContent.Filename + ".docx" }));
        }

        [TestMethod]
        public void JsonToDocx_Success()
        {
            var httpResponse = controller.GenerateDocxFromJson(jsonContent);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            Assert.IsTrue(httpResponse.Content.Headers.ContentDisposition.Equals(new ContentDispositionHeaderValue("attachment") { FileName = jsonContent.Filename + ".docx" }));
        }

        [TestMethod]
        public void GetAllDocTypes()
        {
            var result = controller.GetAllDocTypes();
            Assert.IsNotNull(result);
            IEnumerable<DocumentTypeDomain> dummy = new List<DocumentTypeDomain>();
            Assert.AreEqual(result.GetType(), dummy.GetType());
        }

        [TestMethod]
        public void GetAllLogs()
        {
            GetGeneratedDocRequest getGeneratedDocRequest = new GetGeneratedDocRequest();
            getGeneratedDocRequest.FilterCriteria = new List<FilterCriteria>() { };
            getGeneratedDocRequest.SortCriteria = new List<SortCriteria>() {};
            getGeneratedDocRequest.Paging = new Paging();
            var result = controller.GetAllLogs(getGeneratedDocRequest);
            Assert.IsNotNull(result);
        }


            
    }
}
