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
    public class PermissionManipulation : IPermissionManipulation
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IModuleRepository _moduleRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="permissionRepository, moduleRepository"></param>
        public PermissionManipulation(
            IPermissionRepository permissionRepository,
            IModuleRepository moduleRepository)
        {
            _permissionRepository = permissionRepository;
            _moduleRepository = moduleRepository;
        }

        /// <summary>
        /// Adds new permission  to the database
        /// </summary>
        /// <param name="permission">Permission information to be added. Instance of <see cref="PermissionDomain"/></param>
        /// <returns>PermissionId of the newly created permission</returns>
        public int AddPermission(PermissionDomain permission)
        {
            ValidatePermissionModel(permission);
            //Check if code exists
            var permissionWithProvidedCode = _permissionRepository.GetByCode(permission.Code.SafeTrim());

            if (permissionWithProvidedCode != null)
            {
                throw new NsiArgumentException(MembershipMessages.PermissionCodeAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            return _permissionRepository.Add(permission);
        }

        /// <summary>
        /// Search Permissions
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns></returns>
        public ICollection<PermissionDomain> SearchPermissions(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _permissionRepository.SearchPermissions(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Retrieves all permissions from the database
        /// </summary>
        /// <returns><see cref="ICollection{PermissionDomain}"/></returns>
        public ICollection<PermissionDomain> GetAllPermissions(string searchTerm)
        {
            return _permissionRepository.GetAll(searchTerm);
        }

        /// <summary>
        /// Retrieves permission with provided ID
        /// </summary>
        /// <param name="permissionId">Permission ID</param>
        /// <returns>Permission if it exists, instance of <see cref="PermissionDomain"/>. Else null.</returns>
        public PermissionDomain GetPermissionById(int permissionId)
        {
            ValidationHelper.NotNull(permissionId, MembershipMessages.PermissionIdInvalid);
            ValidationHelper.GreaterThanZero(permissionId, MembershipMessages.PermissionIdInvalid);
            return _permissionRepository.GetById(permissionId);
        }

        /// <summary>
        /// Retrieves permission with provided code
        /// </summary>
        /// <param name="permissionCode">Permission ID</param>
        /// <returns>Permission if it exists, instance of <see cref="PermissionDomain"/>. Else null.</returns>
        public PermissionDomain GetPermissionByCode(string permissionCode)
        {
            ValidationHelper.NotNullOrWhitespace(permissionCode, MembershipMessages.PermissionCodeInvalid);
            ValidationHelper.MaxLength(permissionCode, 100, MembershipMessages.PermissionCodeLenghtExceeded);
            return _permissionRepository.GetByCode(permissionCode);
        }

        /// <summary>
        /// Update Permission
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public void UpdatePermission(PermissionDomain permission)
        {
            ValidatePermissionModel(permission);
            ValidationHelper.GreaterThanZero(permission.Id, MembershipMessages.PermissionIdInvalid);
            ValidationHelper.NotNull(_permissionRepository.GetById(permission.Id), MembershipMessages.PermissionWithIdDoesNotExist);

            var permissionWithProvidedCode = _permissionRepository.GetByCode(permission.Code);

            if (permissionWithProvidedCode != null && permissionWithProvidedCode.Id != permission.Id)
            {
                throw new NsiArgumentException(MembershipMessages.PermissionCodeAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            _permissionRepository.Update(permission);
        }

        private void ValidatePermissionModel(PermissionDomain permission)
        {
            ValidationHelper.NotNull(permission, MembershipMessages.PermissionNotProvided);
            ValidationHelper.MaxLength(permission.Code, 100, MembershipMessages.PermissionCodeLenghtExceeded);
            ValidationHelper.NotNullOrWhitespace(permission.Code, MembershipMessages.PermissionCodeInvalid);
            ValidationHelper.GreaterThanZero(permission.ModuleId, MembershipMessages.ModuleIdInvalid);
            ValidationHelper.NotNull(_moduleRepository.GetById(permission.ModuleId), MembershipMessages.ModuleWithIdDoesNotExist);
        }
        
    }
}
