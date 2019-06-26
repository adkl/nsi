using Moq;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using NSI.Repository.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class PermissionRepositoryMock
    {
        public static Mock<IPermissionRepository> GetPermissionRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var permissionRepository = new Mock<IPermissionRepository> { CallBase = false };

            //Returns 1 for any PermissionDomain forwarded, extended if special cases are needed
            permissionRepository.Setup(x => x.Add(It.IsAny<PermissionDomain>()))
                .Returns(1);

            // returns null for TESTDOESNOTEXIST code
            permissionRepository.Setup(x => x.GetByCode("TESTDOESNOTEXIST")).Returns((PermissionDomain)null);

            // returns domain model for TESTCODEEXISTS code
            permissionRepository.Setup(x => x.GetByCode("TESTCODEEXISTS")).Returns((
                new PermissionDomain()
                {
                    Code = "TESTCODEEXISTS",
                    Id = 1,
                    IsActive = true,
                    ModuleId = 1,
                    TenantId = 1
                }
                ));

            permissionRepository.Setup(x => x.GetById(1)).Returns(new PermissionDomain()
            {
                Code = "TESTCODEEXISTS",
                Id = 1,
                IsActive = true,
                ModuleId = 1,
                TenantId = 1
            });

            permissionRepository.Setup(x => x.GetAll(null)).Returns(new List<PermissionDomain>()
            {
                new PermissionDomain()
                {
                    Code = "TESTCODEEXISTS",
                    Id = 1,
                    IsActive = true,
                    ModuleId = 1,
                    TenantId = 1
                },
                new PermissionDomain()
                {
                    Code = "TESTCODEEXISTS2",
                    Id = 2,
                    IsActive = true,
                    ModuleId = 1,
                    TenantId = 1
                }
            });

            permissionRepository.Setup(x => x.Update(It.IsAny<PermissionDomain>()));

            return permissionRepository;
        }
    }
}
