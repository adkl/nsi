using NSI.BusinessLogic.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using NSI.Common.Helpers;
using NSI.Resources.Membership;
using NSI.Common.Models;
using NSI.Common.Exceptions;
using NSI.Common.Extensions;

namespace NSI.BusinessLogic.Membership
{
    public class RoleManipulation : IRoleManipulation
    {

        private readonly IRoleRepository _roleRepository;
        private readonly ITenantRepository _tenantRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleRepository, tenantRepository"></param>
        public RoleManipulation(IRoleRepository roleRepository, ITenantRepository tenantRepository)
        {
            _roleRepository = roleRepository;
            _tenantRepository = tenantRepository;
        }

        /// <summary>
        /// Adds new role to the database
        /// </summary>
        /// <param name="role">Role information to be added. Instance of <see cref="RoleDomain"/></param>
        /// <returns>RoleId of the newly created role</returns>
        public int AddRole(RoleDomain role)
        {
            ValidateRoleModel(role);

            var roleWithProvidedName = _roleRepository.GetByName(role.Name.SafeTrim());
            if (roleWithProvidedName != null)
            {
                throw new NsiArgumentException(MembershipMessages.RoleNameAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            return _roleRepository.Add(role);
        }

        /// <summary>
        /// Retrieves all roles from the database
        /// </summary>
        /// <returns><see cref="ICollection{RoleDomain}"/></returns>
        public ICollection<RoleDomain> GetAllRoles(int tenantId, string searchTerm)
        {
            return _roleRepository.GetAll(tenantId, searchTerm);
        }

        /// <summary>
        /// Retrieves role with provided name
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Role if it exists, instance of <see cref="RoleDomain"/>. Else null.</returns>
        public RoleDomain GetRoleByName(string name)
        {
            ValidationHelper.NotNullOrWhitespace(name, MembershipMessages.RoleNameNotProvided);
            ValidationHelper.MaxLength(name, 50, MembershipMessages.RoleNameLenghtExceeded);
            return _roleRepository.GetByName(name);
        }

        /// <summary>
        /// Retrieves role with provided ID
        /// </summary>
        /// <param name="roleId">Role ID</param>
        /// <returns>Role if it exists, instance of <see cref="RoleDomain"/>. Else null.</returns>
        public RoleDomain GetRoleById(int roleId)
        {
            ValidationHelper.GreaterThanZero(roleId, MembershipMessages.RoleIdInvalid);
            return _roleRepository.GetById(roleId);
        }

        /// <summary>
        /// Search Roles
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{RoleDomain}"/></returns>
        public ICollection<RoleDomain> SearchRoles(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _roleRepository.SearchRoles(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public void UpdateRole(RoleDomain role)
        {
            ValidateRoleModel(role);
            ValidationHelper.GreaterThanZero(role.Id, MembershipMessages.RoleIdInvalid);
            ValidationHelper.NotNull(_roleRepository.GetById(role.Id), MembershipMessages.RoleWithIdDoesNotExist);

            _roleRepository.Update(role);
        }
        
        private void ValidateRoleModel(RoleDomain role)
        {
            ValidationHelper.NotNull(role, MembershipMessages.RoleNotProvided);
            ValidationHelper.NotNullOrWhitespace(role.Name, MembershipMessages.RoleNameNotProvided);
            ValidationHelper.NotNull(role.TenantId, MembershipMessages.RoleTenantIdNotProvided);
            ValidationHelper.NotNull(_tenantRepository.GetById(role.TenantId), MembershipMessages.RoleTenantIdDoesNotExist);
            ValidationHelper.NotNull(role.ManipulationLogId, MembershipMessages.RoleManipulationLogIdNotProvided);
            ValidationHelper.MaxLength(role.Name, 50, MembershipMessages.RoleNameLenghtExceeded);

        }
    }
}
