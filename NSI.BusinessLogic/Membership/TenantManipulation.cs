using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.Common.Models;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using NSI.Resources.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Membership
{
    public class TenantManipulation : ITenantManipulation
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ILanguageRepository _languageRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tenantRepository, languageRepository"></param>
        public TenantManipulation(
            ITenantRepository tenantRepository,
            ILanguageRepository languageRepository)
        {
            _tenantRepository = tenantRepository;
            _languageRepository = languageRepository;
        }

        /// <summary>
        /// Adds new tenant to the database
        /// </summary>
        /// <param name="tenant">Tenant information to be added. Instance of <see cref="TenantDomain"/></param>
        /// <returns>TenantId of the newly created tenant</returns>
        public int AddTenant(TenantDomain tenant)
        {
            ValidateTenantModel(tenant);
            tenant.Identifier = Guid.NewGuid();
            // Check if identifier exists
            var tenantWithProvidedIdentifier = _tenantRepository.GetByIdentifier(tenant.Identifier);
            if (tenantWithProvidedIdentifier != null)
            {
                throw new NsiArgumentException(MembershipMessages.TenantIdentifierAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            return _tenantRepository.Add(tenant);
        }

        /// <summary>
        /// Retrieves all tenants from the database
        /// </summary>
        /// <returns><see cref="ICollection{TenantDomain}"/></returns>
        public ICollection<TenantDomain> GetAllTenants()
        {
            return _tenantRepository.GetAll();
        }

        /// <summary>
        /// Retrieves tenant with provided ID
        /// </summary>
        /// <param name="tenantId">Tenant ID</param>
        /// <returns>Tenant if it exists, instance of <see cref="TenantDomain"/>. Else null.</returns>
        public TenantDomain GetTenantById(int tenantId)
        {
            ValidationHelper.GreaterThanZero(tenantId, MembershipMessages.TenantIdInvalid);
            return _tenantRepository.GetById(tenantId);
        }

        /// <summary>
        /// Retrieves tenant with provided indetifier
        /// </summary>
        /// <param name="identifier">Tenant identifier</param>
        /// <returns>Tenant if it exists, instance of <see cref="TenantDomain"/>. Else null.</returns>
        public TenantDomain GetTenantByIdentifier(Guid identifier)
        {
            ValidationHelper.NotNull(identifier, MembershipMessages.TenantIdentifierNotProvided);
            return _tenantRepository.GetByIdentifier(identifier);
        }

        /// <summary>
        /// Search Tenants
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{TenantDomain}"/></returns>
        public ICollection<TenantDomain> SearchTenants(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _tenantRepository.SearchTenants(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Update Tenant
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public void UpdateTenant(TenantDomain tenant)
        {
            ValidateTenantModel(tenant);
            ValidationHelper.GreaterThanZero(tenant.Id, MembershipMessages.TenantIdInvalid);
            ValidationHelper.NotNull(_tenantRepository.GetById(tenant.Id), MembershipMessages.TenantWithIdDoesNotExist);


            _tenantRepository.Update(tenant);
        }

        private void ValidateTenantModel(TenantDomain tenant)
        {
            ValidationHelper.NotNull(tenant, MembershipMessages.TenantNotProvided);
            ValidationHelper.GreaterThanZero(tenant.DefaultLanguageId, MembershipMessages.LanguageIdInvalid);
            ValidationHelper.NotNull(_languageRepository.GetById(tenant.DefaultLanguageId), MembershipMessages.LanguageWithIdDoesNotExist);
            ValidationHelper.MaxLength(tenant.Name, 100, MembershipMessages.TenantNameLengthExceeded);
            ValidationHelper.NotNullOrWhitespace(tenant.Name, MembershipMessages.TenantNameInvalid);
        }
    }
}
