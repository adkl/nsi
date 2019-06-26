using Moq;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class ModuleRepositoryMock
    {
        public static Mock<IModuleRepository> GetModuleRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var moduleRepository = new Mock<IModuleRepository> { CallBase = false };

            #region Get Module By ID
            moduleRepository.Setup(x => x.GetById(1)).Returns(
                new NSI.Domain.Membership.ModuleDomain()
                {
                    Code = "TestModule",
                    Id = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Module"
                });
            #endregion

            #region Get All Modules
            moduleRepository.Setup(x => x.GetAll()).Returns(new List<ModuleDomain>()
            {
                new ModuleDomain()
                {
                    Code = "TestModule",
                    Id = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Module"
                },
                new ModuleDomain()
                {
                    Code = "TestModule2",
                    Id = 2,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Module 2"
                }
            });
            #endregion

            # region Get Module By Code
            moduleRepository.Setup(x => x.GetByCode("TestModule")).Returns(
                new ModuleDomain()
                {
                    Code = "TestModule",
                    Id = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Module"
                });
            #endregion

            #region Add Module
            moduleRepository.Setup(x => x.Add(It.IsAny<ModuleDomain>())).Returns(1);
            #endregion

            #region Update Module
            moduleRepository.Setup(x => x.Update(It.IsAny<ModuleDomain>()));
            #endregion

            #region Get Modules By Tenant ID
            moduleRepository.Setup(x => x.GetTenantModules(1)).Returns(new List<ModuleDomain>()
            {
                new ModuleDomain()
                {
                    Code = "TestModule",
                    Id = 1,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Module"
                },
                new ModuleDomain()
                {
                    Code = "TestModule2",
                    Id = 2,
                    IsActive = true,
                    TenantId = 1,
                    Name = "Test Module 2"
                }
            });
            #endregion

            return moduleRepository;

        }
    }
}
