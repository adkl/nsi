using NSI.Common.Extensions;
using NSI.Common.Models;
using NSI.Domain.Membership;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Membership
{
    public class TenantRepository : ITenantRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public TenantRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new tenant to the database
        /// </summary>
        /// <param name="tenant">Tenant information to be added. Instance of <see cref="TenantDomain"/></param>
        /// <returns>TenantId of the newly created tenant</returns>
        public int Add(TenantDomain tenant)
        {
            var tenantDb = new Tenant().FromDomainModel(tenant);
            _context.Tenant.Add(tenantDb);
            _context.SaveChanges();
            return tenantDb.TenantId;
        }

        /// <summary>
        /// Retrieves all tenants from the database
        /// </summary>
        /// <returns><see cref="ICollection{TenantDomain}"/></returns>
        public ICollection<TenantDomain> GetAll()
        {
            return _context.Tenant
                .Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves tenant with provided ID
        /// </summary>
        /// <param name="tenantId">Tenant ID</param>
        /// <returns>Tenant if it exists, instance of <see cref="TenantDomain"/>. Else null.</returns>
        public TenantDomain GetById(int tenantId)
        {
            return _context.Tenant.FirstOrDefault(x => x.TenantId == tenantId).ToDomainModel();
        }

        /// <summary>
        /// Retrieves tenant with provided indetifier
        /// </summary>
        /// <param name="identifier">Tenant identifier</param>
        /// <returns>Tenant if it exists, instance of <see cref="TenantDomain"/>. Else null.</returns>
        public TenantDomain GetByIdentifier(Guid identifier)
        {
            return _context.Tenant.FirstOrDefault(x => x.Identifier == identifier).ToDomainModel();
        }

        /// <summary>
        /// Search Tenants
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{TenantDomain}"/></returns>
        public ICollection<TenantDomain> SearchTenants(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.Tenant
                .DoFiltering(filterCriteria, FilterTenants)
                .DoSorting(sortCriteria, SortTenants)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Update Tenant
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public void Update(TenantDomain tenant)
        {
            var tenantDb = _context.Tenant.FirstOrDefault(x => x.TenantId == tenant.Id);
            tenantDb.FromDomainModel(tenant);
            _context.SaveChanges();
        }

        #region Private methods
        private Expression<Func<Tenant, object>> SortTenants(string columnName)
        {
            Expression<Func<Tenant, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "name":
                    fnc = x => x.Name;
                    break;
                case "isactive":
                    fnc = x => (x.IsActive).ToString();
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<Tenant, bool>> FilterTenants(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Tenant, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "name":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.Name).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.Name == filterTerm;
                    }
                    break;
            }

            return fnc;
        }
        
        #endregion
    }
}
