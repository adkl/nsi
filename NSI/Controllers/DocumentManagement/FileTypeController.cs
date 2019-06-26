using NSI.Common.Exceptions;
using NSI.Common.Resources.DocumentManagement;
using NSI.DocumentRepository.Interfaces;
using NSI.Domain.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for File Type manipulation
    /// </summary>
    public class FileTypeController : ApiController
    {
        private readonly IFileTypeManipulation _fileTypeManipulation;
        //private readonly HttpActionContext _actionContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="FileTypeManipulation"><see cref="IFileTypeManipulation"/></param>
        /// <param name="actionContext"></param>
        public FileTypeController(IFileTypeManipulation fileTypeManipulation)//, HttpActionContext actionContext)
        {
            _fileTypeManipulation = fileTypeManipulation;
            //_actionContext = actionContext;
        }

        /// <summary>
        /// Retrieves all FileTypes from system
        /// </summary>
        /// <returns><see cref="IEnumerable{FileTypeDomain}"/></returns>
        public IEnumerable<FileTypeDomain> Get()
        {
            return _fileTypeManipulation.GetAllFileTypes();
        }

        /// <summary>
        /// Retrieves all FileTypes from system with pagination
        /// </summary>
        /// <returns><see cref="IEnumerable{FileTypeDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/FileTypes")]
        public IHttpActionResult Get(int? page, int size)
        {
            try
            {
                var FileTypes = _fileTypeManipulation.GetAllFileTypes();

                var items = FileTypes.Skip((page - 1 ?? 0) * size)
                                                      .Take(size)
                                                      .ToList();
                var total = FileTypes.Count;

                return Ok(new { items, total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves FileType by provided id
        /// </summary>
        /// <returns><see cref="FileTypeDomain"/></returns>
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);
                }

                var result = _fileTypeManipulation.GetFileTypeById(id);

                return Ok(_fileTypeManipulation.GetFileTypeById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Creates FileType  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult CreateFileType([FromBody] FileTypeDomain fileType)
        {
            try
            {
                if (fileType == null)
                {
                    throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);
                }

                var result = _fileTypeManipulation.CreateFileType(fileType);

                if (result <= 0)
                {
                    throw new NsiBaseException(string.Format(FileTypeMessages.FileTypeCreationFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update FileType  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult UpdateFileType([FromBody] FileTypeDomain fileType)
        {
            try
            {
                if (fileType == null || fileType.FileTypeId <= 0)
                {
                    throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);
                }

                var result = _fileTypeManipulation.UpdateFileType(fileType);

                if (result <= 0)
                {
                    throw new NsiBaseException(string.Format(FileTypeMessages.FileTypeUpdateFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes FileType 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);
                }

                var result = _fileTypeManipulation.DeleteFileType(id);

                if (!result)
                {
                    throw new NsiBaseException(string.Format(FileTypeMessages.FileTypeUpdateFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get File Extension By FileType Id
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult GetFileExtensionById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);
                }

                var result = _fileTypeManipulation.GetFileExtensionById(id);

                if (result == null)
                {
                    throw new NsiBaseException(string.Format(FileTypeMessages.FileTypeUpdateFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get File Type Id By FileType Extension
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [Route("api/FileType/GetFileIdByExtension/{id}")]
        public IHttpActionResult GetFileIdByExtension(string id)
        {
            try
            {
                if (id == null)
                {
                    throw new NsiArgumentException(FileTypeMessages.FileTypeInvalidArgument);
                }

                var result = _fileTypeManipulation.GetFileIdByExtension(id);

                if (result < 0)
                {
                    throw new NsiBaseException(string.Format(FileTypeMessages.FileTypeUpdateFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}