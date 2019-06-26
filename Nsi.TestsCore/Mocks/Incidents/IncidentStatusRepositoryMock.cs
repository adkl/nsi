using Moq;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class IncidentStatusRepositoryMock
    {
        public static Mock<IIncidentStatusRepository> GetIncidentStatusRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var incidentStatusRepository = new Mock<IIncidentStatusRepository> { CallBase = false };

            incidentStatusRepository.Setup(x => x.GetIncidentStatusById(1)).Returns(
               new NSI.Domain.IncidentManagement.IncidentStatusDomain
               {
                   IncidentStatusId = 1,
                   Name = "In progress",
                   Code = "1",
                   IsActive = true
               });

            incidentStatusRepository.Setup(x => x.AddIncidentStatus(It.IsAny<NSI.Domain.IncidentManagement.IncidentStatusDomain>())).Returns(1);

            incidentStatusRepository.Setup(x => x.UpdateIncidentStatus(It.IsAny<NSI.Domain.IncidentManagement.IncidentStatusDomain>()));

            return incidentStatusRepository;
        }
    }
}
