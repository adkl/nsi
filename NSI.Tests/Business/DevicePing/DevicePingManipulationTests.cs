using MassTransit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using Nsi.TestsCore.Mocks.DeviceManagement;
using Nsi.TestsCore.Mocks.DevicePing;
using NSI.BusinessLogic.DeviceManagement;
using NSI.BusinessLogic.DevicePing;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Domain.DevicePing;
using NSI.Queue.MessageConsumers;
using NSI.Repository.Interfaces.DeviceManagement;
using NSI.Repository.Interfaces.DevicePing;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.DevicePing
{
    [TestClass]
    public class DevicePingManipulationTests
    {
        private Mock<IDevicePingRepository> _devicePingRepositoryMock;
        private Mock<IDeviceRepository> _deviceRepositoryMock;
        private Mock<IIncidentRepository> _incidentRepositoryMock;
        private DevicePingManipulation _devicePingManipulation;
        private DeviceManipulation _deviceManipulation;

        #region Tests Initialization
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

                // Define message queues and bind consumers to queue here
                cfg.ReceiveEndpoint(host, "message_log_queue", e =>
                {
                    e.Consumer<LogMessageReceivedConsumer>();
                });

                cfg.ReceiveEndpoint(host, "device_ping_queue", e =>
                {
                    e.Consumer<DevicePingReceivedConsumer>();
                });
            });

            _devicePingRepositoryMock = DevicePingRepositoryMock.GetDevicePingRepositoryMock();
            _deviceRepositoryMock = DeviceRepositoryMock.GetDeviceRepositoryMock();
            _incidentRepositoryMock = IncidentRepositoryMock.GetIncidentRepositoryMock();

            _deviceManipulation = new DeviceManipulation(
                _deviceRepositoryMock.Object,
                _incidentRepositoryMock.Object
            );
        
            _devicePingManipulation = new DevicePingManipulation(
                _devicePingRepositoryMock.Object,
                _deviceManipulation,
                busControl
            );
        }
        #endregion

        #region Get Device Ping By ID - Tests
        [TestMethod, TestCategory("Device Pings - Get Device Ping By Id")]
        public void GetDevicePingById_Success()
        {
            DevicePingDomain devicePing = _devicePingManipulation.GetById(1, 1);
            Assert.AreEqual(1, devicePing.Id);
        }

        [TestMethod, TestCategory("Device Pings - Get Device Ping By Id")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Id must be a non-negative integer.")]
        public void GetDevicePingById_Fail_InvalidDevicePingId()
        {
            _devicePingManipulation.GetById(1, -1);
        }
        #endregion

        #region Add Device Ping - Tests
        [TestMethod, TestCategory("Device Ping - Add Device Ping")]
        public void AddDevicePing_Success()
        {
            int devicePingId = _devicePingManipulation.Add(1, GetValidDevicePropertyValues());
            Assert.AreEqual(1, devicePingId);
        }

        [TestMethod, TestCategory("Device Ping - Add Device Ping")]
        [ExpectedException(typeof(ArgumentNullException), "Provided list of device property values is not valid.")]
        public void AddDevicePing_Fail_InvalidDevicePropertyValues()
        {
            _devicePingManipulation.Add(1, null);
        }

        [TestMethod, TestCategory("Device Ping - Add Device Ping")]
        [ExpectedException(typeof(ArgumentException), "Provided list of device property values is not valid.")]
        public void AddDevicePing_Fail_EmptyDevicePropertyValues()
        {
            _devicePingManipulation.Add(1, new List<DevicePropertyValue>());
        }
        #endregion

        #region Delete Device Ping - Tests
        [TestMethod, TestCategory("Devices - Delete Device")]
        public void DeleteDevicePing_Success()
        {
            bool isDeleted = _devicePingManipulation.Delete(1, 1);
            Assert.AreEqual(true, isDeleted);
        }

        [TestMethod, TestCategory("Devices - Delete Device")]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Device ping id must be a non-negative integer.")]
        public void DeleteDevicePing_Fail_InvalidDevicePingId()
        {
            _devicePingManipulation.Delete(1, -1);
        }
        #endregion

        #region Search Device Pings - Tests
        [TestMethod, TestCategory("Device Ping - Search Device Pings")]
        public void SearchDevicePings_Success()
        {
            IEnumerable<DevicePingDomain>devicePings = _devicePingManipulation.Search(1, GetPaging(), GetFilterCriteria(), GetSortCriteria());

            Assert.AreEqual(3, devicePings.Count());
        }

        [TestMethod, TestCategory("Device Ping - Search Device Pings")]
        [ExpectedException(typeof(ArgumentNullException), "Provided argument is not valid.")]
        public void SearchDevicePings_Fail_InvalidPaging()
        {
            _devicePingManipulation.Search(1, null, GetFilterCriteria(), GetSortCriteria());
        }
        #endregion

        #region Get Last Device Ping For Device - Tests
        [TestMethod, TestCategory("Device Ping - Get Last Device Ping For Device")]
        public void DevicesLastPings_Success()
        {
            IEnumerable<DevicePingDomain> devicePings = _devicePingManipulation.DevicesLastPings(1);

            Assert.AreEqual(1, devicePings.Count());
        }
        #endregion

        #region GetAllDeviceProperties
        [TestMethod, TestCategory("Device Ping - Get All Device Properties")]
        public void GetAllDeviceProperties_Success()
        {
            string value = _devicePingManipulation.GetLastValue(1);
            Assert.AreEqual("TestValue", value);
        }
        #endregion

        #region Valid Domain Ping Models
        private List<DevicePropertyValue> GetValidDevicePropertyValues()
        {
            return new List<DevicePropertyValue>
            {
                new DevicePropertyValue
                {
                    Id = 1,
                    DeviceId = 1,
                    DevicePingId = 1,
                    PropertyId = 1,
                    TenantId = 1,
                    Value = "Test Property 1"
                },
                new DevicePropertyValue
                {
                    Id = 2,
                    DeviceId = 1,
                    DevicePingId = 1,
                    PropertyId = 2,
                    TenantId = 1,
                    Value = "Test Property 2"
                }
            };
        }
        #endregion

        #region Common Models
        private Paging GetPaging()
        {
            return new Paging
            {
                Page = 1,
                Pages = 1,
                RecordsPerPage = 5,
                TotalRecords = 3
            };
        }

        private List<FilterCriteria> GetFilterCriteria()
        {
            return new List<FilterCriteria> {
                new FilterCriteria
                {
                    ColumnName = "deviceName",
                    FilterTerm = "Device1",
                    IsExactMatch = false
                }
            };
        }

        private List<SortCriteria> GetSortCriteria()
        {
            return new List<SortCriteria>
            {
                new SortCriteria
                {
                    ColumnName = "deviceName",
                    Order = System.Data.SqlClient.SortOrder.Ascending,
                    Priority = 1
                }
            };
        }
        #endregion
    }
}
