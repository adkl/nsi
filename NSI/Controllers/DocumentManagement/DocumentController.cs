using Microsoft.WindowsAzure.Storage;
using NSI.Common.Exceptions;
using NSI.Common.Resources.DocumentManagement;
using NSI.DataContracts.Documents;
using NSI.DocumentRepository.Helpers;
using NSI.DocumentRepository.Implementations;
using NSI.DocumentRepository.Interfaces;
using NSI.Domain.DocumentManagement;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Document Type manipulation
    /// </summary>
    public class DocumentController : ApiController
    {
        private readonly IDocumentManipulation _documentManipulation;
        private readonly IFileTypeManipulation _fileTypeManipulation;
        private readonly string _azureActive;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="DocumentManipulation"><see cref="IDocumentManipulation"/></param>
        /// <param name="actionContext"></param>
        public DocumentController(IDocumentManipulation documentManipulation, IFileTypeManipulation fileTypeManipulation)//, HttpActionContext actionContext)
        {
            _documentManipulation = documentManipulation;
            _fileTypeManipulation = fileTypeManipulation;
            _azureActive = ConfigurationManager.AppSettings["azureStorageActive"];
        }

        /// <summary>
        /// Retrieves all Documents from system
        /// </summary>
        /// <returns><see cref="IEnumerable{DocumentDomain}"/></returns>
        public IEnumerable<DocumentDomain> Get()
        {
            if (this._azureActive.Equals("true"))
            {
                return _documentManipulation.GetAllDocumentsWithStorageTypeId(1);
            }
            else if (this._azureActive.Equals("false"))
            {
                return _documentManipulation.GetAllDocumentsWithStorageTypeId(2);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves all Documents from system with pagination
        /// </summary>
        /// <returns><see cref="IEnumerable{DocumentDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Documents")]
        public IHttpActionResult Get(int? page, int size)
        {
            try
            {
                ICollection<DocumentDomain> documents = new List<DocumentDomain>();
                if (this._azureActive.Equals("true"))
                {
                    documents = _documentManipulation.GetAllDocumentsWithStorageTypeId(1);
                }
                else if (this._azureActive.Equals("false"))
                {
                    documents = _documentManipulation.GetAllDocumentsWithStorageTypeId(2);
                }

                var items = documents.Skip((page - 1 ?? 0) * size)
                                                      .Take(size)
                                                      .ToList();
                var total = documents.Count;

                return Ok(new { items, total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves Document by provided id
        /// </summary>
        /// <returns><see cref="DocumentDomain"/></returns>
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);
                }
                if (this._azureActive.Equals("true"))
                {
                    return Ok(_documentManipulation.GetDocumentById(id, 1));
                }
                else if (this._azureActive.Equals("false"))
                {
                    return Ok(_documentManipulation.GetDocumentById(id, 2));
                }
                else
                {
                    throw new NsiArgumentException(DocumentMessages.DocumentNotFound);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Retrieves all documents from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentDomain}"/></returns>
        public IEnumerable<DocumentDomain> Search(SearchRequest request)
        {
            return _documentManipulation.Search(request.Paging, request.FilterCriteria, request.SortCriteria);
        }

        /// <summary>
        /// Update Document  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult UpdateDocument([FromBody] DocumentDomain document)
        {
            try
            {
                if (document == null || document.DocumentId <= 0)
                {
                    throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);
                }

                var result = _documentManipulation.UpdateDocument(document);

                if (result <= 0)
                {
                    throw new NsiBaseException(string.Format(DocumentMessages.DocumentUpdateFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes Document 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);
                }
                var document = new DocumentDomain();
                if (this._azureActive.Equals("true"))
                {
                    document = _documentManipulation.GetDocumentById(id, 1);
                }
                else if (this._azureActive.Equals("false"))
                {
                    document = _documentManipulation.GetDocumentById(id, 2);
                }
                var fileType = _fileTypeManipulation.GetFileExtensionById(document.FileTypeId);
                var documentName = document.Name + '.' + fileType;
                if (document.StorageTypeId == 1)
                {
                    DeleteHelper.DeleteFileFromAzure(documentName);
                }
                else if (document.StorageTypeId == 2)
                {
                    FileInfo fi = new FileInfo(document.Path);
                    fi.Delete();
                }

                var result = _documentManipulation.DeleteDocument(id);

                if (!result)
                {
                    throw new NsiBaseException(string.Format(DocumentMessages.DocumentDeleteFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [SwaggerResponse(
            HttpStatusCode.OK,
            Description = "Saved successfully",
            Type = typeof(UploadedFileInfo))]
        [SwaggerResponse(
            HttpStatusCode.BadRequest,
            Description = "Could not find file to upload")]
        [SwaggerOperation("UploadFile")]
        public async Task<IHttpActionResult> UploadFile(string fileName = "", string description = "")
        {
            //Check if submitted content is of MIME Multi Part Content with Form-data in it?
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Could not find file to upload");
            }
            //Read the content in a InMemory Muli-Part Form Data format
            var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());
            //Get the first file
            var files = provider.Files;
            var uploadedFile = files[0];
            fileName = Path.GetFileNameWithoutExtension(uploadedFile.Headers.ContentDisposition.FileName.Trim('"'));
            //Extract the file extention
            var extension = ExtractExtension(uploadedFile);
            //Get the file's content type
            var contentType = uploadedFile.Headers.ContentType.ToString();
            //create the full name of the image with the fileName and extension
            var imageName = string.Concat(fileName, extension);
            //Get the reference to the Blob Storage and upload the file there
            var storageConnectionString = "";

            string[] tokens = extension.Split('.');
            string extensionName = tokens[tokens.Length - 1];
            var fileTypeId = _fileTypeManipulation.GetFileIdByExtension(extensionName);

            if (this._azureActive.Equals("true"))
            {
                storageConnectionString = ConfigurationManager.AppSettings["azurestoragepath"];
                var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference("nsicontainer");
                container.CreateIfNotExists();
                var blockBlob = container.GetBlockBlobReference(imageName);
                blockBlob.Properties.ContentType = contentType;
                using (var fileStream = await uploadedFile.ReadAsStreamAsync()) //as Stream is IDisposable
                {
                    blockBlob.UploadFromStream(fileStream);
                }

                var document = new DocumentDomain
                {
                    DocumentId = 0,
                    Name = fileName,
                    Path = blockBlob.Uri.ToString(),
                    FileSize = blockBlob.StreamWriteSizeInBytes,
                    ExternalId = Guid.NewGuid(),
                    LocationExternalId = blockBlob.Uri.ToString(),
                    DateCreated = DateTime.Now,
                    StorageTypeId = 1,
                    FileTypeId = fileTypeId,
                    Description = description
                };
                var result = _documentManipulation.CreateDocument(document);
                return Ok(result);
            }
            else if (this._azureActive.Equals("false"))
            {
                var localFilePath = ConfigurationManager.AppSettings["localstoragepath"];
                var guid = Guid.NewGuid();
                var fullPath = $@"{localFilePath}\{fileName}{extension}";
                var fileSize = 0;

                using (var fileStream = await uploadedFile.ReadAsStreamAsync())
                using (var localFileStream = File.Create(fullPath))
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.CopyTo(localFileStream);
                    fileSize = (int)fileStream.Length;
                }

                if (!DocumentManipulation.IsSafe(fullPath))
                {
                    File.Delete(fullPath);
                    return BadRequest("File contains malware");
                }

                var document = new DocumentDomain
                {
                    DocumentId = 0,
                    Name = fileName,
                    Path = fullPath,
                    FileSize = fileSize,
                    ExternalId = guid,
                    LocationExternalId = "",
                    DateCreated = DateTime.Now,
                    StorageTypeId = 2,
                    FileTypeId = fileTypeId,
                    Description = description
                };

                var result = _documentManipulation.CreateDocument(document);

                return Ok(result);
            }
            else
            {
                return BadRequest(DocumentMessages.UnexpectedProblem);
            }
        }
        public static string ExtractExtension(HttpContent file)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var fileStreamName = file.Headers.ContentDisposition.FileName;
            var fileName = new string(fileStreamName.Where(x => !invalidChars.Contains(x)).ToArray());
            var extension = Path.GetExtension(fileName);
            return extension;
        }

        /// <summary>
        /// Downloads Document 
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpGet]
        public HttpResponseMessage Download(int id)
        {
            DocumentDomain document = this._documentManipulation.GetDocumentById(id,1);
            var webClient = new WebClient();
            byte[] byteContent = webClient.DownloadData(document.Path);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(byteContent)

            };
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = document.Name + '.' + this._fileTypeManipulation.GetFileExtensionById(document.FileTypeId).ToString()
            };
            return response;
        }

    }
}
