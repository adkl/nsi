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
    public class IncidentWorkOrderManipulationTests
    {
        private Mock<IIncidentWorkOrderRepository> _incidentWorkOrderRepositoryMock;
        private IncidentWorkOrderManipulation _incidentWorkOrderManipulation;

        [TestInitialize]
        public void Initialize()
        {

            _incidentWorkOrderRepositoryMock = IncidentWorkOrderRepositoryMock.GetIncidentWorkOrderRepositoryMock();
            _incidentWorkOrderManipulation = new IncidentWorkOrderManipulation(
                _incidentWorkOrderRepositoryMock.Object
                );
        }

        [TestMethod, TestCategory("IncidentWorkOrder - GetIncidentWorkOrderById")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetIncidentWorkOrderById_Fail_IdLessThanZero()
        {
            _incidentWorkOrderManipulation.GetIncidentWorkOrderById(-1);
        }

        [TestMethod, TestCategory("IncidentWorkOrder - GetIncidentWorkOrderById")]
        public void GetIncidentWorkOrderById_Success_ValidId()
        {
            Domain.IncidentManagement.IncidentWorkOrderDomain result = _incidentWorkOrderManipulation.GetIncidentWorkOrderById(1);
            Assert.AreEqual(1, result.TenantId);
        }

        [TestMethod, TestCategory("IncidentWorkOrder - DeleteIncidentWorkOrder")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteIncidentWorkOrder_Fail_IdLessThanZero()
        {
            _incidentWorkOrderManipulation.DeleteIncidentWorkOrder(-1);
        }

        [TestMethod, TestCategory("IncidentWorkOrder - AddIncidentWorkOrder")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void AddIncidentWorkOrder_Fail_NullValue()
        {
            _incidentWorkOrderManipulation.AddIncidentWorkOrder(null);
        }

        [TestMethod, TestCategory("IncidentWorkOrder - UpdateIncidentWorkOrder")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateIncidentWorkOrder_Fail_NullValue()
        {
            _incidentWorkOrderManipulation.UpdateIncidentWorkOrder(null);
        }

        [TestMethod, TestCategory("IncidentWorkOrder - AddIncidentWorkOrder")]
        public void AddIncidentWorkOrder_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentWorkOrderDomain request = new NSI.Domain.IncidentManagement.IncidentWorkOrderDomain
            {
                WorkOrderId = 1,
                DateCreated = DateTime.MaxValue,
                CreatedBy = 1,
                ModifiedBy = 1,
                DateModified = DateTime.MaxValue,
                TenantId = 1,
                IncidentId = 1,
                IncidentSettlementId = 1
            };

            int id = _incidentWorkOrderManipulation.AddIncidentWorkOrder(request);
            Assert.AreEqual(1, id);
        }

        [TestMethod, TestCategory("IncidentWorkOrder - UpdateIncidentWorkOrder")]
        public void UpdateIncidentWorkOrder_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentWorkOrderDomain request = new NSI.Domain.IncidentManagement.IncidentWorkOrderDomain
            {
                WorkOrderId = 1,
                DateCreated = DateTime.MaxValue,
                CreatedBy = 1,
                ModifiedBy = 1,
                DateModified = DateTime.MaxValue,
                TenantId = 1,
                IncidentId = 1,
                IncidentSettlementId = 1
            };

            _incidentWorkOrderManipulation.UpdateIncidentWorkOrder(request);

        }
    }
}
