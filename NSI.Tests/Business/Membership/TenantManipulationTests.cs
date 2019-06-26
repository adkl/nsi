using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using NSI.BusinessLogic.Membership;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Membership
{
    [TestClass]
    public class TenantManipulationTests
    {
        private Mock<ITenantRepository> _tenantRepositoryMock;
        private Mock<ILanguageRepository> _languageRepositoryMock;
        private TenantManipulation _tenantManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _tenantRepositoryMock = TenantRepositoryMock.GetTenantRepositoryMock();
            _languageRepositoryMock = LanguageRepositoryMock.GetLanguageRepositoryMock();
            _tenantManipulation = new TenantManipulation(_tenantRepositoryMock.Object, _languageRepositoryMock.Object);
        }

        [TestMethod, TestCategory("Tenant - Get Tenant By Id")]
        public void GetTenantById_Success()
        {
            TenantDomain tenant = _tenantManipulation.GetTenantById(1);
            Assert.AreEqual(1, tenant.Id);
        }

        [TestMethod, TestCategory("Tenant - Get Tenant By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided tenant ID is not valid.", SeverityEnum.Warning)]
        public void GetTenantById_Fail_InvalidLanguageId()
        {
            _tenantManipulation.GetTenantById(-1);
        }

        [TestMethod, TestCategory("Tenant - Get All Tenants")]
        public void GetAllTenants_Success()
        {
            _tenantManipulation.GetAllTenants();
        }

        [TestMethod, TestCategory("Tenant - Get Tenant By Identifier")]
        public void GetModuleByIdentifier_Success()
        {
            TenantDomain tenant = _tenantManipulation.GetTenantByIdentifier(Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f"));
            Assert.AreEqual(Guid.Parse("81a130d2-502f-4cf1-a376-63edeb000e9f"), tenant.Identifier);
        }

        [TestMethod, TestCategory("Tenant - Add Tenant")]
        public void AddTenant_Success()
        {
            int tenantId = _tenantManipulation.AddTenant(GetValidTenantDomain());
            Assert.AreEqual(1, tenantId);
        }

        [TestMethod, TestCategory("Tenant - Add Tenant")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Tenant details are not provided.", SeverityEnum.Warning)]
        public void AddTenant_Fail_InvalidTenant()
        {
            _tenantManipulation.AddTenant(null);
        }

        [TestMethod, TestCategory("Tenant - Add Tenant")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Tenant name can only have 100 characters maximum.", SeverityEnum.Warning)]
        public void AddTenant_Fail_NameLengthExceeded()
        {
            TenantDomain tenant = new TenantDomain()
            {
                Name = "LejlaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaLejlaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                DefaultLanguageId = 1,
                IsActive = true,
                Id = 1
            };
            _tenantManipulation.AddTenant(tenant);
        }

        [TestMethod, TestCategory("Tenant - Add Tenant")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Provided tenant name is not valid.", SeverityEnum.Warning)]
        public void AddTenant_Fail_NameInvalid()
        {
            TenantDomain tenant = new TenantDomain()
            {
                Name = "",
                DefaultLanguageId = 1,
                IsActive = true,
                Id = 1
            };
            _tenantManipulation.AddTenant(tenant);
        }

        [TestMethod, TestCategory("Tenant - Add Tenant")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided language ID is not valid.", SeverityEnum.Warning)]
        public void AddTenant_Fail_LanguageIdInvalid()
        {
            TenantDomain tenant = new TenantDomain()
            {
                Name = "Lejla",
                DefaultLanguageId = -1,
                IsActive = true,
                Id = 1
            };
            _tenantManipulation.AddTenant(tenant);
        }

        [TestMethod, TestCategory("Tenant - Update Tenant")]
        public void UpdateTenant_Success()
        {
            var tenant = _tenantManipulation.GetTenantById(1);
            tenant.Name = "Updated name";
            _tenantManipulation.UpdateTenant(tenant);
            Assert.AreEqual("Updated name", _tenantManipulation.GetTenantById(1).Name);
        }

        [TestMethod, TestCategory("Tenant - Update Tenant")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Tenant details are not provided.", SeverityEnum.Warning)]
        public void UpdateTenant_Fail_NullValue()
        {
            _tenantManipulation.UpdateTenant(null);
        }

        private TenantDomain GetValidTenantDomain()
        {
            return new TenantDomain()
            {
                Name = "Lejla",
                DefaultLanguageId = 1,
                IsActive = true,
                Id = 1
            };
        }
    }
}
