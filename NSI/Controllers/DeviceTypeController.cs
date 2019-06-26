using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Device Type manipulation
    /// </summary>
    [NsiAuthorization]
    public class DeviceTypeController : ApiController
    {
        private readonly IDeviceTypeManipulation _deviceTypeManipulation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="deviceTypeManipulation"><see cref="IDeviceTypeManipulation"/></param>
        public DeviceTypeController(IDeviceTypeManipulation deviceTypeManipulation)
        {
            _deviceTypeManipulation = deviceTypeManipulation;
        }

        /// <summary>
        /// Retrieves all device types from system
        /// </summary>
        /// <returns><see cref="IEnumerable{DeviceTypeDomain}"/></returns>
        public IEnumerable<DeviceTypeDomain> Get()
        {
            return _deviceTypeManipulation.GetAllDeviceTypes();
        }

        /// <summary>
        /// Retrieves all device types from system with pagination
        /// </summary>
        /// <returns><see cref="IEnumerable{DeviceTypeDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/DeviceTypes")]
        public IHttpActionResult Get(int? page, int size)
        {
            var devicetypes = _deviceTypeManipulation.GetAllDeviceTypes();

            var items = devicetypes.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();

            var total = devicetypes.Count;

            return Ok(new { items, total });
        }

        /// <summary>
        /// Retrieves device type by provided id
        /// </summary>
        /// <returns><see cref="DeviceTypeDomain"/></returns>
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceTypeMessages.DeviceTypeInvalidArgument);
            }

            var result = _deviceTypeManipulation.GetDeviceTypeById(id);

            if (result == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, DeviceTypeMessages.DeviceTypeNotFound);
            }

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all active device types from system
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/DeviceTypes/Active")]
        public IHttpActionResult GetActiveDeviceTypes(int? page, int size)
        {
            var deviceTypes = _deviceTypeManipulation.GetAllActiveDeviceTypes();

            var items = deviceTypes.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();
            var total = deviceTypes.Count;

            return Ok(new { items, total });
        }

        /// <summary>
        /// Retrieves all inactive device types from system
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/DeviceTypes/Inactive")]
        public IHttpActionResult GetInactiveDeviceTypes(int? page, int size)
        {
            var deviceTypes = _deviceTypeManipulation.GetAllInactiveDeviceTypes();

            var items = deviceTypes.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();
            var total = deviceTypes.Count;

            return Ok(new { items, total });
        }

        /// <summary>
        /// Creates device type 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult CreateDeviceType([FromBody] DeviceTypeDomain deviceType)
        {
            if (deviceType == null)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceTypeMessages.DeviceTypeInvalidArgument);
            }

            var result = _deviceTypeManipulation.CreateDeviceType(deviceType);

            if (result <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceTypeMessages.DeviceTypeCreationFailed);

            }

            return Ok(result);
        }

        /// <summary>
        /// Updates device type 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult UpdateDeviceType([FromBody] DeviceTypeDomain deviceType)
        {
            if (deviceType == null)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceTypeMessages.DeviceTypeInvalidArgument);
            }

            var result = _deviceTypeManipulation.UpdateDeviceType(deviceType);

            if (result <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceTypeMessages.DeviceTypeUpdateFailed);
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes device type 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceTypeMessages.DeviceTypeInvalidArgument);
            }

            var result = _deviceTypeManipulation.DeleteDeviceType(id);

            if (!result)
            {
                return Content(System.Net.HttpStatusCode.NotFound, DeviceTypeMessages.DeviceTypeNotFound);
            }

            return Ok(result);
        }

        /// <summary>
        /// Searches for devicetypes from system with pagination
        /// </summary>
        /// <returns><see cref="IEnumerable{DeviceTypeDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/DeviceTypes/Search")]
        public IHttpActionResult SearchDeviceTypes(int? page, int size, String s, bool filtered = false, bool active = false)
        {
            if (page <= 0 || size <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceTypeMessages.DeviceTypeInvalidArgument);
            }

            var deviceTypes = filtered ? _deviceTypeManipulation.SearchDeviceTypes(s, active) : _deviceTypeManipulation.SearchDeviceTypes(s);

            var items = deviceTypes.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();

            var total = deviceTypes.Count;

            return Ok(new { items, total });
        }
    }
}