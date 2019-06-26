using NSI.Common.Extensions;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces;
using System.Linq;
using System.Collections.Generic;
using NSI.Repository.Extensions;
using NSI.EF;
using NSI.Common.Models;
using System.Linq.Expressions;
using System;
using System.Data.Entity;

namespace NSI.Repository.Membership
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public UserRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        /// <returns><see cref="ICollection{UserDomain}"/></returns>
        public ICollection<UserDomain> GetAll(int tenantId, string searchTerm)
        {
            var users = GetUsersWithReferences(searchTerm, u => u.RoleMember.Select(r => r.Role));
            return users.Select(u => u.ToDomainModel()).Where(x => x.TenantId == tenantId).ToList();
        }

        /// <summary>
        /// Adds new user to the database
        /// </summary>
        /// <param name="user">User information to be added. Instance of <see cref="ModuleDomain"/></param>
        /// <returns>UserId of the newly created user</returns>
        public int AddUser(UserDomain user)
        {
            var userDb = new UserInfo().FromDomainModel(user);
            _context.UserInfo.Add(userDb);
            _context.SaveChanges();
            return userDb.UserInfoId;
        }

        /// <summary>
        /// Retrieves user with provided email
        /// </summary>
        /// <param name="email">User ID</param>
        /// <returns>User if it exists, instance of <see cref="UserDomain"/>. Else null.</returns>
        public UserDomain GetUserByEmail(string email)
        {
            email = email.SafeTrim();
            return _context.UserInfo.FirstOrDefault(x=>x.Email == email).ToDomainModel();
        }

        /// <summary>
        /// Retrieves user with provided Id
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User if it exists, instance of <see cref="UserDomain"/>. Else null.</returns>
        public UserDomain GetUserById(int userId)
        {
            return _context.UserInfo.FirstOrDefault(x => x.UserInfoId == userId).ToDomainModel();
        }

        /// <summary>
        /// Retrieves user with provided identifier
        /// </summary>
        /// <param name="identifier">User identifier</param>
        /// <returns>User if it exists, instance of <see cref="UserDomain"/>. Else null.</returns>
        public UserDomain GetUserByIdentifier(Guid identifier)
        {
            return _context.UserInfo.FirstOrDefault(x => x.Identifier == identifier).ToDomainModel();
        }

        /// <summary>
        /// Retrieves users with provided tenant ID
        /// </summary>
        /// <param name="tenantId">Tenant IDr</param>
        /// <returns>Users if it exists, instance of <see cref="ICollection{UserDomain}"/>. Else null.</returns>
        public ICollection<UserDomain> GetUsersByTenantId(int tenantId)
        {
            ICollection<UserDomain> result = new List<UserDomain>();
            foreach (UserInfo user in _context.UserInfo.Where(x => x.TenantId == tenantId).ToList())
            {
                result.Add(user.ToDomainModel());
            }
            return result;
        }

        /// <summary>
        /// Check if email is unique
        /// </summary>
        /// <param name="email">Emailr</param>
        /// <returns></returns>
        public bool IsEmailUnique(string email)
        {
            return !_context.UserInfo.Any(x => x.Email == email.SafeTrim());
        }

        /// <summary>
        /// Search Users
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{UserDomain}"/></returns>
        public ICollection<UserDomain> SearchUsers(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.UserInfo
                .Include(x => x.RoleMember)
                .Include(x => x.Language)
                .DoFiltering(filterCriteria, FilterUsers)
                .DoSorting(sortCriteria, SortUsers)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void UpdateUser(UserDomain user)
        {
            var userDb = _context.UserInfo.FirstOrDefault(x => x.UserInfoId == user.Id);
            userDb.FromDomainModel(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all users from the database with references
        /// </summary>
        /// <param name="searchTerm, includes"></param>
        /// <returns><see cref="IList{UserInfo}"/></returns>
        public IList<UserInfo> GetUsersWithReferences(string searchTerm, params Expression<Func<UserInfo, object>>[] includes)
        {
            IQueryable<UserInfo> query = _context.UserInfo;
            if (includes != null && includes.Any())
            {
                foreach (var i in includes)
                {
                    query = query.Include(i);
                }
            }

            // Do search if searchTerm exists
            if (!String.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm));

            query = query.OrderByDescending(u => u.FirstName); //
            
            var users = query.ToList();

            return users;
        }

        #region Private methods
        private Expression<Func<UserInfo, object>> SortUsers(string columnName)
        {
            Expression<Func<UserInfo, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "firstname":
                    fnc = x => x.FirstName;
                    break;
                case "lasttname":
                    fnc = x => x.LastName;
                    break;
                case "isactive":
                    fnc = x => (x.IsActive).ToString();
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<UserInfo, bool>> FilterUsers(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<UserInfo, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "fullname":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.FirstName + " " + x.LastName).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => (x.FirstName + " " + x.LastName) == filterTerm;
                    }
                    break;
            }

            return fnc;
        }

        #endregion
    }
}
