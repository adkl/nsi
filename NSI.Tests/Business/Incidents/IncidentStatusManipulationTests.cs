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
    public class IncidentStatusManipulationTests
    {
        private Mock<IIncidentStatusRepository> _incidentStatusRepositoryMock;
        private IncidentStatusManipulation _incidentStatusManipulation;

        [TestInitialize]
        public void Initialize()
        {
            
            _incidentStatusRepositoryMock = IncidentStatusRepositoryMock.GetIncidentStatusRepositoryMock();
            _incidentStatusManipulation = new IncidentStatusManipulation(
                _incidentStatusRepositoryMock.Object
                );
        }
        
        [TestMethod, TestCategory("IncidentStatus - GetIncidentStatusById")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetIncidentStatusById_Fail_IdLessThanZero()
        {
            _incidentStatusManipulation.GetIncidentStatusById(-1);
        }

        [TestMethod, TestCategory("IncidentStatus - GetIncidentStatusById")]
        public void GetIncidentStatusById_Success_ValidId()
        {
            Domain.IncidentManagement.IncidentStatusDomain result = _incidentStatusManipulation.GetIncidentStatusById(1);
            Assert.AreEqual("In progress", result.Name);
        }

        [TestMethod, TestCategory("IncidentStatus - DeleteIncidentStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteIncidentStatus_Fail_IdLessThanZero()
        {
            _incidentStatusManipulation.DeleteIncidentStatus(-1);
        }

        [TestMethod, TestCategory("IncidentStatus - AddIncidentStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void AddIncidentStatus_Fail_NullValue()
        {
            _incidentStatusManipulation.AddIncidentStatus(null);
        }

        [TestMethod, TestCategory("IncidentStatus - UpdateIncidentStatus")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateIncidentStatus_Fail_NullValue()
        {
            _incidentStatusManipulation.UpdateIncidentStatus(null);
        }

        [TestMethod, TestCategory("IncidentStatus - AddIncidentStatus")]
        public void AddIncidentStatus_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentStatusDomain request = new NSI.Domain.IncidentManagement.IncidentStatusDomain
            {
                IncidentStatusId = 1,
                Name = "In progress",
                Code = "1",
                IsActive = true
            };

            int id = _incidentStatusManipulation.AddIncidentStatus(request);
            Assert.AreEqual(1, id);
        }

        [TestMethod, TestCategory("IncidentStatus - UpdateIncidentStatus")]
        public void UpdateIncidentStatus_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentStatusDomain request = new NSI.Domain.IncidentManagement.IncidentStatusDomain
            {
                IncidentStatusId = 1,
                Name = "In progress",
                Code = "1",
                IsActive = true
            };

            _incidentStatusManipulation.UpdateIncidentStatus(request);

        }
    }
}
