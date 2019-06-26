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
    public class PermissionRepository : IPermissionRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public PermissionRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new permission  to the database
        /// </summary>
        /// <param name="permission">Permission information to be added. Instance of <see cref="PermissionDomain"/></param>
        /// <returns>PermissionId of the newly created permission</returns>
        public int Add(PermissionDomain permission)
        {
            var permissionDb = new Permission().FromDomainModel(permission);
            _context.Permission.Add(permissionDb);
            _context.SaveChanges();
            return permissionDb.PermissionId;
        }

        /// <summary>
        /// Retrieves all permissions from the database
        /// </summary>
        /// <returns><see cref="ICollection{PermissionDomain}"/></returns>
        public ICollection<PermissionDomain> GetAll(string searchTerm)
        {
            var permissions = GetPermissionsWithReferences(searchTerm, u => u.RolePermission.Select(r => r.Permission));
            return permissions.Select(u => u.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves permission with provided ID
        /// </summary>
        /// <param name="permissionId">Permission ID</param>
        /// <returns>Permission if it exists, instance of <see cref="PermissionDomain"/>. Else null.</returns>
        public PermissionDomain GetById(int permissionId)
        {
            return _context.Permission.FirstOrDefault(x => x.PermissionId == permissionId).ToDomainModel();
        }

        /// <summary>
        /// Retrieves permission with provided code
        /// </summary>
        /// <param name="permissionCode">Permission ID</param>
        /// <returns>Permission if it exists, instance of <see cref="PermissionDomain"/>. Else null.</returns>
        public PermissionDomain GetByCode(string permissionCode)
        {
            return _context.Permission.FirstOrDefault(x => x.Code == permissionCode).ToDomainModel();
        }

        /// <summary>
        /// Update Permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public void Update(PermissionDomain permission)
        {
            var permissionDb = _context.Permission.FirstOrDefault(x => x.PermissionId == permission.Id);
            permissionDb.FromDomainModel(permission);
            _context.SaveChanges();
        }

        /// <summary>
        /// Search Permissions
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns></returns>
        public ICollection<PermissionDomain> SearchPermissions(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result =  _context.Permission
                .Include(x => x.Module)
                .DoFiltering(filterCriteria, FilterPermissions)
                .DoSorting(sortCriteria, SortPermissions)
                .DoPaging(paging)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves all permissions from the database with references
        /// </summary>
        /// <param name="searchTerm, includes"></param>
        /// <returns><see cref="IList{Permission}"/></returns>
        public IList<Permission> GetPermissionsWithReferences(string searchTerm, params Expression<Func<Permission, object>>[] includes)
        {
            IQueryable<Permission> query = _context.Permission;
            if (includes != null && includes.Any())
            {
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }
            }

            //Do search if search term exist
            if (!String.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(u => u.Code.Contains(searchTerm));
            query = query.OrderByDescending(u => u.Code);
            var permissions= query.ToList();


            return permissions;
        }

        #region Private methods
        private Expression<Func<Permission, object>> SortPermissions(string columnName)
        {
            Expression<Func<Permission, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "code":
                    fnc = x => x.Code;
                    break;
                case "isactive":
                    fnc = x => (x.IsActive).ToString();
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<Permission, bool>> FilterPermissions(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Permission, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "code":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.Code).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.Code == filterTerm;
                    }
                    break;
            }

            return fnc;
        }
        #endregion
    }
}
