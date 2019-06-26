using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.Membership;
using NSI.Common.Models;
using NSI.EF;
using System.Data.Entity;
using NSI.Common.Extensions;
using System.Linq.Expressions;
using NSI.Repository.Extensions;

namespace NSI.Repository.Membership
{
    public class RoleRepository : IRoleRepository
    {

        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public RoleRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new role to the database
        /// </summary>
        /// <param name="role">Role information to be added. Instance of <see cref="RoleDomain"/></param>
        /// <returns>RoleId of the newly created role</returns>
        public int Add(RoleDomain role)
        {
            var roleDb = new Role().FromDomainModel(role);
            _context.Role.Add(roleDb);
            _context.SaveChanges();
            return roleDb.RoleId;
        }

        /// <summary>
        /// Retrieves all roles from the database
        /// </summary>
        /// <returns><see cref="ICollection{RoleDomain}"/></returns>
        public ICollection<RoleDomain> GetAll(int tenantId, string searchTerm)
        {
            var roles = GetRolesWithReferences(searchTerm, u => u.RolePermission.Select(r => r.Permission));
            return roles.Where(u => u.TenantId== tenantId).Select(u => u.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves role with provided name
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Role if it exists, instance of <see cref="RoleDomain"/>. Else null.</returns>
        public RoleDomain GetByName(string roleName)
        {
            return _context.Role.FirstOrDefault(x => x.Name == roleName).ToDomainModel();
        }

        /// <summary>
        /// Retrieves role with provided ID
        /// </summary>
        /// <param name="roleId">Role ID</param>
        /// <returns>Role if it exists, instance of <see cref="RoleDomain"/>. Else null.</returns>
        public RoleDomain GetById(int roleId)
        {
            return _context.Role.FirstOrDefault(x => x.RoleId == roleId).ToDomainModel();
        }

        /// <summary>
        /// Search Roles
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{RoleDomain}"/></returns>
        public ICollection<RoleDomain> SearchRoles(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.Role
                .Include(x => x.Tenant)
                .Include(x => x.RoleMember)
                .Include(x => x.RolePermission)
                .DoFiltering(filterCriteria, FilterRoles)
                .DoSorting(sortCriteria, SortRoles)
                .DoPaging(paging)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public void Update(RoleDomain role)
        {
            var roleDb = _context.Role.FirstOrDefault(x => x.RoleId == role.Id);
            roleDb.FromDomainModel(role);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all roles from the database with references
        /// </summary>
        /// <param name="searchTerm, includes"></param>
        /// <returns><see cref="IList{Role}"/></returns>
        public IList<Role> GetRolesWithReferences(string searchTerm, params Expression<Func<Role, object>>[] includes)
        {
            IQueryable<Role> query = _context.Role;
            if (includes != null && includes.Any())
            {
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }
            }

            //Do search if search term exist
            if (!String.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(u => u.Name.Contains(searchTerm));
            query = query.OrderByDescending(u => u.Name);
            var roles = query.ToList();


            return roles;
        }

        #region Private methods
        private Expression<Func<Role, object>> SortRoles(string columnName)
        {
            Expression<Func<Role, object>> fnc = null;

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

        private Expression<Func<Role, bool>> FilterRoles(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Role, bool>> fnc = null;

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
