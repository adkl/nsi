using MassTransit;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.BusinessLogic.Interfaces.DevicePing;
using NSI.Common.Models;
using NSI.Domain.DevicePing;
using NSI.Domain.Membership;
using NSI.Queue.Messages.Events;
using NSI.Repository.Interfaces.DevicePing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NSI.BusinessLogic.DevicePing
{
    public class DevicePingManipulation : IDevicePingManipulation
    {
        private readonly IDevicePingRepository _devicePingRepository;
        private readonly IDeviceManipulation _deviceManipulation;
        private readonly IPublishEndpoint _publishEndpoint;
    
        public DevicePingManipulation(IDevicePingRepository devicePingRepository, IDeviceManipulation deviceManipulation, IPublishEndpoint publishEndpoint)
        {
            _devicePingRepository = devicePingRepository ?? throw new ArgumentNullException("devicePingRepository can't be null");
            _deviceManipulation = deviceManipulation ?? throw new ArgumentNullException("deviceManipulation can't be null");
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException("publishEndpoint can't be null"); ;
        }

        public int Add(int tenantId, List<DevicePropertyValue> devicePropertyValues)
        {
            if (devicePropertyValues == null)
            {
                throw new ArgumentNullException("devicePropertyValues cannot be null");
            }

            if(devicePropertyValues.Count == 0)
            {
                throw new ArgumentException("devicePropertyValues cannot be an empty list");
            }

            devicePropertyValues.ForEach(x => x.DateCreated = DateTime.Now);

            DevicePingDomain devicePing = new DevicePingDomain
            {
                DateCreated = DateTime.Now,
                DevicePropertyValues = devicePropertyValues,
                DeviceId = devicePropertyValues[0].DeviceId,
                TenantId = tenantId,
                ActionId = 1,
                RuleId = null
            };

            var createdDevicePingId = _devicePingRepository.AddDevicePing(tenantId, devicePing);
            var cretedDevicePing = _devicePingRepository.GetDevicePingById(tenantId, createdDevicePingId);

            if (cretedDevicePing != null)
            {
                _publishEndpoint.Publish<IDevicePingReceived>(new
                {
                    MessageId = Guid.NewGuid(),
                    Timestamp = DateTime.Now,
                    DevicePing = cretedDevicePing
                });
            }

            return createdDevicePingId;
        }

        public bool Delete(int tenantId, int devicePingId)
        {
            if (devicePingId < 0)
            {
                throw new ArgumentOutOfRangeException("devicePingId index must be a non-negative integer");
            }

            return _devicePingRepository.DeleteDevicePingById(tenantId, devicePingId);
        }

        public IEnumerable<DevicePingDomain> Search(int tenantId, Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            if (paging == null)
            {
                throw new ArgumentNullException("paging cannot be null");
            }

            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());

            return _devicePingRepository.Search(tenantId, paging, filterCriteria, sortCriteria);
        }

        public DevicePingDomain GetById(int tenantId, int devicePingId)
        {
            if (devicePingId < 0)
            {
                throw new ArgumentOutOfRangeException("devicePingId index must be a non-negative integer");
            }

            return _devicePingRepository.GetDevicePingById(tenantId, devicePingId);
        }

        public int DevicePingsCount()
        {
            return _devicePingRepository.DevicePingsCount();
        }

        public IEnumerable<DevicePingDomain> DevicesLastPings(int tenantId)
        {
            List<DevicePingDomain> devicePings = new List<DevicePingDomain>();
            var devices = this._deviceManipulation.GetAllActiveDevices(tenantId).ToList();

            devices.ForEach(device =>
            {
                devicePings.Add(_devicePingRepository.LastDevicePingForDevice(device.DeviceId));
            });

            return devicePings;
        }

        public string GetLastValue(int id) {
           IEnumerable<DevicePropertyValue> dpValues = _devicePingRepository.GetAllDeviceProperties();

           string lastValue = null;
           DateTime lastPing = new DateTime();

           foreach (DevicePropertyValue dpValue in dpValues) {
               if (dpValue.PropertyId == id)
               {
                   if (lastValue == null) {
                       lastValue = dpValue.Value;
                       lastPing = dpValue.DateCreated;
                   }
                   else if (DateTime.Compare(lastPing, dpValue.DateCreated) <= 0) {
                       lastPing = dpValue.DateCreated;
                       lastValue = dpValue.Value;

                   }

               }
           }


           return (lastValue == null) ? "Enter value!" : lastValue;
       }
    }
}
