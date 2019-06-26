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
    public static class RoleRepositoryMock
    {
        
        public static Mock<IRoleRepository> GetRoleRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var roleRepository = new Mock<IRoleRepository> { CallBase = false };

            roleRepository.Setup(x => x.GetById(1)).Returns(
                new RoleDomain()
                {

                    Name = "Rola 1",
                    IsActive = false,
                    ManipulationLogId = 0,
                    TenantId = 1,
                    Id = 1
                    
                    /*Name = "Engleski jezik",
                    IsoCode = "en",
                    IsActive = true,
                    IsDefault = true,
                    Id = 1*/
                });

            roleRepository.Setup(x => x.GetAll(1, null)).Returns(new List<RoleDomain>()
            {
                new RoleDomain()
                {
                    Name = "Rola 1",
                    IsActive = false,
                    ManipulationLogId = 0,
                    TenantId = 1,
                    Id = 1
                },
                new RoleDomain()
                {
                    Name = "Rola 2",
                    IsActive = false,
                    ManipulationLogId = 0,
                    TenantId = 1,
                    Id = 2
                }
            });

            roleRepository.Setup(x => x.GetByName("Rola 1")).Returns(
                new RoleDomain()
                {
                    Name = "Rola 1",
                    IsActive = false,
                    ManipulationLogId = 0,
                    TenantId = 1,
                    Id = 1
                });

            roleRepository.Setup(x => x.Add(It.IsAny<RoleDomain>())).Returns(1);

            roleRepository.Setup(x => x.Update(It.IsAny<RoleDomain>()));

            return roleRepository;
        }
        
    }
}
