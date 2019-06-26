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
    public class DeviceActionManipulation : IDeviceActionManipulation
    {
        private readonly IDeviceActionRepository _deviceActionRepository;

        /// <summary>
        /// Constructor for DeviceActionManipulation
        /// </summary>
        /// <param name="deviceActionRepository"></param>
        public DeviceActionManipulation(IDeviceActionRepository deviceActionRepository)
        {
            _deviceActionRepository = deviceActionRepository;
        }

        /// <summary>
        /// Get all device type actions for specific tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<ActionDomain> GetAllActions(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceActionRepository.GetAllActions(tenantId);
        }
    }
}
