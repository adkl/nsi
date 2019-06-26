using Moq;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class IncidentPriorityRepositoryMock
    {
        public static Mock<IIncidentPriorityRepository> GetIncidentPriorityRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var incidentPriorityRepository = new Mock<IIncidentPriorityRepository> { CallBase = false };

            incidentPriorityRepository.Setup(x => x.GetIncidentPriorityById(1)).Returns(
               new NSI.Domain.IncidentManagement.IncidentPriorityDomain
               {
                   PriorityId = 1,
                   Name = "In progress",
                   Code = "1",
                   IsActive = true,
                   ColorCode = "1",
                   IconPath = "1"
               });

            incidentPriorityRepository.Setup(x => x.AddIncidentPriority(It.IsAny<NSI.Domain.IncidentManagement.IncidentPriorityDomain>())).Returns(1);

            incidentPriorityRepository.Setup(x => x.UpdateIncidentPriority(It.IsAny<NSI.Domain.IncidentManagement.IncidentPriorityDomain>()));

            return incidentPriorityRepository;
        }
    }
}
