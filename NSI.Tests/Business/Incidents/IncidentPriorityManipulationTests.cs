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
    public class IncidentPriorityManipulationTests
    {
        private Mock<IIncidentPriorityRepository> _incidentPriorityRepositoryMock;
        private IncidentPriorityManipulation _incidentPriorityManipulation;

        [TestInitialize]
        public void Initialize()
        {

            _incidentPriorityRepositoryMock = IncidentPriorityRepositoryMock.GetIncidentPriorityRepositoryMock();
            _incidentPriorityManipulation = new IncidentPriorityManipulation(
                _incidentPriorityRepositoryMock.Object
                );
        }

        [TestMethod, TestCategory("IncidentPriority - GetIncidentPriorityById")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetIncidentPriorityById_Fail_IdLessThanZero()
        {
            _incidentPriorityManipulation.GetIncidentPriorityById(-1);
        }

        [TestMethod, TestCategory("IncidentPriority - GetIncidentPriorityById")]
        public void GetIncidentPriorityById_Success_ValidId()
        {
            Domain.IncidentManagement.IncidentPriorityDomain result = _incidentPriorityManipulation.GetIncidentPriorityById(1);
            Assert.AreEqual("In progress", result.Name);
        }

        [TestMethod, TestCategory("IncidentPriority - DeleteIncidentPriority")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteIncidentPriority_Fail_IdLessThanZero()
        {
            _incidentPriorityManipulation.DeleteIncidentPriority(-1);
        }

        [TestMethod, TestCategory("IncidentPriority - AddIncidentPriority")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void AddIncidentPriority_Fail_NullValue()
        {
            _incidentPriorityManipulation.AddIncidentPriority(null);
        }

        [TestMethod, TestCategory("IncidentPriority - UpdateIncidentPriority")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateIncidentPriority_Fail_NullValue()
        {
            _incidentPriorityManipulation.UpdateIncidentPriority(null);
        }

        [TestMethod, TestCategory("IncidentPriority - AddIncidentPriority")]
        public void AddIncidentPriority_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentPriorityDomain request = new NSI.Domain.IncidentManagement.IncidentPriorityDomain
            {
                PriorityId = 1,
                Name = "In progress",
                Code = "1",
                IsActive = true,
                ColorCode = "1",
                IconPath = "1"
    };

            int id = _incidentPriorityManipulation.AddIncidentPriority(request);
            Assert.AreEqual(1, id);
        }

        [TestMethod, TestCategory("IncidentPriority - UpdateIncidentPriority")]
        public void UpdateIncidentPriority_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentPriorityDomain request = new NSI.Domain.IncidentManagement.IncidentPriorityDomain
            {
                PriorityId = 1,
                Name = "In progress",
                Code = "1",
                IsActive = true,
                ColorCode = "1",
                IconPath = "1"
            };

            _incidentPriorityManipulation.UpdateIncidentPriority(request);

        }
    }
}
