using Moq;
using NSI.Domain.IncidentManagement;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class IncidentRepositoryMock
    {
        public static Mock<IIncidentRepository> GetIncidentRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var incidentRepository = new Mock<IIncidentRepository> { CallBase = false };

            incidentRepository.Setup(x => x.GetAllIncidents(1)).Returns(
                new List<IncidentDomain> {
                    new IncidentDomain(), new IncidentDomain()
                 }
                );

            incidentRepository.Setup(x => x.GetIncidentById(1,1)).Returns(
               new NSI.Domain.IncidentManagement.IncidentDomain
               {
                   IncidentId = 1,
                   DateCreated = DateTime.MaxValue,
                   CreatedBy = 1,
                   ModifiedBy = 1,
                   TenantId = 1,
                   DateModified = DateTime.MaxValue,
                   Tenant = new NSI.Domain.Membership.TenantDomain
                   {
                       Name = "Milan",
                       DefaultLanguageId = 1,
                       IsActive = true
                   },
                   IncidentStatus = new NSI.Domain.IncidentManagement.IncidentStatusDomain
                   {
                       IncidentStatusId = 1,
                       Name = "Active",
                       Code = "501",
                       IsActive = true
                   },
                   Device = new NSI.Domain.DeviceManagement.DeviceDomain
                   {
                       DeviceId = 1,
                       Description = "Samsung TV",
                       Name = "TV",
                       IsActive = true,
                       IsDeleted = false,
                       DeviceTypeId = 1,
                       DeviceStatusId = 1
                   },
                   Priority = new NSI.Domain.IncidentManagement.IncidentPriorityDomain
                   {
                       PriorityId = 1,
                       Name = "High",
                       Code = "1",
                       ColorCode = "2",
                       IsActive = true,
                       IconPath = "somePath"
                   },
                   IncidentType = new NSI.Domain.IncidentManagement.IncidentTypeDomain
                   {
                       IncidentTypeId = 1,
                       Name = "TV not working",
                       Code = "2",
                       IsActive = true
                   },
                  Reporter = new NSI.Domain.Membership.UserDomain
                  {
                      FirstName = "Milan",
                      LastName = "Zuza"
                  },
                  Assignee = new NSI.Domain.Membership.UserDomain
                  {

                  }
    });

            incidentRepository.Setup(x => x.AddIncident(It.IsAny<NSI.Domain.IncidentManagement.POSTIncidentDomain>(),1)).Returns(1);

            incidentRepository.Setup(x => x.UpdateIncident(It.IsAny<NSI.Domain.IncidentManagement.POSTIncidentDomain>(),1));

            return incidentRepository;
        }
    }
}
