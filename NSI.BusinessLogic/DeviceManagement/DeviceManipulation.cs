using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Domain.IncidentManagement;
using NSI.Domain.Membership;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.DeviceManagement;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.DeviceManagement
{
    public class DeviceManipulation : IDeviceManipulation
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IIncidentRepository _incidentRepository;

        /// <summary>
        /// Constructor for Device Manipulation
        /// </summary>
        /// <param name="deviceRepository"></param>
        /// <param name="incidentRepository"></param>
        public DeviceManipulation(IDeviceRepository deviceRepository, IIncidentRepository incidentRepository)
        {
            _deviceRepository = deviceRepository;
            _incidentRepository = incidentRepository;
        }

        /// <summary>
        /// Get all devices from system with specified tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> GetAllDevices(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceRepository.GetAllDevices(tenantId);
        }

        /// <summary>
        /// Get device by provided id
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public DeviceDomain GetDeviceById(int deviceId)
        {
            if (deviceId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidId);

            return _deviceRepository.GetDeviceById(deviceId);
        }

        /// <summary>
        /// Create device
        /// </summary>
        /// <param name="device"></param>
        /// <param name="user">This parameter is provided so that we can know which user created device.</param>
        /// <returns></returns>
        public int CreateDevice(CreateDeviceDomain device, UserDomain user)
        {
            if (device == null || user == null) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceRepository.CreateDevice(device, user);
        }

        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="device"></param>
        /// <param name="user">This parameter is provided so that we can know which user updated device.</param>
        /// <returns></returns>
        public int UpdateDevice(UpdateDeviceDomain device, UserDomain user)
        {
            if (device == null || user == null) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceRepository.UpdateDevice(device, user);
        }

        /// <summary>
        /// Get all active devices from system with specified tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> GetAllActiveDevices(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceRepository.GetAllActiveDevices(tenantId);
        }

        /// <summary>
        /// Get all inactive devices from system with specified tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> GetAllInactiveDevices(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceRepository.GetAllInactiveDevices(tenantId);
        }

        /// <summary>
        /// Delete Device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="user">This parameter is provided so that we can know which user deleted device.</param>
        /// <returns></returns>
        public bool DeleteDevice(int deviceId, UserDomain user)
        {
            if (deviceId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidId);

            return _deviceRepository.DeleteDevice(deviceId, user);
        }

        /// <summary>
        /// Search Devices
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="s">search word</param>
        /// <returns></returns>
        public ICollection<DeviceDomain> SearchDevices(int tenantId, String s)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceRepository.SearchDevices(tenantId, s);
        }

        /// <summary>
        /// Get number of incidents for specific deviceId in last X days
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="periodInDays"></param>
        /// <returns></returns>
        public int GetNumberOfIncidents(int deviceId, int periodInDays, int tenantId)
        {
            if(deviceId <= 0 || periodInDays <= 0 || tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            List<IncidentDomain> listOfIncidents = _incidentRepository.GetAllIncidents(tenantId)
                    .Where(x => x.Device.DeviceId == deviceId && x.DateCreated >= DateTime.Now.AddDays(-periodInDays)).ToList();

            return listOfIncidents != null ? listOfIncidents.Count : 0;
        }

        /// <summary>
        /// Search devices by specific word with filter applied
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="s"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public ICollection<DeviceDomain> SearchDevices(int tenantId, String s, bool isActive)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            return _deviceRepository.SearchDevices(tenantId, s, isActive);
        }
    }
}
