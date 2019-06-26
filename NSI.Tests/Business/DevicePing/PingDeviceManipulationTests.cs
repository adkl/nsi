using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Mocks.DeviceManagement;
using Nsi.TestsCore.Mocks.DevicePing;
using NSI.BusinessLogic.DevicePing;
using NSI.Domain.DevicePing;
using NSI.Queue.MessageConsumers;
using NSI.Repository.Interfaces.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.DevicePing
{
    [TestClass]
    public class PingDeviceManipulationTests
    {
        private Mock<IDeviceRepository> _deviceRepositoryMock;
        private PingDeviceManipulation _pingDeviceManipulation;

        [TestInitialize]
        public void Initialize()
        {
            // Queue setup
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(ConfigurationManager.AppSettings["rabbitMQHostUri"].ToString()), h =>
                {
                    h.Username(ConfigurationManager.AppSettings["rabbitMQUsername"].ToString());
                    h.Password(ConfigurationManager.AppSettings["rabbitMQPassword"].ToString());
                });

                cfg.ReceiveEndpoint(host, "ping_device_queue", e =>
                {
                    e.Consumer<PingDeviceReceivedConsumer>();
                });
            });

            _deviceRepositoryMock = PingDeviceRepositoryMock.GetDeviceRepositoryMock();
            _pingDeviceManipulation = new PingDeviceManipulation(
                _deviceRepositoryMock.Object,
                busControl
            );
        }

        #region Invalid ping parameters
        [TestMethod, TestCategory("Ping to device - Invalid ping parameters")]
        [ExpectedException(typeof(ArgumentNullException), "Provided tenat id must be a positive integer.")]
        public void PingDevice_Fail_InvalidTenantId()
        {
            int tenantId = -1;
            var pingDeviceDomain = new PingDeviceDomain()
            {
                DeviceId = 1,
                ActionId = 1,
                Content = "Lorem ipsum"
            };

            _pingDeviceManipulation.PingDevice(tenantId, pingDeviceDomain);
        }

        [TestMethod, TestCategory("Ping to device - Invalid ping parameters")]
        [ExpectedException(typeof(ArgumentNullException), "PingDeviceDomain cannot be null")]
        public void PingDevice_Fail_NoPingDeviceDomain()
        {
            int tenantId = 1;
            PingDeviceDomain pingDeviceDomain = null;

            _pingDeviceManipulation.PingDevice(tenantId, pingDeviceDomain);
        }

        [TestMethod, TestCategory("Ping to device - Invalid ping parameters")]
        [ExpectedException(typeof(ArgumentNullException), "Provided device id must be a positive integer.")]
        public void PingDevice_Fail_InvalidDeviceId()
        {
            int tenantId = 1;
            var pingDeviceDomain = new PingDeviceDomain()
            {
                DeviceId = -1,
                ActionId = 1,
                Content = "Lorem ipsum"
            };

            _pingDeviceManipulation.PingDevice(tenantId, pingDeviceDomain);
        }

        [TestMethod, TestCategory("Ping to device - Invalid ping parameters")]
        [ExpectedException(typeof(ArgumentNullException), "Provided action id must be a positive integer.")]
        public void PingDevice_Fail_InvalidActionId()
        {
            int tenantId = 1;
            var pingDeviceDomain = new PingDeviceDomain()
            {
                DeviceId = 1,
                ActionId = -1,
                Content = "Lorem ipsum"
            };

            _pingDeviceManipulation.PingDevice(tenantId, pingDeviceDomain);
        }

        [TestMethod, TestCategory("Ping to device - Invalid ping parameters")]
        [ExpectedException(typeof(ArgumentException), "Device must be active to access it.")]
        public void PingDevice_Fail_InactiveDevice()
        {
            int tenantId = 1;
            var pingDeviceDomain = new PingDeviceDomain()
            {
                DeviceId = 2,
                ActionId = 1,
                Content = "Lorem ipsum"
            };

            _pingDeviceManipulation.PingDevice(tenantId, pingDeviceDomain);
        }

        [TestMethod, TestCategory("Ping to device - Invalid ping parameters")]
        [ExpectedException(typeof(ArgumentException), "Provided content must not be empty.")]
        public void PingDevice_Fail_InvalidContent()
        {
            int tenantId = 1;
            var pingDeviceDomain = new PingDeviceDomain()
            {
                DeviceId = 1,
                ActionId = 1,
                Content = ""
            };

            _pingDeviceManipulation.PingDevice(tenantId, pingDeviceDomain);
        }
        #endregion
    }
}
