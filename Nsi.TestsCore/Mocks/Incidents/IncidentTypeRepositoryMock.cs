using Moq;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class IncidentTypeRepositoryMock
    {
        public static Mock<IIncidentTypeRepository> GetIncidentTypeRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var incidentTypeRepository = new Mock<IIncidentTypeRepository> { CallBase = false };

            incidentTypeRepository.Setup(x => x.GetIncidentTypeById(1)).Returns(
               new NSI.Domain.IncidentManagement.IncidentTypeDomain
               {
                   IncidentTypeId = 1,
                   Name = "In progress",
                   Code = "1",
                   IsActive = true
               });

            incidentTypeRepository.Setup(x => x.AddIncidentType(It.IsAny<NSI.Domain.IncidentManagement.IncidentTypeDomain>())).Returns(1);

            incidentTypeRepository.Setup(x => x.UpdateIncidentType(It.IsAny<NSI.Domain.IncidentManagement.IncidentTypeDomain>()));

            return incidentTypeRepository;
        }
    }
}
