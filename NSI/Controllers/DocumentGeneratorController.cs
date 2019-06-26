using NSI.Common.Enumerations;
using NSI.Common.Models;
using NSI.DataContracts.Base;
using NSI.DataContracts.Document;
using NSI.DocumentGenerator.Dtos;
using NSI.DocumentGenerator.Implementations;
using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.DocumentGenerator.Interfaces.Helpers;
using NSI.Domain.Document;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Generates PDF and HTML documents
    /// </summary>
    [Authorization.NsiAuthorization]
    public class DocumentGeneratorController : ApiController
    {
        private readonly IDocumentGenerator _documentGenerator;
        private readonly IGeneratedDocumentLogger _logger;

        /// <summary>
        /// Constructor for DocumentGeneratorController. Place of dependency injection.
        /// </summary>
        public DocumentGeneratorController(IDocumentGenerator documentGenerator, IGeneratedDocumentLogger logger)
        {
            _documentGenerator = documentGenerator;
            _logger = logger;
        }

        /// <summary>
        /// Converts HTML file to PDF document
        /// </summary>
        /// <param name="request"><see cref="GenerateDocumentRequest"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpPost]
        public HttpResponseMessage GeneratePdfFromHtml([FromBody] GenerateDocumentRequest request)
        {
            request.ValidateNotNull();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_documentGenerator.Generate(request.Content, request.Filename,
                    DocumentTypeEnum.Html, DocumentTypeEnum.Pdf,null).ByteContent)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename + ".pdf"
            };
            return response;
        }

        /// <summary>
        /// Converts JSON object to PDF document
        /// </summary>
        /// <param name="request"><see cref="GenerateDocumentRequest"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpPost]
        public HttpResponseMessage GeneratePdfFromJson([FromBody] GenerateDocumentRequest request)
        {
            request.ValidateNotNull();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_documentGenerator.Generate(request.Content, request.Filename,
                    DocumentTypeEnum.Json, DocumentTypeEnum.Pdf,null).ByteContent)
            };

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename + ".pdf"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }

        /// <summary>
        /// Converts JSON object to HTML file
        /// </summary>
        /// <param name="request"><see cref="GenerateDocumentRequest"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpPost]
        public HttpResponseMessage GenerateHtmlFromJson([FromBody] GenerateDocumentRequest request)
        {
            request.ValidateNotNull();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_documentGenerator.Generate(request.Content, request.Filename,
                    DocumentTypeEnum.Json, DocumentTypeEnum.Html,null).Content)
            };

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename + ".html"
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        /// <summary>
        /// Converts JSON file to ODT document
        /// </summary>
        /// <param name="request"><see cref="GenerateDocumentRequest"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpPost]
        public HttpResponseMessage GenerateOdtFromJson([FromBody] GenerateDocumentRequest request)
        {
            request.ValidateNotNull();
            string htmlContent = _documentGenerator.Generate(request.Content, request.Filename,
                    DocumentTypeEnum.Json, DocumentTypeEnum.Html,null).Content;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_documentGenerator.Generate(htmlContent, request.Filename,
                    DocumentTypeEnum.Html, DocumentTypeEnum.Odt,null).ByteContent)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.oasis.opendocument.text");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename + ".odt"
            };
            return response;
        }

        /// <summary>
        /// Converts HTML file to ODT document
        /// </summary>
        /// <param name="request"><see cref="GenerateDocumentRequest"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpPost]
        public HttpResponseMessage GenerateOdtFromHtml([FromBody] GenerateDocumentRequest request)
        {
            request.ValidateNotNull();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_documentGenerator.Generate(request.Content, request.Filename,
                    DocumentTypeEnum.Html, DocumentTypeEnum.Odt,null).ByteContent)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.oasis.opendocument.text");
            // https://www.ryadel.com/en/get-file-content-mime-type-from-extension-asp-net-mvc-core/
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename + ".odt"
            };
            return response;
        }

        /// <summary>
        /// Converts JSON file to DOCX document
        /// </summary>
        /// <param name="request"><see cref="GenerateDocumentRequest"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpPost]
        public HttpResponseMessage GenerateDocxFromJson([FromBody] GenerateDocumentRequest request)
        {
            request.ValidateNotNull();
            string htmlContent = _documentGenerator.Generate(request.Content, request.Filename,
                    DocumentTypeEnum.Json, DocumentTypeEnum.Html,null).Content;
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_documentGenerator.Generate(htmlContent, request.Filename,
                    DocumentTypeEnum.Html, DocumentTypeEnum.Docx,null).ByteContent)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename + ".docx"
            };
            return response;
        }

        /// <summary>
        /// Converts HTML file to DOCX document
        /// </summary>
        /// <param name="request"><see cref="GenerateDocumentRequest"/></param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpPost]
        public HttpResponseMessage GenerateDocxFromHtml([FromBody] GenerateDocumentRequest request)
        {
            request.ValidateNotNull();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(_documentGenerator.Generate(request.Content, request.Filename,
                    DocumentTypeEnum.Html, DocumentTypeEnum.Docx,null).ByteContent)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            // https://www.ryadel.com/en/get-file-content-mime-type-from-extension-asp-net-mvc-core/
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = request.Filename + ".docx"
            };
            return response;
        }

        /// <summary>
        /// Returns generated documents specified by page
        /// </summary>
        /// <param name="request"><see cref="GetGeneratedDocRequest"/></param>
        /// <returns><see cref="GetGeneratedDocResponse"/></returns>
//        [Route("logger-test")]
        [HttpGet]
        [ResponseType(typeof(GetGeneratedDocResponse))]
        public IHttpActionResult GetAllLogs([FromBody] GetGeneratedDocRequest request)
        {
            request.ValidateNotNull();
            return Ok(new GetGeneratedDocResponse()
            {
                Data = _logger.GetAllLogs(request.FilterCriteria, request.SortCriteria, request.Paging),
                Success = ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Returns all document types 
        /// </summary>
        /// <returns><see cref="IEnumerable"/></returns>
        [HttpGet]
        public IEnumerable<DocumentTypeDomain> GetAllDocTypes()
        {
            return _documentGenerator.GetAllTypes();
        }   

    }
}
