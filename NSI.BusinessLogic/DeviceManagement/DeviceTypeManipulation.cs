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
    public class DeviceTypeManipulation : IDeviceTypeManipulation
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;

        /// <summary>
        /// Constructor for DeviceTypeManipulation
        /// </summary>
        /// <param name="deviceTypeRepository"></param>
        public DeviceTypeManipulation(IDeviceTypeRepository deviceTypeRepository)
        {
            _deviceTypeRepository = deviceTypeRepository;
        }

        /// <summary>
        /// Get all device types
        /// </summary>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> GetAllDeviceTypes()
        {
            return _deviceTypeRepository.GetAllDeviceTypes();
        }

        /// <summary>
        /// Get Device type by provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DeviceTypeDomain GetDeviceTypeById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeInvalidArgument);

            return _deviceTypeRepository.GetDeviceTypeById(id);
        }

        /// <summary>
        /// Create device type
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public int CreateDeviceType(DeviceTypeDomain deviceType)
        {
            if(deviceType == null) throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeInvalidArgument);

            return _deviceTypeRepository.CreateDeviceType(deviceType);
        }

        /// <summary>
        /// Update device type
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public int UpdateDeviceType(DeviceTypeDomain deviceType)
        {
            if (deviceType == null) throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeInvalidArgument);

            return _deviceTypeRepository.UpdateDeviceType(deviceType);
        }

        /// <summary>
        /// Delete device type by id
        /// </summary>
        /// <param name="deviceTypeId"></param>
        /// <returns></returns>
        public bool DeleteDeviceType(int deviceTypeId)
        {
            if (deviceTypeId <= 0) throw new NsiArgumentException(DeviceTypeMessages.DeviceTypeInvalidArgument);

            return _deviceTypeRepository.DeleteDeviceType(deviceTypeId);
        }

        /// <summary>
        /// Get all active device types
        /// </summary>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> GetAllActiveDeviceTypes()
        {
            return _deviceTypeRepository.GetAllActiveDeviceTypes();
        }

        /// <summary>
        /// Get all inactive device types
        /// </summary>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> GetAllInactiveDeviceTypes()
        {
            return _deviceTypeRepository.GetAllInactiveDeviceTypes();
        }

        /// <summary>
        /// Search devices
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> SearchDeviceTypes(String s)
        {
            return _deviceTypeRepository.SearchDeviceTypes(s);
        }

        /// <summary>
        /// Search devices with filter applied
        /// </summary>
        /// <param name="s"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public ICollection<DeviceTypeDomain> SearchDeviceTypes(String s, bool isActive)
        {
            return _deviceTypeRepository.SearchDeviceTypes(s, isActive);
        }
    }
}
