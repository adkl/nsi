using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSI.Common.Exceptions;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NSI.Repository.Interfaces.Membership;
using Nsi.TestsCore.Extensions;
using NSI.Common.Enumerations;
using NSI.Resources.Membership;
using NSI.BusinessLogic.Membership;
using Nsi.TestsCore.Mocks;
using NSI.Repository.Interfaces.IncidentManagement;
using NSI.BusinessLogic.IncidentManagement;
using NSI.Repository.Interfaces.Notifications;

namespace NSI.Tests.Business
{
    [TestClass]
    public class IncidentManipulationTests
    {
        private Mock<IIncidentRepository> _IncidentRepositoryMock;
        private Mock<IIncidentSettlementRepository> _IncidentSettlementRepositoryMock;
        private Mock<IIncidentWorkOrderRepository> _IncidentWorkOrderRepositoryMock;
        private Mock<INotificationRepository> _NotificationRepositoryMock;
        private Mock<ITenantRepository> _TenantRepositoryMock;
        private IncidentManipulation _IncidentManipulation;

        [TestInitialize]
        public void Initialize()
        {

            _IncidentRepositoryMock = IncidentRepositoryMock.GetIncidentRepositoryMock();
            _IncidentSettlementRepositoryMock = IncidentSettlementRepositoryMock.GetIncidentSettlementRepositoryMock();
            _IncidentWorkOrderRepositoryMock = IncidentWorkOrderRepositoryMock.GetIncidentWorkOrderRepositoryMock();
            _NotificationRepositoryMock = NotificationRepositoryMock.GetNotificationRepositoryMock();
            _TenantRepositoryMock = TenantRepositoryMock.GetTenantRepositoryMock();
            _IncidentManipulation = new IncidentManipulation(
                _IncidentRepositoryMock.Object,
                _IncidentSettlementRepositoryMock.Object,
                _IncidentWorkOrderRepositoryMock.Object,
                _NotificationRepositoryMock.Object,
                _TenantRepositoryMock.Object
        );
        }

        [TestMethod, TestCategory("Incident - GetIncidentById")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetIncidentById_Fail_IdLessThanZero()
        {
            _IncidentManipulation.GetIncidentById(-1, 1);
        }

        [TestMethod, TestCategory("Incident - GetIncidentById")]
        public void GetIncidentById_Success_ValidId()
        {
            Domain.IncidentManagement.IncidentDomain result = _IncidentManipulation.GetIncidentById(1, 1);
        }

        [TestMethod, TestCategory("Incident - GetAllIncidents")]
        public void GetAllIncidents_Success_ValidId()
        {
            ICollection<Domain.IncidentManagement.IncidentDomain> result = _IncidentManipulation.GetAllIncidents(1);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod, TestCategory("Incident - GetAllIncidents")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Tenant with provided id does not exist.", SeverityEnum.Error)]
        public void GetAllIncidents_Fail_InvalidTenantId()
        {
            _IncidentManipulation.GetAllIncidents(-1);
        }

        [TestMethod, TestCategory("Incident - DeleteIncident")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteIncident_Fail_IdLessThanZero()
        {
            _IncidentManipulation.DeleteIncident(-1, 1);
        }

        [TestMethod, TestCategory("Incident - AddIncident")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void AddIncident_Fail_NullValue()
        {
            _IncidentManipulation.AddIncident(null, 1);
        }

        [TestMethod, TestCategory("Incident - UpdateIncident")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateIncident_Fail_NullValue()
        {
            _IncidentManipulation.UpdateIncident(null, 1);
        }

        [TestMethod, TestCategory("Incident - AddIncident")]
        public void AddIncident_Success_ValidRequestWithAutomatization()
        {
            Domain.IncidentManagement.POSTIncidentDomain request = new NSI.Domain.IncidentManagement.POSTIncidentDomain
            {
                IncidentId = 1,
                DateCreated = DateTime.MaxValue,
                CreatedBy = 1,
                ModifiedBy = 1,
                DateModified = DateTime.MaxValue,
                TenantId = 1,
                IncidentStatus = 1,
                DeviceId = 1,
                Priority = 1,
                IncidentType = 1,
                ReporterId = 1
             };

            int id = _IncidentManipulation.AddIncident(request, 1);
            Assert.AreEqual(1, id);
        }
        [TestMethod, TestCategory("Incident - AddIncident")]
        public void AddIncident_Success_ValidRequestWithoutAutomatization()
        {
            Domain.IncidentManagement.POSTIncidentDomain request = new NSI.Domain.IncidentManagement.POSTIncidentDomain
            {
                IncidentId = 1,
                DateCreated = DateTime.MaxValue,
                CreatedBy = 1,
                ModifiedBy = 1,
                DateModified = DateTime.MaxValue,
                TenantId = 1,
                IncidentStatus = 1,
                DeviceId = 1,
                Priority = 1,
                IncidentType = 2,
                ReporterId = 1
            };

            int id = _IncidentManipulation.AddIncident(request, 1);
            Assert.AreEqual(1, id);
        }
        [TestMethod, TestCategory("Incident - UpdateIncident")]
        public void UpdateIncident_Success_ValidRequest()
        {
            Domain.IncidentManagement.POSTIncidentDomain request = new NSI.Domain.IncidentManagement.POSTIncidentDomain
            {
                IncidentId = 1,
                DateCreated = DateTime.MaxValue,
                CreatedBy = 1,
                ModifiedBy = 1,
                DateModified = DateTime.MaxValue,
                TenantId = 1,
                IncidentStatus = 1,
                DeviceId = 1,
                Priority = 1,
                IncidentType = 1,
                ReporterId = 1
            };

            _IncidentManipulation.UpdateIncident(request, 1);

        }
    }
}
