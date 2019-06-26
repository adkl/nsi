using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Repository.Interfaces.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.DeviceManagement
{
    public class DevicePropertyManipulation : IDevicePropertyManipulation
    {
        private readonly IDevicePropertyRepository _devicePropertyRepository;

        /// <summary>
        /// Constructor for DevicePropertyManipulation
        /// </summary>
        /// <param name="devicePropertyRepository"></param>
        public DevicePropertyManipulation(IDevicePropertyRepository devicePropertyRepository)
        {
            _devicePropertyRepository = devicePropertyRepository;
        }

        /// <summary>
        /// Get all device type properties for specific tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<PropertyDomain> GetAllProperties(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _devicePropertyRepository.GetAllProperties(tenantId);
        }
    }
}
