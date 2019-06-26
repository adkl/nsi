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
    public class ModuleManipulationTests
    {
        private Mock<IModuleRepository> _moduleRepositoryMock;
        private ModuleManipulation _moduleManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _moduleRepositoryMock = ModuleRepositoryMock.GetModuleRepositoryMock();
            _moduleManipulation = new ModuleManipulation(_moduleRepositoryMock.Object);
        }

        #region Get Module By ID 
        [TestMethod, TestCategory("Modules - Get Module By Id")]
        public void GetModuleById_Success()
        {
            ModuleDomain module = _moduleManipulation.GetModuleById(1);
            Assert.AreEqual(1, module.Id);
        }

        [TestMethod, TestCategory("Modules - Get Module By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided module ID is not valid.", SeverityEnum.Warning)]
        public void GetModuleById_Fail_InvalidModuleId()
        {
            _moduleManipulation.GetModuleById(-1);
        }
        #endregion

        #region Get All Modules - Tests
        [TestMethod, TestCategory("Devices - Get All Modules")]
        public void GetAllModules_Success()
        {
            _moduleManipulation.GetAllModules();
        }
        #endregion

        #region Get Module By Code
        [TestMethod, TestCategory("Modules - Get Module By Code")]
        public void GetModuleByCode_Success()
        {
            ModuleDomain module = _moduleManipulation.GetModuleByCode("TestModule");
            Assert.AreEqual("TestModule", module.Code);
        }

        [TestMethod, TestCategory("Modules - Get Module By Code")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided module code is not valid.", SeverityEnum.Warning)]
        public void GetModuleByCode_Fail_EmptyCode()
        {
            _moduleManipulation.GetModuleByCode("");
        }
        #endregion

        #region Add Module
        [TestMethod, TestCategory("Modules - Add Module")]
        public void AddModule_Success()
        {
            int moduleId = _moduleManipulation.AddModule(GetValidModuleDomain());
            Assert.AreEqual(1, moduleId);
        }

        [TestMethod, TestCategory("Modules - Add Module")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Module details are not provided.", SeverityEnum.Warning)]
        public void AddModule_Fail_InvalidModule()
        {
            _moduleManipulation.AddModule(null);
        }

        [TestMethod, TestCategory("Modules - Add Module")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Module code can only have 100 characters maximum.", SeverityEnum.Warning)]
        public void AddModule_Fail_CodeLengthExceeded()
        {
            ModuleDomain module = new ModuleDomain()
            {
                Code = "TestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModule",
                Id = 1,
                IsActive = true,
                TenantId = 1,
                Name = "Test Module"
            };
            _moduleManipulation.AddModule(module);
        }

        [TestMethod, TestCategory("Modules - Add Module")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided module code is not valid.", SeverityEnum.Warning)]
        public void AddModule_Fail_CodeInvalid()
        {
            ModuleDomain module = new ModuleDomain()
            {
                Code = null,
                Id = 1,
                IsActive = true,
                TenantId = 1,
                Name = "Test Module"
            };
            _moduleManipulation.AddModule(module);
        }

        [TestMethod, TestCategory("Modules - Add Module")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Module with provided code already exists.", SeverityEnum.Warning)]
        public void AddModule_Fail_CodeAlreadyExists()
        {
            var module = GetValidModuleDomain();
            module.Code = "TestModule";
            _moduleManipulation.AddModule(module);
        }

        [TestMethod, TestCategory("Modules - Add Module")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided module name is not valid.", SeverityEnum.Warning)]
        public void AddModule_Fail_NameInvalid()
        {
            ModuleDomain module = new ModuleDomain()
            {
                Code = "TestModule2",
                Id = 1,
                IsActive = true,
                TenantId = 1,
                Name = null
            };
            _moduleManipulation.AddModule(module);
        }

        [TestMethod, TestCategory("Modules - Add Module")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Module name can only have 100 characters maximum.", SeverityEnum.Warning)]
        public void AddModule_Fail_NameLengthExceeded()
        {
            ModuleDomain module = new ModuleDomain()
            {
                Code = "TestModule",
                Id = 1,
                IsActive = true,
                TenantId = 1,
                Name = "TestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModuleTestModule"
            };
            _moduleManipulation.AddModule(module);
        }
        #endregion

        #region Update Module
        [TestMethod, TestCategory("Modules - Update Module")]
        public void UpdateModule_Success()
        {
            var module = _moduleManipulation.GetModuleById(1);
            module.Name = "Updated name";
            _moduleManipulation.UpdateModule(module);
            Assert.AreEqual("Updated name", _moduleManipulation.GetModuleById(1).Name);
        }

        [TestMethod, TestCategory("Modules - Update Module")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Module details are not provided.", SeverityEnum.Warning)]
        public void UpdateModule_Fail_NullValue()
        {
            _moduleManipulation.UpdateModule(null);
        }
        
        #endregion

        #region Valid Domain Models
        private ModuleDomain GetValidModuleDomain()
        {
            return new ModuleDomain()
            {
                Code = "TestModule2",
                Id = 1,
                IsActive = true,
                TenantId = 1,
                Name = "Test Module"
            };
        }
        #endregion
    }
}
