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

namespace NSI.Tests.Business
{
    [TestClass]
    public class IncidentSettlementManipulationTests
    {
        private Mock<IIncidentSettlementRepository> _incidentSettlementRepositoryMock;
        private IncidentSettlementManipulation _incidentSettlementManipulation;

        [TestInitialize]
        public void Initialize()
        {

            _incidentSettlementRepositoryMock = IncidentSettlementRepositoryMock.GetIncidentSettlementRepositoryMock();
            _incidentSettlementManipulation = new IncidentSettlementManipulation(
                _incidentSettlementRepositoryMock.Object
                );
        }

        [TestMethod, TestCategory("IncidentSettlement - GetIncidentSettlementById")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetIncidentSettlementById_Fail_IdLessThanZero()
        {
            _incidentSettlementManipulation.GetIncidentSettlementById(-1);
        }

        [TestMethod, TestCategory("IncidentSettlement - GetIncidentSettlementById")]
        public void GetIncidentSettlementById_Success_ValidId()
        {
            Domain.IncidentManagement.IncidentSettlementDomain result = _incidentSettlementManipulation.GetIncidentSettlementById(1);
            Assert.AreEqual("Solved", result.Description);
        }

        [TestMethod, TestCategory("IncidentSettlement - DeleteIncidentSettlement")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteIncidentSettlement_Fail_IdLessThanZero()
        {
            _incidentSettlementManipulation.DeleteIncidentSettlement(-1);
        }

        [TestMethod, TestCategory("IncidentSettlement - AddIncidentSettlement")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void AddIncidentSettlement_Fail_NullValue()
        {
            _incidentSettlementManipulation.AddIncidentSettlement(null);
        }

        [TestMethod, TestCategory("IncidentSettlement - UpdateIncidentSettlement")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateIncidentSettlement_Fail_NullValue()
        {
            _incidentSettlementManipulation.UpdateIncidentSettlement(null);
        }

        [TestMethod, TestCategory("IncidentSettlement - AddIncidentSettlement")]
        public void AddIncidentSettlement_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentSettlementDomain request = new NSI.Domain.IncidentManagement.IncidentSettlementDomain
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
            };

            int id = _incidentSettlementManipulation.AddIncidentSettlement(request);
            Assert.AreEqual(1, id);
        }

        [TestMethod, TestCategory("IncidentSettlement - UpdateIncidentSettlement")]
        public void UpdateIncidentSettlement_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentSettlementDomain request = new NSI.Domain.IncidentManagement.IncidentSettlementDomain
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
            };

            _incidentSettlementManipulation.UpdateIncidentSettlement(request);

        }
    }
}
