using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using NSI.BusinessLogic.Membership;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Membership
{
    [TestClass]
    public class RoleManipulationTests
    {

        private Mock<IRoleRepository> _roleRepositoryMock;
        private RoleManipulation _roleManipulation;
        private Mock<ITenantRepository> _tenantRepositoryMock;

        [TestInitialize]
        public void Initialize()
        {
            _roleRepositoryMock = RoleRepositoryMock.GetRoleRepositoryMock();
            _tenantRepositoryMock = TenantRepositoryMock.GetTenantRepositoryMock();
            _roleManipulation = new RoleManipulation(_roleRepositoryMock.Object, _tenantRepositoryMock.Object);
        }

        [TestMethod, TestCategory("Role - Get Role By Id")]
        public void GetRoleById_Success()
        {
            RoleDomain module = _roleManipulation.GetRoleById(1);
            Assert.AreEqual(1, module.Id);
        }
        
        [TestMethod, TestCategory("Role - Get Role By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided role ID is not valid.", SeverityEnum.Warning)]
        public void GetRoleById_Fail_InvalidId()
        {
            _roleManipulation.GetRoleById(-1);
        }
        
        [TestMethod, TestCategory("Role - Get All Roles")]
        public void GetAllRoles_Success()
        {
            _roleManipulation.GetAllRoles(6, null);
        }

        [TestMethod, TestCategory("Role - Get Role By Name")]
        public void GetRoleByName_Success()
        {
            RoleDomain module = _roleManipulation.GetRoleByName("Rola 1");
            Assert.AreEqual("Rola 1", module.Name);
        }

        [TestMethod, TestCategory("Role - Get Role By Name")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Role name is not provided.", SeverityEnum.Warning)]
        public void GetRoleByName_RoleNameNotProvided()
        {
            _roleManipulation.GetRoleByName("");
        }

        [TestMethod, TestCategory("Role - Get Role By Name")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Role name can only hace 50 characters maximum.", SeverityEnum.Warning)]
        public void GetRoleByName_RoleNameLenghtExceeded()
        {
            _roleManipulation.GetRoleByName("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        [TestMethod, TestCategory("Role - Add Role")]
        public void AddRole_Success()
        {
            int RoleId = _roleManipulation.AddRole(GetValidRoleDomain());
            Assert.AreEqual(1, RoleId);
        }
        
        [TestMethod, TestCategory("Language - Add Role")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Role details are not provided.", SeverityEnum.Warning)]
        public void AddRole_Fail_InvalidModule()
        {
            _roleManipulation.AddRole(null);
        }
        
        [TestMethod, TestCategory("Role - Add Role")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Role with provided name already exists.", SeverityEnum.Warning)]
        public void AddRole_Fail_RoleNameExists()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "Rola 1",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 1,
                Id = 1
            };
            _roleManipulation.AddRole(role);
        }

        [TestMethod, TestCategory("Role - Add Role")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Role name can only hace 50 characters maximum.", SeverityEnum.Warning)]
        public void AddRole_Fail_NameLengthExceeded()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "Rola 1 aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 1,
                Id = 1
            };
            _roleManipulation.AddRole(role);
        }

        [TestMethod, TestCategory("Role - Add Role")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Role name is not provided.", SeverityEnum.Warning)]
        public void AddRole_Fail_RoleNameNotProvided()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 1,
                Id = 1
            };
            _roleManipulation.AddRole(role);
        }

        [TestMethod, TestCategory("Role - Update Role")]
        public void UpdateRole_Success()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "Rola 5",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 1,
                Id = 1
            };
            _roleManipulation.UpdateRole(role);
        }

        [TestMethod, TestCategory("Role - Update Role")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided role ID is not valid.", SeverityEnum.Warning)]
        public void UpdateRole_Fail_ProvidedRoleIdNotValid()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "Rola 5",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 1,
                Id = -1
            };
            _roleManipulation.UpdateRole(role);
        }

        [TestMethod, TestCategory("Role - Update Role")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Role with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateRole_Fail_ProvidedRoleIDNotExist()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "Rola 5",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 1,
                Id = 5
            };
            _roleManipulation.UpdateRole(role);
        }

        [TestMethod, TestCategory("Role - Update Role")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Role details are not provided.", SeverityEnum.Warning)]
        public void UpdateRole_Fail_RoleIDNotProvided()
        {
            _roleManipulation.UpdateRole(null);
        }

        [TestMethod, TestCategory("Role - Update Role")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided tenant ID does not exist.", SeverityEnum.Warning)]
        public void UpdateRole_Fail_RoleTenantIdNotProvided()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "Rola 5",
                IsActive = false,
                ManipulationLogId = 0,
                Id = 1
            };
            _roleManipulation.UpdateRole(role);
        }

        [TestMethod, TestCategory("Role - Update Role")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided tenant ID does not exist.", SeverityEnum.Warning)]
        public void UpdateRole_Fail_ProvidedTenantIDNotExist()
        {
            RoleDomain role = new RoleDomain()
            {
                Name = "Rola 5",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 15,
                Id = 1
            };
            _roleManipulation.UpdateRole(role);
        }





        private RoleDomain GetValidRoleDomain()
        {
            return new RoleDomain()
            {
                Name = "Rola 3",
                IsActive = false,
                ManipulationLogId = 0,
                TenantId = 1,
                Id = 1
            };
        }

    }
}
