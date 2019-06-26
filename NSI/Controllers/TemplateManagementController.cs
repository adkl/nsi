using NSI.BusinessLogic.Interfaces.TemplateManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Common.Resources.TemplateManagement;
using NSI.DataContracts.Base;
using NSI.DataContracts.TemplateManagement;
using NSI.Domain.Document;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Template manipulation
    /// </summary>
    [Authorization.NsiAuthorization]
    public class TemplateManagementController : ApiController
    {
        private readonly IFolderManipulation _folderManipulation;
        private readonly ITemplateManipulation _templateManipulation;
        private readonly ITemplateVersionManipulation _templateVersionManipulation;
        private readonly IExportTemplateManipulation _exportTemplateManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        public TemplateManagementController(IFolderManipulation folderManipulation, ITemplateManipulation templateManipulation, ITemplateVersionManipulation templateVersionManipulation, IExportTemplateManipulation exportTemplateManipulation)
        {
            _folderManipulation = folderManipulation;
            _templateManipulation = templateManipulation;
            _templateVersionManipulation = templateVersionManipulation;
            _exportTemplateManipulation = exportTemplateManipulation;
        }

        /// <summary>
        /// Creates template  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpPost]
        [ResponseType(typeof(CreateTemplateResponse))]
        public IHttpActionResult CreateTemplate([FromBody] CreateTemplateRequest template)
        {
            template.ValidateNotNull();
            template.ValidateContent();
            return Ok(new CreateTemplateResponse()
            {
                Data = _templateManipulation.Add(template),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });          
            
        }

        /// <summary>
        /// Creates template version 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpPost]
        [ResponseType(typeof(CreateTemplateVersionResponse))]
        public IHttpActionResult CreateTemplateVersion([FromBody] CreateTemplateVersionRequest template)
        {
            template.ValidateNotNull();
            template.ValidateContent();
            return Ok(new CreateTemplateVersionResponse()
            {
                Data = _templateVersionManipulation.Add(template),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });

        }

        /// <summary>
        /// Creates folder 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpPost]
        [ResponseType(typeof(CreateFolderResponse))]
        public IHttpActionResult CreateFolder([FromBody] CreateFolderRequest folder)
        {
            folder.ValidateContent();
            return Ok(new CreateFolderResponse()
            {
                Data = _folderManipulation.Add(folder),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });

        }

        /// <summary>
        /// Exports pdf template version by Id
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpGet]
        public HttpResponseMessage ExportTemplateToPdf(int templateVersionId)
        {
            GeneratedDocumentDomain generatedDocument = _exportTemplateManipulation.ExportTemplate(templateVersionId, DocumentTypeEnum.Pdf);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(generatedDocument.ByteContent)         
                    
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = generatedDocument.Name + ".pdf"
            };
            return response;
        }

        /// <summary>
        /// Exports html template version by Id
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpGet]
        public HttpResponseMessage ExportTemplateToHtml(int templateVersionId)
        {
            GeneratedDocumentDomain generatedDocument = _exportTemplateManipulation.ExportTemplate(templateVersionId,DocumentTypeEnum.Html);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(generatedDocument.Content)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = generatedDocument.Name + ".html"
            };
            return response;
        }

        /// <summary>
        /// Get template version by Id
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetTemplateVersionResponse))]
        public IHttpActionResult GetTemplateVersion([FromUri] int templateVersionId)
        {
            return Ok(new GetTemplateVersionResponse
            {
                Data = _templateVersionManipulation.GetByVersionId(templateVersionId),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Gets all templates. Paging, sorting and filtering can be specified in the request 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetTemplatesResponse))]
        public IHttpActionResult GetAll([FromUri] GetTemplatesRequest request)
        {
            if (request == null) request = new GetTemplatesRequest();
            return Ok(new GetTemplatesResponse()
            {
                Data = _templateManipulation.GetAll(request.FilterCriteria, request.SortCriteria, request.Paging),
                Success = Common.Enumerations.ResponseStatus.Succeeded,
                Paging = request.Paging
            });

        }

        /// <summary>
        /// Gets all template folders. Paging can be specified in the request 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetFoldersResponse))]
        public IHttpActionResult GetAllRootFolders([FromBody] GetFoldersRequest request)
        {
            if (request == null) request = new GetFoldersRequest();
            return Ok(new GetFoldersResponse()
            {
                Data = _folderManipulation.GetAllRootFolders(request.Paging),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });

        }

        /// <summary>
        /// Performs search based on template name. Pagination can be specified in the request 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetTemplatesResponse))]
        public IHttpActionResult SearchByName([FromUri] string templateName, [FromBody] SimplePagingRequest request)
        {
            return Ok(new GetTemplatesResponse()
            {
                Data = _templateManipulation.GetAllByName(templateName, request == null ? null : request.Paging),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });

        }

        /// <summary>
        /// Gets all template versions. Paging, sorting and filtering can be specified in the request 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetTemplateVersionsResponse))]
        public IHttpActionResult GetAllTemplateVersions([FromUri] GetTemplateVersionsRequest request)
        {
            if (request == null) request = new GetTemplateVersionsRequest();
            return Ok(new GetTemplateVersionsResponse()
            {
                Data = _templateVersionManipulation.GetAll(request.FilterCriteria, request.SortCriteria, request.Paging),
                Success = Common.Enumerations.ResponseStatus.Succeeded,
                Paging = request.Paging
            });
        }

        /// <summary>
        /// Get default template version by template id
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetTemplateVersionResponse))]
        public IHttpActionResult GetDefaultTemplateVersion(int templateId)
        {
            return Ok(new GetTemplateVersionResponse
            {
                Data = _templateVersionManipulation.GetDefaultByTemplateId(templateId),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Delete template version by Id
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteTemplateVersionResponse))]
        public IHttpActionResult DeleteTemplateVersion([FromUri] int templateVersionId)
        {
            return Ok(new DeleteTemplateVersionResponse
            {
                Data = _templateVersionManipulation.DeleteByVersionId(templateVersionId),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Delete template by template version Id
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteTemplateResponse))]
        public IHttpActionResult DeleteTemplateByTemplateVersionId([FromUri] int templateVersionId)
        {
            return Ok(new DeleteTemplateResponse
            {
                Data = _templateManipulation.DeleteByTemplateVersionId(templateVersionId),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}