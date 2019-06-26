using Moq;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class IncidentWorkOrderRepositoryMock
    {
        public static Mock<IIncidentWorkOrderRepository> GetIncidentWorkOrderRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var incidentWorkOrderRepository = new Mock<IIncidentWorkOrderRepository> { CallBase = false };

            incidentWorkOrderRepository.Setup(x => x.GetIncidentWorkOrderById(1)).Returns(
               new NSI.Domain.IncidentManagement.IncidentWorkOrderDomain
               {
                   WorkOrderId = 1,
                   DateCreated = DateTime.MaxValue,
                   CreatedBy = 1,
                   ModifiedBy = 1,
                   DateModified = DateTime.MaxValue,
                   TenantId = 1,
                   IncidentId = 1,
                   IncidentSettlementId = 1
    });

            incidentWorkOrderRepository.Setup(x => x.GetIncidentSettlementByTypeId(1)).Returns(
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
                   IncidentStatusId = 1
               });

            incidentWorkOrderRepository.Setup(x => x.AddIncidentWorkOrder(It.IsAny<NSI.Domain.IncidentManagement.IncidentWorkOrderDomain>())).Returns(1);

            incidentWorkOrderRepository.Setup(x => x.UpdateIncidentWorkOrder(It.IsAny<NSI.Domain.IncidentManagement.IncidentWorkOrderDomain>()));

            return incidentWorkOrderRepository;
        }
    }
}
