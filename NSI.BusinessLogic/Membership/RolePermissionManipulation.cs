using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Common.Helpers;
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
    public class RolePermissionManipulation : IRolePermissionManipulation
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissonRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rolePermissionrepository, roleRepository, permissionRepository"></param>
        public RolePermissionManipulation(IRolePermissionRepository rolePermissionrepository, 
            IRoleRepository roleRepository, 
            IPermissionRepository permissionRepository)
        {
            _rolePermissionRepository = rolePermissionrepository;
            _roleRepository = roleRepository;
            _permissonRepository = permissionRepository;
        }

        /// <summary>
        /// Adds new rolePermission to the database
        /// </summary>
        /// <param name="rolePermission">RolePermission information to be added. Instance of <see cref="RolePermissionDomain"/></param>
        /// <returns>RolePermissionID of the newly created rolePermission</returns>
        public int AddRolePermission(RolePermissionDomain rolePermission)
        {
            ValidateRolePermissionModel(rolePermission);
            return _rolePermissionRepository.Add(rolePermission);
        }

        /// <summary>
        /// Delete rolePermissions with provided permissionId
        /// </summary>
        /// <param name="permissionId"></param>
        public void DeleteRolePermissionByPermissionId(int permissionId)
        {
            ValidationHelper.GreaterThanZero(permissionId, MembershipMessages.PermissionIdInvalid);
            _rolePermissionRepository.DeleteByPermissionId(permissionId);
        }

        /// <summary>
        /// Delete rolePermissions with provided roleId
        /// </summary>
        /// <param name="roleId"></param>
        public void DeleteRolePermissionByRoleId(int roleId)
        {
            ValidationHelper.GreaterThanZero(roleId, MembershipMessages.RoleIdInvalid);
            _rolePermissionRepository.DeleteByRoleId(roleId);
        }

        /// <summary>
        /// Retrieves all rolePermission from the database
        /// </summary>
        /// <returns><see cref="ICollection{RolePermissionDomain}"/></returns>
        public ICollection<RolePermissionDomain> GetAllRolePermissions()
        {
            return _rolePermissionRepository.GetAll();
        }

        /// <summary>
        /// Retrieves rolePermission with provided ID
        /// </summary>
        /// <param name="rolePermissionId">RolePermission ID</param>
        /// <returns>RolePermission if it exists, instance of <see cref="RolePermissionDomain"/>. Else null.</returns>
        public RolePermissionDomain GetRolePermissionById(int rolePermissionId)
        {
            ValidationHelper.GreaterThanZero(rolePermissionId, MembershipMessages.RolePermissionIdInvalid);
            return _rolePermissionRepository.GetById(rolePermissionId);
        }

        /// <summary>
        /// Update RolePermission
        /// </summary>
        /// <param name="rolePermission"></param>
        /// <returns></returns>
        public void UpdateRolePermission(RolePermissionDomain rolePermission)
        {
            ValidateRolePermissionModel(rolePermission);
            ValidationHelper.GreaterThanZero(rolePermission.Id, MembershipMessages.RolePermissionIdInvalid);
            ValidationHelper.NotNull(_rolePermissionRepository.GetById(rolePermission.Id), MembershipMessages.RolePermissionIdDoesNotExist);
            _rolePermissionRepository.Update(rolePermission);
        }

        private void ValidateRolePermissionModel(RolePermissionDomain rolePermission)
        {
            ValidationHelper.NotNull(rolePermission, MembershipMessages.RolePermissionNotProvided);
            ValidationHelper.NotNull(rolePermission.RoleId, MembershipMessages.RoleIdNotProvided);
            ValidationHelper.NotNull(_roleRepository.GetById(rolePermission.RoleId), MembershipMessages.RoleIdDoesNotExist);
            ValidationHelper.NotNull(rolePermission.PermissionId, MembershipMessages.PermissionIdNotProvided);
            ValidationHelper.NotNull(_permissonRepository.GetById(rolePermission.PermissionId), MembershipMessages.PermissionWithIdDoesNotExist);
        }
    }
}
