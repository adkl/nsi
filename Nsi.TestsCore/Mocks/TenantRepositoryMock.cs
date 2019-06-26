using Moq;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.IncidentManagement;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class TenantRepositoryMock
    {
        public static Mock<ITenantRepository> GetTenantRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var TenantRepository = new Mock<ITenantRepository> { CallBase = false };

            TenantRepository.Setup(x => x.GetById(1)).Returns(
               new NSI.Domain.Membership.TenantDomain
               {
                   Name = "Milan",
                   DefaultLanguageId = 1,
                   IsActive = true,
                   Id = 1
               });

            TenantRepository.Setup(x => x.GetAll()).Returns(new List<TenantDomain>()
            {
                new TenantDomain()
                {
                    Name = "Milan",
                    DefaultLanguageId = 1,
                    IsActive = true,
                    Id = 1
                },
                new TenantDomain()
                {
                    Name = "Milan2",
                    DefaultLanguageId = 1,
                    IsActive = true,
                    Id = 2
                }
            });


            TenantRepository.Setup(x => x.GetByIdentifier(Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f"))).Returns(
                new TenantDomain()
                {
                    Name = "Milan",
                    DefaultLanguageId = 1,
                    IsActive = true,
                    Id = 1,
                    Identifier = Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f")
                });

            TenantRepository.Setup(x => x.Add(It.IsAny<TenantDomain>())).Returns(1);

            TenantRepository.Setup(x => x.Update(It.IsAny<TenantDomain>()));

            return TenantRepository;
        }
    }
}
