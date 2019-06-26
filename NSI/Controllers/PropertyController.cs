using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Property manipulation
    /// </summary>
    [NsiAuthorization]
    public class PropertyController : ApiController
    {
        private readonly IDevicePropertyManipulation _devicePropertyManipulation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="devicePropertyManipulation"><see cref="IDevicePropertyManipulation"/></param>
        public PropertyController(IDevicePropertyManipulation devicePropertyManipulation)
        {
            _devicePropertyManipulation = devicePropertyManipulation;
        }

        /// <summary>
        /// Retrieves all device properties from system
        /// </summary>
        /// <returns><see cref="IEnumerable{PropertyDomain}"/></returns>
        public IEnumerable<PropertyDomain> Get()
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];
            return _devicePropertyManipulation.GetAllProperties(userTenantId);
        }
    }
}