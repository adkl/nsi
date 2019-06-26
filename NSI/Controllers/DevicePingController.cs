using MassTransit;
using NSI.Api.Authorization;
using NSI.BusinessLogic.DevicePing;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.BusinessLogic.Interfaces.DevicePing;
using NSI.DataContracts.Base;
using NSI.DataContracts.DevicePing;
using NSI.Domain.DevicePing;
using NSI.Queue.Messages.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Device Ping manipulation
    /// </summary>
    [NsiAuthorization]
    public class DevicePingController : ApiController
    {
        private readonly IDevicePingManipulation _devicePingManipulation;
        private readonly IPingDeviceManipulation _pingDeviceManipulation;
        private readonly IDeviceManipulation _deviceManipulation;
        private readonly IPublishEndpoint _publishEndpoint;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="devicePingManipulation"></param>
        /// <param name="pingDeviceManipulation"></param>
        /// <param name="deviceManipulation"></param>
        /// <param name="publishEndpoint"></param>
        public DevicePingController(IDevicePingManipulation devicePingManipulation, IPingDeviceManipulation pingDeviceManipulation, IDeviceManipulation deviceManipulation, IPublishEndpoint publishEndpoint)
        {
            _devicePingManipulation = devicePingManipulation ?? throw new ArgumentNullException("devicePingManipulation cannot be null");
            _pingDeviceManipulation = pingDeviceManipulation ?? throw new ArgumentNullException("pingDeviceManipulation cannot be null");
            _deviceManipulation = deviceManipulation ?? throw new ArgumentNullException("deviceManipulation cannot be null");
            _publishEndpoint = publishEndpoint; 
        }

        // GET
        /// <summary>
        /// Get device ping object by id
        /// </summary>
        /// <param name="devicePingId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/DevicePing/GetById/{devicePingId}")]
        public DevicePingDomain GetDevicePingById(int devicePingId)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];
            return _devicePingManipulation.GetById(userTenantId, devicePingId);
        }

        // POST
        /// <summary>
        /// Add new device ping object and property values for device ping
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Post(List<DevicePropertyValue> devicePropertyValues)
        {
            try
            {
                if (devicePropertyValues != null && devicePropertyValues.Count > 0)
                {
                    var device = _deviceManipulation.GetDeviceById(devicePropertyValues[0].DeviceId);
                    _devicePingManipulation.Add(device.TenantId, devicePropertyValues);
                    return Ok();
                }

                return BadRequest();                
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        /// <summary>
        /// Searches devicePing. If no parameters have been provided in request, return all devicePings.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(SearchDevicePingResponse))]
        public IHttpActionResult Search(SearchDevicePingRequest request)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];
            request.ValidateNotNull();

            request.Paging.TotalRecords = _devicePingManipulation.DevicePingsCount();

            return Ok(new SearchDevicePingResponse()
            {
                Data = _devicePingManipulation.Search(userTenantId, request.Paging, request.FilterCriteria, request.SortCriteria),
                Paging = request.Paging,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        // DELETE
        /// <summary>
        /// Delete device ping object
        /// </summary>
        /// <param name="devicePingId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/DevicePing/Delete/{devicePingId}")]
        public IHttpActionResult Delete(int devicePingId)
        {
            try
            {
                int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];
                _devicePingManipulation.Delete(userTenantId, devicePingId);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST
        /// <summary>
        /// Add new device ping object
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PingDevice(PingDeviceRequest pingDeviceRequest)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            try
            {
                this._pingDeviceManipulation.PingDevice(userTenantId, pingDeviceRequest.pingDeviceDomain);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Retrieves last pinged value for selected property
        /// </summary>
        /// <returns></returns>
        public string GetLastValue(int id)
        {
            return _devicePingManipulation.GetLastValue(id);
        }
    }
}
