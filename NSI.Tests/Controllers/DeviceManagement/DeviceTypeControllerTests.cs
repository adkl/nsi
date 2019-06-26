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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace NSI.Tests.Controllers.DeviceMangement
{
    [TestClass]
    public class DeviceTypeControllerTests
    {
        private Mock<IDeviceTypeManipulation> _deviceTypeManipulationMock;
        private DeviceTypeController _deviceTypeController;

        [TestInitialize]
        public void Initialize()
        {
            _deviceTypeManipulationMock = DeviceTypeManipulationMock.GetDeviceTypeManipulationMock();
            _deviceTypeController = new DeviceTypeController(
                _deviceTypeManipulationMock.Object
                );

            _deviceTypeController.ControllerContext.Request = new HttpRequestMessage();
        }

        #region Devices - Get Device Type By Id
        [TestMethod, TestCategory("Device Types - Get Device Type By Id")]
        public void GetDeviceTypeById_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<DeviceTypeDomain>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.DeviceTypeId);
        }

        [TestMethod, TestCategory("Device Types - Get Device Type By Id")]
        public void GetDeviceTypeById_Failed_DeviceTypeNotFound()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.Get(55);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
            Assert.AreEqual("Device Type is not found.", contentResult.Content);
        }

        [TestMethod, TestCategory("Device Types - Get Device Type By Id")]
        public void GetDeviceTypeById_Failed_InvalidDeviceTypeId()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.Get(-1);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }
        #endregion

        #region Devices - Get All Device Types With Pagination
        [TestMethod, TestCategory("Device Types - Get All Device Types With Pagination")]
        public void GetAllDeviceTypesWithPagination_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceTypeController.Get(1, 1);

            // Assert
            Assert.IsNotNull(actionResult);
        }
        #endregion

        #region Devices - Get All Device Types
        [TestMethod, TestCategory("Devices - Get All Devices")]
        public void GetAllDevices_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            List<DeviceTypeDomain> result = _deviceTypeController.Get().ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
        #endregion

        #region Device Types - Create Device Type
        [TestMethod, TestCategory("Device Types - Create Device Type")]
        public void CreateDeviceType_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.CreateDeviceType(GetValidDeviceTypeDomain());
            var contentResult = actionResult as OkNegotiatedContentResult<int>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content);

        }

        [TestMethod, TestCategory("Device Types - Create Device Type")]
        public void CreateDeviceType_Failed_InvalidArgument()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.CreateDeviceType(null);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);

        }

        #endregion

        #region Device Types - Update Device Type
        [TestMethod, TestCategory("Device Types - Update Device Type")]
        public void UpdateDeviceType_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.UpdateDeviceType(GetValidDeviceTypeDomain());
            var contentResult = actionResult as OkNegotiatedContentResult<int>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult.Content);

        }

        [TestMethod, TestCategory("Device Types - Update Device Type")]
        public void UpdateDeviceType_Failed_InvalidArgument()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.UpdateDeviceType(null);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);

        }
        #endregion

        #region Device Types - Delete Device Type
        [TestMethod, TestCategory("Device Types - Delete Device Type")]
        public void DeleteDeviceType_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.Delete(1);
            var contentResult = actionResult as OkNegotiatedContentResult<bool>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsTrue(contentResult.Content);

        }

        [TestMethod, TestCategory("Device Types - Delete Device Type")]
        public void DeleteDeviceType_Failed_InvalidArgument()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = GetValidUserDomain();

            // Act
            IHttpActionResult actionResult = _deviceTypeController.Delete(-5);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }

        [TestMethod, TestCategory("Device Types - Delete Device Type")]
        public void DeleteDeviceType_Failed()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserDetails"] = null;

            // Act
            IHttpActionResult actionResult = _deviceTypeController.Delete(55);
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
            Assert.AreEqual("Device Type is not found.", contentResult.Content);
        }
        #endregion

        #region Device Types - Get All Active Device Types With Pagination
        [TestMethod, TestCategory("Devices - Get All Active Device Types With Pagination")]
        public void GetAllActiveDeviceTypesWithPagination_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceTypeController.GetActiveDeviceTypes(1, 1);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        #endregion

        #region Devices - Get All Inactive Device Types With Pagination
        [TestMethod, TestCategory("Devices - Get All Inactive Device Types With Pagination")]
        public void GetAllInactiveDeviceTypesWithPagination_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceTypeController.GetInactiveDeviceTypes(1, 1);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        #endregion

        #region Device Types - Search Device Types
        [TestMethod, TestCategory("Device Types - Search Device Types")]
        public void SearchDeviceTypes_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceTypeController.SearchDeviceTypes(1, 1, "Device Type 2");

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod, TestCategory("Device Types - Search Device Types")]
        public void SearchDevicesWithFilter_Success()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceTypeController.SearchDeviceTypes(1, 1, "Device Type", true, true);

            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod, TestCategory("Device Types - Search Device Types")]
        public void SearchDeviceTypes_Failed_InvalidArgument()
        {
            //Arrange
            _deviceTypeController.ControllerContext.Request.Properties["UserTenantId"] = GetValidUserDomain().TenantId;

            // Act
            IHttpActionResult actionResult = _deviceTypeController.SearchDeviceTypes(0, 0, "Device");
            var contentResult = actionResult as NegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.BadRequest, contentResult.StatusCode);
            Assert.AreEqual("Provided argument is not valid.", contentResult.Content);
        }

        #endregion

        #region Valid Domain Models
        private DeviceTypeDomain GetValidDeviceTypeDomain()
        {
            return new DeviceTypeDomain()
            {
                DeviceTypeId = 1,
                Name = "Test Device Type 1",
                Code = "100",
                Actions = new List<ActionDomain>(),
                Properties = new List<PropertyDomain>(),
                IsActive = true
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
        #endregion
    }
}
