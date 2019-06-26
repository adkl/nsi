using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Mocks.DeviceManagement;
using NSI.Api.Controllers;
using NSI.BusinessLogic.Interfaces.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.Domain.IncidentManagement;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace NSI.Tests.Controllers.DeviceMangement
{
    [TestClass]
    public class DeviceControllerTests
    {
        private Mock<IDeviceManipulation> _deviceManipulationMock;
        private DeviceController _deviceController;

        [TestInitialize]
        public void Initialize()
        {
            _deviceManipulationMock = DeviceManipulationMock.GetDeviceManipulationMock();
            _deviceController = new DeviceController(
                _deviceManipulationMock.Object
                );
            _deviceController.ControllerContext.Request = new HttpRequestMessage();
        }

        #region Devices - Get Device By Id
        [TestMethod, TestCategory("Devices - Get Device By Id")]
        public void GetDeviceById_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<DeviceDomain>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DeviceId);
        }

        [TestMethod, TestCategory("Devices - Get Device By Id")]
        public void GetDeviceById_Failed_NotAuthorized()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomainWithDifferentTenant();

            // Act
            IHttpActionResult actionResult = _deviceController.Get(1);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Unauthorized, contentResult.StatusCode);
            Assert.AreEqual("You are not authorized for this action.", contentResult.Content);
        }

        [TestMethod, TestCategory("Devices - Get Device By Id")]
        public void GetDeviceById_Failed_DeviceNotFound()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.Get(55);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
            Assert.AreEqual("Device is not found.", contentResult.Content);
        }

        [TestMethod, TestCategory("Devices - Get Device By Id")]
        public void GetDeviceById_Failed_InvalidDeviceId()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.Get(-1);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }
        #endregion

        #region Devices - Get All Devices With Pagination
        [TestMethod, TestCategory("Devices - Get All Devices With Pagination")]
        public void GetAllDevicesWithPagination_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.Get(1, 1);

            // Assert
            Assert.IsNotNull(actionResult);
        }
        #endregion

        #region Devices - Get All Devices
        [TestMethod, TestCategory("Devices - Get All Devices")]
        public void GetAllDevices_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<ICollection<DeviceDomain>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count);
        }

        [TestMethod, TestCategory("Devices - Get All Devices")]
        public void GetAllDevices_Failed_NotAuthorized()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomainWithDifferentTenant().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<ICollection<DeviceDomain>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNull(contentResult.Content);
        }

        #endregion

        #region Devices - Create Device
        [TestMethod, TestCategory("Devices - Create Device")]
        public void CreateDevice_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.CreateDevice(GetValidCreateDeviceDomain());
            var contentResult = actionResult as NegotiatedContentResult<int>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Created, contentResult.StatusCode);
            Assert.AreEqual(1, contentResult.Content);

        }

        [TestMethod, TestCategory("Devices - Create Device")]
        public void CreateDevice_Failed_InvalidArgument()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();
            CreateDeviceDomain device = GetValidCreateDeviceDomain();
            device.Name = null;

            // Act
            IHttpActionResult actionResult = _deviceController.CreateDevice(device);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);

        }

        [TestMethod, TestCategory("Devices - Create Device")]
        public void CreateDevice_Failed()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = null;
            CreateDeviceDomain device = GetValidCreateDeviceDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.CreateDevice(device);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Creating new Device failed.", contentResult.Content);

        }
        #endregion

        #region Devices - Update Device
        [TestMethod, TestCategory("Devices - Update Device")]
        public void UpdateDevice_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.UpdateDevice(GetValidUpdateDeviceDomain());
            var contentResult = actionResult as OkNegotiatedContentResult<int>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content);

        }

        [TestMethod, TestCategory("Devices - Update Device")]
        public void UpdateDevice_Failed_InvalidArgument()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();
            UpdateDeviceDomain device = GetValidUpdateDeviceDomain();
            device.Name = null;

            // Act
            IHttpActionResult actionResult = _deviceController.UpdateDevice(device);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);

        }

        [TestMethod, TestCategory("Devices - Update Device")]
        public void UpdateDevice_Failed()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = null;
            UpdateDeviceDomain device = GetValidUpdateDeviceDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.UpdateDevice(device);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Updating device failed.", contentResult.Content);

        }
        #endregion

        #region Devices - Delete Device
        [TestMethod, TestCategory("Devices - Delete Device")]
        public void DeleteDevice_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.Delete(1);
            var contentResult = actionResult as OkNegotiatedContentResult<bool>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content);

        }

        [TestMethod, TestCategory("Devices - Delete Device")]
        public void DeleteDevice_Failed_InvalidArgument()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceController.Delete(-5);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }

        [TestMethod, TestCategory("Devices - Update Device")]
        public void DeleteDevice_Failed()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserDetails"] = null;

            // Act
            IHttpActionResult actionResult = _deviceController.Delete(55);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
            Assert.AreEqual("Updating device failed.", contentResult.Content);
        }
        #endregion

        #region Devices - Get Number Of Incidents
        [TestMethod, TestCategory("Devices - Get Number Of Incidents")]
        public void GetNumberOfIncidents_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.GetNumberOfIncidents(1, 7);
            var contentResult = actionResult as OkNegotiatedContentResult<int>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(10, contentResult.Content);
        }

        [TestMethod, TestCategory("Devices - Get Number Of Incidents")]
        public void GetNumberOfIncidents_Failed_InvalidDeviceId()
        {
            // Act
            IHttpActionResult actionResult = _deviceController.GetNumberOfIncidents(-5, 5);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }

        [TestMethod, TestCategory("Devices - Get Number Of Incidents")]
        public void GetNumberOfIncidents_Failed_InvalidPeriodOfDays()
        {
            // Act
            IHttpActionResult actionResult = _deviceController.GetNumberOfIncidents(5, -5);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }
        #endregion

        #region Devices - Get All Active Devices With Pagination
        [TestMethod, TestCategory("Devices - Get All Active Devices With Pagination")]
        public void GetAllActiveDevicesWithPagination_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.GetActiveDevices(1, 1);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        #endregion

        #region Devices - Get All Inactive Devices With Pagination
        [TestMethod, TestCategory("Devices - Get All Inactive Devices With Pagination")]
        public void GetAllInactiveDevicesWithPagination_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.GetInactiveDevices(1, 1);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        #endregion

        #region Devices - Search Devices
        [TestMethod, TestCategory("Devices - Search Devices")]
        public void SearchDevices_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.SearchDevices(1, 1, "Device 2");

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod, TestCategory("Devices - Search Devices")]
        public void SearchDevicesWithFilter_Success()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.SearchDevices(1, 1, "Device", true, true);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod, TestCategory("Devices - Search Devices")]
        public void SearchDevices_Failed_InvalidArgument()
        {
            //Arrange
            _deviceController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceController.SearchDevices(0, 0, "Device", true, true);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }

        #endregion

        #region Valid Domain Models
        private CreateDeviceDomain GetValidCreateDeviceDomain()
        {
            return new CreateDeviceDomain()
            {
                Name = "Device Test",
                Description = "Test",
                DeviceTypeId = 1,
                DeviceImage = "FakeUrl"
            };
        }

        private UpdateDeviceDomain GetValidUpdateDeviceDomain()
        {
            return new UpdateDeviceDomain()
            {
                DeviceId = 2,
                Name = "Device Test",
                Description = "Test",
                DeviceTypeId = 1,
                DeviceImage = "FakeUrl",
                IsActive = false,
                IsDeleted = true
            };
        }

        private UserDomain GetValidUserDomain()
        {
            return new UserDomain()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                TenantId = 1,
                IsActive = true
            };
        }

        private UserDomain GetValidUserDomainWithDifferentTenant()
        {
            return new UserDomain()
            {
                FirstName = "Test 2",
                LastName = "Test 2",
                Email = "test2@test.com",
                TenantId = 2,
                IsActive = true
            };
        }

        private List<IncidentDomain> GetListOfValidIncidentDomains()
        {
            return new List<IncidentDomain>() {
                new IncidentDomain()
                {
                    Device = new DeviceDomain
                    {
                        DeviceId = 1
                    },
                    DateCreated = DateTime.Now
                },
                new IncidentDomain()
                {
                    Device = new DeviceDomain
                    {
                        DeviceId = 1
                    },
                    DateCreated = DateTime.Now.AddDays(-10)
                }
            };
        }
        #endregion
    }
}
