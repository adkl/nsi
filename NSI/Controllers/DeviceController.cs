using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Device manipulation
    /// </summary>
    [NsiAuthorization]
    public class DeviceController : ApiController
    {
        private readonly IDeviceManipulation _deviceManipulation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="deviceManipulation"><see cref="IDeviceManipulation"/></param>
        public DeviceController(IDeviceManipulation deviceManipulation)
        {
            _deviceManipulation = deviceManipulation;
        }

        /// <summary>
        /// Retrieves all devices from system
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult Get()
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            var devices = _deviceManipulation.GetAllDevices(userTenantId);

            return Ok(devices);
        }
        /// <summary>
        /// Retrieves all devices from system with pagination
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Devices")]
        public IHttpActionResult Get(int? page, int size)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            var devices = _deviceManipulation.GetAllDevices(userTenantId);

            var items = devices.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();
            var total = devices.Count;

            return Ok(new { items, total });
        }

        /// <summary>
        /// Retrieves device by provided id
        /// </summary>
        /// <returns><see cref="DeviceDomain"/></returns>
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceMessages.DeviceInvalidArgument);
            }

            UserDomain user = (UserDomain)ActionContext.Request.Properties["UserDetails"];

            var result = _deviceManipulation.GetDeviceById(id);

            if (result == null)
            {
                return Content(System.Net.HttpStatusCode.NotFound, DeviceMessages.DeviceNotFound);
            }

            if (result.TenantId != user.TenantId)
            {
                return Content(System.Net.HttpStatusCode.Unauthorized, DeviceMessages.NotAuthorizedAction);
            }

            return Ok(_deviceManipulation.GetDeviceById(id));
        }

        /// <summary>
        /// Retrieves all active devices from system
        /// </summary>
        /// <returns><see cref="IEnumerable{DeviceDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Devices/Active")]
        public IHttpActionResult GetActiveDevices(int? page, int size)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            var devices = _deviceManipulation.GetAllActiveDevices(userTenantId);

            var items = devices.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();
            var total = devices.Count;

            return Ok(new { items, total });
        }

        /// <summary>
        /// Retrieves all inactive devices from system
        /// </summary>
        /// <returns><see cref="IEnumerable{DeviceDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Devices/Inactive")]
        public IHttpActionResult GetInactiveDevices(int? page, int size)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            var devices = _deviceManipulation.GetAllInactiveDevices(userTenantId);

            var items = devices.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();
            var total = devices.Count;

            return Ok(new { items, total });
        }

        /// <summary>
        /// Creates device  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult CreateDevice([FromBody] CreateDeviceDomain device)
        {
            if (device.Description == null || device.Name == null || device.DeviceTypeId <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceMessages.DeviceInvalidArgument);
            }

            UserDomain user = (UserDomain)ActionContext.Request.Properties["UserDetails"];

            var result = _deviceManipulation.CreateDevice(device, user);

            if (result <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceMessages.DeviceCreationFailed);
            }

            return Content(System.Net.HttpStatusCode.Created, result);
        }

        /// <summary>
        /// Updates device  
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult UpdateDevice([FromBody] UpdateDeviceDomain device)
        {
            if (device.Description == null || device.Name == null || device.DeviceTypeId <= 0 || device.DeviceId <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceMessages.DeviceInvalidArgument);
            }

            UserDomain user = (UserDomain)ActionContext.Request.Properties["UserDetails"];

            var result = _deviceManipulation.UpdateDevice(device, user);

            if (result <= 0)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, DeviceMessages.DeviceUpdateFailed);
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes device 
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceMessages.DeviceInvalidArgument);
            }

            UserDomain user = (UserDomain)ActionContext.Request.Properties["UserDetails"];

            var result = _deviceManipulation.DeleteDevice(id, user);

            if (!result)
            {
                return Content(System.Net.HttpStatusCode.NotFound, DeviceMessages.DeviceUpdateFailed);
            }

            return Ok(result);
        }

        /// <summary>
        /// Searches for devices from system
        /// </summary>
        /// <returns><see cref="IEnumerable{DeviceDomain}"/></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Devices/Search")]
        public IHttpActionResult SearchDevices(int? page, int size, String s, bool filtered = false, bool active = false)
        {
            if(page <= 0 || size <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceMessages.DeviceInvalidArgument);
            }

            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            var devices = filtered ? _deviceManipulation.SearchDevices(userTenantId, s, active) : _deviceManipulation.SearchDevices(userTenantId, s);

            var items = devices.Skip((page - 1 ?? 0) * size)
                                                  .Take(size)
                                                  .ToList();
            var total = devices.Count;

            return Ok(new { items, total });
        }


        /// <summary>
        /// Retrieves number of incidents for provided device in provided number of last days
        /// </summary>
        /// <returns><see cref="DeviceDomain"/></returns>
        public IHttpActionResult GetNumberOfIncidents(int deviceId, int periodInDays)
        {
            if (deviceId <= 0 || periodInDays <= 0)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, DeviceMessages.DeviceInvalidArgument);
            }

            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            return Ok(_deviceManipulation.GetNumberOfIncidents(deviceId, periodInDays, userTenantId));

        }
    }
}