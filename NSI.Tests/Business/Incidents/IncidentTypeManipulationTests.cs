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
    public class IncidentTypeManipulationTests
    {
        private Mock<IIncidentTypeRepository> _incidentTypeRepositoryMock;
        private IncidentTypeManipulation _incidentTypeManipulation;

        [TestInitialize]
        public void Initialize()
        {

            _incidentTypeRepositoryMock = IncidentTypeRepositoryMock.GetIncidentTypeRepositoryMock();
            _incidentTypeManipulation = new IncidentTypeManipulation(
                _incidentTypeRepositoryMock.Object
                );
        }

        [TestMethod, TestCategory("IncidentType - GetIncidentTypeById")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void GetIncidentTypeById_Fail_IdLessThanZero()
        {
            _incidentTypeManipulation.GetIncidentTypeById(-1);
        }

        [TestMethod, TestCategory("IncidentType - GetIncidentTypeById")]
        public void GetIncidentTypeById_Success_ValidId()
        {
            Domain.IncidentManagement.IncidentTypeDomain result = _incidentTypeManipulation.GetIncidentTypeById(1);
            Assert.AreEqual("In progress", result.Name);
        }

        [TestMethod, TestCategory("IncidentType - DeleteIncidentType")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void DeleteIncidentType_Fail_IdLessThanZero()
        {
            _incidentTypeManipulation.DeleteIncidentType(-1);
        }

        [TestMethod, TestCategory("IncidentType - AddIncidentType")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void AddIncidentType_Fail_NullValue()
        {
            _incidentTypeManipulation.AddIncidentType(null);
        }

        [TestMethod, TestCategory("IncidentType - UpdateIncidentType")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided argument is not valid.", SeverityEnum.Error)]
        public void UpdateIncidentType_Fail_NullValue()
        {
            _incidentTypeManipulation.UpdateIncidentType(null);
        }

        [TestMethod, TestCategory("IncidentType - AddIncidentType")]
        public void AddIncidentType_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentTypeDomain request = new NSI.Domain.IncidentManagement.IncidentTypeDomain
            {
                IncidentTypeId = 1,
                Name = "In progress",
                Code = "1",
                IsActive = true
            };

            int id = _incidentTypeManipulation.AddIncidentType(request);
            Assert.AreEqual(1, id);
        }

        [TestMethod, TestCategory("IncidentType - UpdateIncidentType")]
        public void UpdateIncidentType_Success_ValidRequest()
        {
            Domain.IncidentManagement.IncidentTypeDomain request = new NSI.Domain.IncidentManagement.IncidentTypeDomain
            {
                IncidentTypeId = 1,
                Name = "In progress",
                Code = "1",
                IsActive = true
            };

            _incidentTypeManipulation.UpdateIncidentType(request);

        }
    }
}
