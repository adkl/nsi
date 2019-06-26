using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using Nsi.TestsCore.Mocks.DeviceManagement;
using NSI.BusinessLogic.DeviceManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.DeviceManagement;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.DeviceManagement;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;

namespace NSI.Tests.Business.DeviceMangement
{
    [TestClass]
    public class DeviceActionManipulationTests
    {
        private Mock<IDeviceActionRepository> _deviceActionRepositoryMock;
        private DeviceActionManipulation _deviceActionManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _deviceActionRepositoryMock = DeviceActionRepositoryMock.GetDeviceActionRepositoryMock();
            _deviceActionManipulation = new DeviceActionManipulation(
                _deviceActionRepositoryMock.Object
                );
        }

        #region Get All Device Actions - Tests
        [TestMethod, TestCategory("Device Actions - Get All Device Actions")]
        public void GetAlldeviceActions_Success()
        {
            UserDomain user = GetValidUserDomain();
            _deviceActionManipulation.GetAllActions(user.TenantId);
        }
        #endregion

        #region Valid Domain Models
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
        #endregion
    }
}
