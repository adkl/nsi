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
    /// Exposes methods for Storage Type manipulation
    /// </summary>
    public class StorageTypeController : ApiController
    {
        private readonly IStorageTypeManipulation _storageTypeManipulation;
        //private readonly HttpActionContext _actionContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="StorageTypeManipulation"><see cref="IStorageTypeManipulation"/></param>
        /// <param name="actionContext"></param>
        public StorageTypeController(IStorageTypeManipulation storageTypeManipulation)//, HttpActionContext actionContext)
        {
            _storageTypeManipulation = storageTypeManipulation;
            //_actionContext = actionContext;
        }

        /// <summary>
        /// Retrieves all StorageTypes from system
        /// </summary>
        /// <returns><see cref="IEnumerable{StorageTypeDomain}"/></returns>
        public IEnumerable<StorageTypeDomain> Get()
        {
            return _storageTypeManipulation.GetAllStorageTypes();
        }

        /// <summary>
        /// Retrieves all StorageTypes from system with pagination
        /// </summary>
        /// <returns><see cref="IEnumerable{StorageTypeDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/StorageTypes")]
        public IHttpActionResult Get(int? page, int size)
        {
            try
            {
                var StorageTypes = _storageTypeManipulation.GetAllStorageTypes();

                var items = StorageTypes.Skip((page - 1 ?? 0) * size)
                                                      .Take(size)
                                                      .ToList();
                var total = StorageTypes.Count;

                return Ok(new { items, total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves StorageType by provided id
        /// </summary>
        /// <returns><see cref="StorageTypeDomain"/></returns>
        public IHttpActionResult Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);
                }

                var result = _storageTypeManipulation.GetStorageTypeById(id);

                return Ok(_storageTypeManipulation.GetStorageTypeById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Creates StorageType  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult CreateStorageType([FromBody] StorageTypeDomain storageType)
        {
            try
            {
                if (storageType == null)
                {
                    throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);
                }

                var result = _storageTypeManipulation.CreateStorageType(storageType);

                if (result <= 0)
                {
                    throw new NsiBaseException(string.Format(StorageTypeMessages.StorageTypeCreationFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update StorageType  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult UpdateStorageType([FromBody] StorageTypeDomain storageType)
        {
            try
            {
                if (storageType == null || storageType.StorageTypeId <= 0)
                {
                    throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);
                }

                var result = _storageTypeManipulation.UpdateStorageType(storageType);

                if (result <= 0)
                {
                    throw new NsiBaseException(string.Format(StorageTypeMessages.StorageTypeUpdateFailed));
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes StorageType 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);
                }

                var result = _storageTypeManipulation.DeleteStorageType(id);

                if (!result)
                {
                    throw new NsiBaseException(string.Format(StorageTypeMessages.StorageTypeUpdateFailed));
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