using Moq;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class IncidentSettlementRepositoryMock
    {
        public static Mock<IIncidentSettlementRepository> GetIncidentSettlementRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var incidentSettlementRepository = new Mock<IIncidentSettlementRepository> { CallBase = false };

            incidentSettlementRepository.Setup(x => x.GetIncidentSettlementById(1)).Returns(
               new NSI.Domain.IncidentManagement.IncidentSettlementDomain
               {
                   IncidentSettlementId = 1,
                   Description = "Solved",
                   FullText = "Incident successfully solved",
                   DateSettled = DateTime.MaxValue,
                   DateCreated = DateTime.MaxValue,
                   ModifiedBy = 1,
                   DateModified = DateTime.MaxValue,
                   TenantId = 1,
                   IncidentStatusId =1
    });

            incidentSettlementRepository.Setup(x => x.AddIncidentSettlement(It.IsAny<NSI.Domain.IncidentManagement.IncidentSettlementDomain>())).Returns(1);

            incidentSettlementRepository.Setup(x => x.UpdateIncidentSettlement(It.IsAny<NSI.Domain.IncidentManagement.IncidentSettlementDomain>()));

            return incidentSettlementRepository;
        }
    }
}
