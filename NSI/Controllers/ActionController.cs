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
    /// Exposes methods for Action manipulation
    /// </summary>
    [NsiAuthorization]
    public class ActionController : ApiController
    {
        private readonly IDeviceActionManipulation _deviceActionManipulation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="deviceActionManipulation"><see cref="IDeviceActionManipulation"/></param>
        public ActionController(IDeviceActionManipulation deviceActionManipulation)
        {
            _deviceActionManipulation = deviceActionManipulation;
        }

        /// <summary>
        /// Retrieves all device actions from system
        /// </summary>
        /// <returns><see cref="IEnumerable{ActionDomain}"/></returns>
        public IEnumerable<ActionDomain> Get()
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];
            return _deviceActionManipulation.GetAllActions(userTenantId);
        }
    }
}