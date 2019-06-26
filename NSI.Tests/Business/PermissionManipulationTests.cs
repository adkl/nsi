using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSI.Common.Exceptions;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NSI.Repository.Interfaces.Membership;
using Nsi.TestsCore.Extensions;
using NSI.Common.Enumerations;
using NSI.Resources.Membership;
using NSI.BusinessLogic.Membership;
using Nsi.TestsCore.Mocks;

namespace NSI.Tests.Business
{
    [TestClass]
    public class PermissionManipulationTests
    {
        private Mock<IPermissionRepository> _permissionRepositoryMock;
        private Mock<IModuleRepository> _moduleRepositoryMock;
        private PermissionManipulation _permissionManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _permissionRepositoryMock = PermissionRepositoryMock.GetPermissionRepositoryMock();
            _moduleRepositoryMock = ModuleRepositoryMock.GetModuleRepositoryMock();
            _permissionManipulation = new PermissionManipulation(
                _permissionRepositoryMock.Object, 
                _moduleRepositoryMock.Object
                );
        }

        [TestMethod, TestCategory("Permission - AddPermission")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Permission details are not provided.", SeverityEnum.Warning)]
        public void AddPermission_Fail_ParameterNull()
        {
            _permissionManipulation.AddPermission(null);
        }

        [TestMethod, TestCategory("Permission - AddPermission")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Permission code can only have 100 characters maximum.", SeverityEnum.Warning)]
        public void AddPermission_Fail_InvalidCode()
        {
            var permission = GetValidPermissionModel();
            permission.Code = "Dm2xXNOnmx7v6TwNiBfaDm2xXNOnmx7v6TwNiBfaDm2xXNOnmx7v6TwNiBfaDm2xXNOnmx7v6TwNiBfaDm2xXNOnmx7v6TwNiBfaDm2xXNOnmx7v6TwNiBfaDm2xXNOnmx7v6TwNiBfaDm2xXNOnmx7v6TwNiBfa";
            _permissionManipulation.AddPermission(permission);
        }

        [TestMethod, TestCategory("Permission - AddPermission")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Permission with provided code already exists.", SeverityEnum.Warning)]
        public void AddPermission_Fail_CodeExists()
        {
            var permission = GetValidPermissionModel();
            permission.Code = "TESTCODEEXISTS";
            _permissionManipulation.AddPermission(permission);
        }

        [TestMethod, TestCategory("Permission - AddPermission")]
        public void AddPermission_Success_ValidPermission()
        {
            var permission = GetValidPermissionModel();
            _permissionManipulation.AddPermission(permission);
        }

        [TestMethod, TestCategory("Permission - AddPermission")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided permision code is not valid.", SeverityEnum.Warning)]
        public void AddPermission_Fail_CodeNull()
        {
            var permission = GetValidPermissionModel();
            permission.Code = "";
            _permissionManipulation.AddPermission(permission);
        }

        [TestMethod, TestCategory("Permission - AddPermission")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Module wit provided ID does not exist.", SeverityEnum.Warning)]
        public void AddPermission_Fail_InvalidModuleId()
        {
            var permission = GetValidPermissionModel();
            permission.ModuleId = 5;
            _permissionManipulation.AddPermission(permission);
        }

        [TestMethod, TestCategory("Permission - Get All Permissions")]
        public void GetAllModules_Success()
        {
            _permissionManipulation.GetAllPermissions(null);
        }

        [TestMethod, TestCategory("Permission - Get Permission By Id")]
        public void GetModuleById_Success()
        {
            PermissionDomain module = _permissionManipulation.GetPermissionById(1);
            Assert.AreEqual(1, module.Id);
        }

        [TestMethod, TestCategory("Permission - Get Permission By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided permission ID is not valid.", SeverityEnum.Warning)]
        public void GetModuleById_InvalidPermissionId()
        {
            _permissionManipulation.GetPermissionById(-1);
        }

        [TestMethod, TestCategory("Permission - Get Permission By Code")]
        public void GetModuleByCode_Success()
        {
            PermissionDomain module = _permissionManipulation.GetPermissionByCode("TESTCODEEXISTS");
            Assert.AreEqual("TESTCODEEXISTS", module.Code);
        }

        [TestMethod, TestCategory("Permission - Get Permission By Code")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided permision code is not valid.", SeverityEnum.Warning)]
        public void GetModuleByCode_Fail_EmptyCode()
        {
            _permissionManipulation.GetPermissionByCode("");
        }

        [TestMethod, TestCategory("Permission - Update Permission")]
        public void UpdateModule_Success()
        {
            var permission = _permissionManipulation.GetPermissionById(1);
            permission.Code = "Updated code";
            _permissionManipulation.UpdatePermission(permission);
            Assert.AreEqual("Updated code", _permissionManipulation.GetPermissionById(1).Code);
        }

        [TestMethod, TestCategory("Permission - Update Permission")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Permission details are not provided.", SeverityEnum.Warning)]
        public void UpdateModule_Fail_NullValue()
        {
            _permissionManipulation.UpdatePermission(null);
        }

        private PermissionDomain GetValidPermissionModel()
        {
            return new PermissionDomain()
            {
                Code = "TESTDOESNOTEXIST",
                IsActive = true,
                ModuleId = 1,
                TenantId = 1
            };
        }
    }
}
