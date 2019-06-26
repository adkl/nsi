using MassTransit;
using NSI.Domain.DeviceManagement;
using NSI.Domain.DevicePing;
using NSI.Queue.Messages.Events;
using NSI.Repository.Interfaces.DeviceManagement;
using System;

namespace NSI.BusinessLogic.DevicePing
{
    public class PingDeviceManipulation : IPingDeviceManipulation
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public PingDeviceManipulation(IDeviceRepository deviceRepository, IPublishEndpoint publishEndpoint)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException("deviceRepository can't be null");
            _publishEndpoint = publishEndpoint;
        }

        public void PingDevice(int tenantId, PingDeviceDomain pingDeviceDomain)
        {
            if (tenantId < 0)
            {
                throw new ArgumentNullException("Tennat Id must be set and valid");
            }

            if (pingDeviceDomain is null)
            {
                throw new ArgumentNullException("PingDeviceDomain cannot be null");
            }

            if (pingDeviceDomain.DeviceId <= 0)
            {
                throw new ArgumentNullException("Device Id must be valid");
            }

            if (pingDeviceDomain.ActionId <= 0)
            {
                throw new ArgumentNullException("Action Id must be valid");
            }

            if (string.Equals(pingDeviceDomain.Content, string.Empty))
            {
                throw new ArgumentException("Cannot send empty content to device");
            }

            // Get device and check if it is active
            DeviceDomain device = this._deviceRepository.GetDeviceById(pingDeviceDomain.DeviceId);

            if(!device.IsActive)
            {
                throw new ArgumentException("Device is no longer active");
            }

            _publishEndpoint.Publish<IPingDeviceReceived>(new
            {
                PingDeviceId = Guid.NewGuid(),
                TenantId = tenantId,
                DeviceId = pingDeviceDomain.DeviceId,
                ActionId = pingDeviceDomain.ActionId,
                Content = pingDeviceDomain.Content,
                Timestamp = DateTime.Now
            });
        }
    }
}
