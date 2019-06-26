using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.Document;
using Nsi.TestsCore.Mocks.DocumentGenerator;
using Nsi.TestsCore.Mocks.TemplateManagement;
using NSI.Api.Controllers;
using NSI.BusinessLogic.Interfaces.TemplateManagement;
using NSI.BusinessLogic.TemplateManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.DataContracts.Document;
using NSI.DataContracts.TemplateManagement;
using NSI.DocumentGenerator.Implementations.Generators;
using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.DocumentGenerator.Interfaces.Helpers;
using NSI.Domain.TemplateManagement;
using NSI.Repository.Interfaces.Document;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace NSI.Tests.Controllers.TemplateManagement
{
    [TestClass]
    public class TemplateManagementControllerTest
    {
        private Mock<ITemplateVersionRepository> _templateVersionRepositoryMock;
        private Mock<ITemplateRepository> _templateRepositoryMock;
        private Mock<IFolderRepository> _folderRepositoryMock;
        private static TemplateManagementController controller;
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
        private Mock<IDocxGenerator> _docxGeneratorMock;
        private Mock<IOdtGenerator> _odtGeneratorMock;
        private static ITemplateGenerator templateGenerator;
        private IDocumentGenerator documentGenerator;
        public CreateTemplateRequest templateRequest;

        public CreateTemplateRequest createTemplateRequest()
        {
            List<TemplatePlaceholderDomain> placeholders = new List<TemplatePlaceholderDomain>();
            placeholders.Add(new TemplatePlaceholderDomain()
            {
                Id = 1,
                Description = "placeholder Description",
                Type = HtmlInputTypes.text,
                Length = 10
            });

            templateRequest = new CreateTemplateRequest();
            templateRequest.Name = "TemplateName";
            templateRequest.FolderId = 1;
            templateRequest.Content = new TemplateContentDomain()
            {
                Type = TemplateType.Text,
                Payload = new TemplatePayloadDomain()
                {
                    Text = "",
                    Placeholders = placeholders
                }
            };

            return templateRequest;
        }

        public CreateTemplateVersionRequest createTemplateVersionRequest()
        {
            List<TemplatePlaceholderDomain> placeholders = new List<TemplatePlaceholderDomain>();
            placeholders.Add(new TemplatePlaceholderDomain()
            {
                Id = 1,
                Description = "placeholder Description",
                Type = HtmlInputTypes.text,
                Length = 10
            });

            CreateTemplateVersionRequest templateVersionRequest = new CreateTemplateVersionRequest()
            {
                TemplateId = 1,
                Content = new TemplateContentDomain()
                {
                    Type = TemplateType.Text,
                    Payload = new TemplatePayloadDomain()
                    {
                        Text = "",
                        Placeholders = placeholders
                    }
                }
            };

            return templateVersionRequest;
        }

        public CreateFolderRequest createFolderRequest()
        {
            CreateFolderRequest folderRequest = new CreateFolderRequest()
            {
                Name = "Root",
                ParentFolderId = 1
            };

            return folderRequest;
        }


        [TestInitialize]
        public void Initialize()
        {
            _documentTypeRepositoryMock = DocumentTypeRepositoryMock.GetDocumentTypeRepositoryMock();
            _generatedDocumentRepositoryMock = GeneratedDocumentRepositoryMock.GetGeneratedDocumentRepositoryMock();
            _docxGeneratorMock = DocxGeneratorMock.GetDocxGeneratorMock();
            _odtGeneratorMock = OdtGeneratorMock.GetOdtGeneratorMock();
            jsonContent = new GenerateDocumentRequest();
            htmlContent = new GenerateDocumentRequest();
            htmlGenerator = new DocumentGenerator.Implementations.HtmlGenerator(new DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper());
            pdfGenerator = new DocumentGenerator.Implementations.PdfGenerator();
            generatedDocumentLogger = new DocumentGenerator.Implementations.Helpers.GeneratedDocumentLogger(_generatedDocumentRepositoryMock.Object);
            templateGenerator = new TemplateGenerator(new DocumentGenerator.Implementations.PdfGenerator(), new DocumentGenerator.Implementations.HtmlGenerator(new DocumentGenerator.Implementations.Helpers.HtmlGeneratorHelper()));
            documentGenerator = new DocumentGenerator.Implementations.Generators.DocumentGenerator(_documentTypeRepositoryMock.Object, generatedDocumentLogger, htmlGenerator, pdfGenerator, _odtGeneratorMock.Object, _docxGeneratorMock.Object, templateGenerator);

            _templateVersionRepositoryMock = TemplateVersionRepositoryMock.GetTemplateVersionRepositoryMock();
            _folderRepositoryMock = FolderRepositoryMock.GetFolderRepositoryMock();
            _templateRepositoryMock = TemplateRepositoryMock.GetTemplateRepositoryMock();

            templateVersionManipulation = new TemplateVersionManipulation(_templateVersionRepositoryMock.Object);
            folderManipulation = new FolderManipulation(_folderRepositoryMock.Object);
            templateManipulation = new TemplateManipulation(_templateRepositoryMock.Object, templateVersionManipulation, folderManipulation);

            templateVersionManipulation = new TemplateVersionManipulation(_templateVersionRepositoryMock.Object);
            exportTemplateManipulation = new ExportTemplateManipulation(templateManipulation, templateVersionManipulation, documentGenerator);
            controller = new TemplateManagementController(folderManipulation, templateManipulation, templateVersionManipulation, exportTemplateManipulation);

            createTemplateRequest();
        }

        [TestMethod]
        public void CreateTemplate_Success()
        {
            var result = controller.CreateTemplate(templateRequest);
            Assert.IsNotNull(result);
            var getContent = result as OkNegotiatedContentResult<CreateTemplateResponse>;
            Assert.AreEqual(getContent.Content.Data, 1);
            Assert.AreEqual(getContent.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Template name is not provided.", SeverityEnum.Warning)]
        public void CreateTemplate_NameIsNull()
        {
            templateRequest.Name = null;
            var result = controller.CreateTemplate(templateRequest);
        }

        /*    [TestMethod]
            [ExtendedExpectedException(typeof(NsiArgumentException), "Template type is not valid.", SeverityEnum.Warning)]
            public void CreateTemplate_ContentTypeIsNull()
            {
                templateRequest.Content.Type = TemplateType.UnsupportedType;
                var result = controller.CreateTemplate(templateRequest);
            } */

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Template payload is not valid.", SeverityEnum.Warning)]
        public void CreateTemplate_TextIsNull()
        {
            templateRequest.Content.Type = TemplateType.Text;
            templateRequest.Content.Payload.Text = null;
            var result = controller.CreateTemplate(templateRequest);
        }

        [TestMethod]
        public void CreateTemplateVersion_Success()
        {
            CreateTemplateVersionRequest templateVersionRequest = createTemplateVersionRequest();
            var result = controller.CreateTemplateVersion(templateVersionRequest);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<CreateTemplateVersionResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<CreateTemplateVersionResponse>;
            Assert.AreEqual(unwrappedResult.Content.Data, 1);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Template payload is not valid.", SeverityEnum.Warning)]
        public void CreateTemplateVersion_TextIsNull()
        {
            CreateTemplateVersionRequest templateVersionRequest = createTemplateVersionRequest();
            templateVersionRequest.Content.Payload.Text = null;
            var result = controller.CreateTemplateVersion(templateVersionRequest);
        }

        [TestMethod]
        public void CreateFolder_Success()
        {
            CreateFolderRequest folderRequest = createFolderRequest();
            var result = controller.CreateFolder(folderRequest);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<CreateFolderResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<CreateFolderResponse>;
            Assert.AreEqual(unwrappedResult.Content.Data, 1);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        public void ExportTemplateToPdf_Success()
        {
            var httpResponse = controller.ExportTemplateToPdf(19);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/pdf"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            int index = httpResponse.Content.Headers.ContentDisposition.FileName.IndexOf(".");
            Assert.AreEqual(httpResponse.Content.Headers.ContentDisposition.FileName.Substring(index), ".pdf");
        }

        [TestMethod]
        public void ExportTemplateToHtml_Success()
        {
            var httpResponse = controller.ExportTemplateToHtml(19);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("text/html"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            int index = httpResponse.Content.Headers.ContentDisposition.FileName.IndexOf(".");
            Assert.AreEqual(httpResponse.Content.Headers.ContentDisposition.FileName.Substring(index), ".html");
        }

        [TestMethod]
        public void ExportTemplateTableToPdf_Success()
        {
            var httpResponse = controller.ExportTemplateToPdf(20);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("application/pdf"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            int index = httpResponse.Content.Headers.ContentDisposition.FileName.IndexOf(".");
            Assert.AreEqual(httpResponse.Content.Headers.ContentDisposition.FileName.Substring(index), ".pdf");
            
        }

        [TestMethod]
        public void ExportTemplateTableToHtml_Success()
        {
            var httpResponse = controller.ExportTemplateToHtml(20);
            Assert.IsNotNull(httpResponse);
            HttpResponseMessage dummy = new HttpResponseMessage();
            Assert.AreEqual(httpResponse.GetType(), dummy.GetType());
            Assert.AreEqual(httpResponse.Content.Headers.ContentType, new MediaTypeHeaderValue("text/html"));
            Assert.IsTrue(httpResponse.IsSuccessStatusCode);
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK);
            int index = httpResponse.Content.Headers.ContentDisposition.FileName.IndexOf(".");
            Assert.AreEqual(httpResponse.Content.Headers.ContentDisposition.FileName.Substring(index), ".html");

        }
        [TestMethod]
        public void GetTemplateVersion()
        {
            var result = controller.GetTemplateVersion(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GetTemplateVersionResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<GetTemplateVersionResponse>;
            Assert.IsInstanceOfType(unwrappedResult.Content.Data, typeof(TemplateVersionDomain));
            Assert.AreEqual(unwrappedResult.Content.Data.TemplateId, 1);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        public void GetAll()
        {
            var result = controller.GetAll(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GetTemplatesResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<GetTemplatesResponse>;
            Assert.IsInstanceOfType(unwrappedResult.Content.Data, typeof(List<TemplateDomain>));
            Assert.IsTrue(unwrappedResult.Content.Data.Count >= 0);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        public void GetAllRootFolders()
        {
            var result = controller.GetAllRootFolders(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GetFoldersResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<GetFoldersResponse>;
            Assert.IsInstanceOfType(unwrappedResult.Content.Data, typeof(List<FolderDomain>));
            Assert.IsTrue(unwrappedResult.Content.Data.Count >= 0);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        public void GetAllTemplateVersions_()
        {
           /* var result = controller.GetAllTemplateVersions(null);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GetTemplateVersionsResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<GetTemplateVersionsResponse>;
            Assert.IsInstanceOfType(unwrappedResult.Content.Data, typeof(List<TemplateVersionDomain>));
            Assert.IsTrue(unwrappedResult.Content.Data.Count >= 0);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded); */
        }

        [TestMethod]
        public void GetDefaultTemplateVersion()
        {
            var result = controller.GetDefaultTemplateVersion(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GetTemplateVersionResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<GetTemplateVersionResponse>;
            Assert.IsInstanceOfType(unwrappedResult.Content.Data, typeof(TemplateVersionDomain));
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        public void DeleteTemplateVersion()
        {
            var result = controller.DeleteTemplateVersion(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<DeleteTemplateVersionResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<DeleteTemplateVersionResponse>;
            Assert.AreEqual(unwrappedResult.Content.Data, null);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        public void DeleteTemplateByTemplateVersionId()
        {
            var result = controller.DeleteTemplateByTemplateVersionId(0);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<DeleteTemplateResponse>));
            var unwrappedResult = result as OkNegotiatedContentResult<DeleteTemplateResponse>;
            Assert.AreEqual(unwrappedResult.Content.Data, null);
            Assert.AreEqual(unwrappedResult.Content.Success, ResponseStatus.Succeeded);
        }

        [TestMethod]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Template version with provided id does not exist.", SeverityEnum.Error)]
        public void ExportTemplate_Failed()
        {
            var httpResponse = controller.ExportTemplateToPdf(10);
        }
    }
}
