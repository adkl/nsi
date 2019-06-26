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
    public static class RolePermissionRepositoryMock
    {
        public static Mock<IRolePermissionRepository> GetRolePermissionRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var rolePermissionRepository = new Mock<IRolePermissionRepository> { CallBase = false };

            rolePermissionRepository.Setup(x => x.GetById(1)).Returns(
                new RolePermissionDomain()
                {
                    Id = 1,
                    TenantId = 1,
                    IsActive = true,
                    RoleId = 1,
                    PermissionId = 1
                });

            rolePermissionRepository.Setup(x => x.GetAll()).Returns(new List<RolePermissionDomain>()
            {
                new RolePermissionDomain()
                {
                    Id = 1,
                    TenantId = 1,
                    IsActive = true,
                    RoleId = 1,
                    PermissionId = 1
                },
                new RolePermissionDomain()
                {
                    Id = 2,
                    TenantId = 1,
                    IsActive = true,
                    RoleId = 2,
                    PermissionId = 2
                }
            });

            rolePermissionRepository.Setup(x => x.Add(It.IsAny<RolePermissionDomain>())).Returns(1);

            rolePermissionRepository.Setup(x => x.Update(It.IsAny<RolePermissionDomain>()));

            

            return rolePermissionRepository;
        }
    }
}
