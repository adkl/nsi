using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces;
using NSI.Repository.Interfaces.Membership;
using NSI.Resources.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Membership
{
    public class RoleMemberManipulation : IRoleMemberManipulation
    {
        private readonly IRoleMemberRepository _roleMemberRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleMemberRepository, userRepository, roleRepository"></param>
        public RoleMemberManipulation(IRoleMemberRepository roleMemberRepository, 
            IUserRepository userRepository, 
            IRoleRepository roleRepository)
        {
            _roleMemberRepository = roleMemberRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Adds new roleMmber to the database
        /// </summary>
        /// <param name="roleMember">RoleMmber information to be added. Instance of <see cref="RoleMemberDomain"/></param>
        /// <returns>RoleMemberID of the newly created module</returns>
        public int AddRoleMember(RoleMemberDomain roleMember)
        {
            ValidateRoleMemberModel(roleMember);
            return _roleMemberRepository.Add(roleMember);
        }

        /// <summary>
        /// Delete roleMember with provided user Id
        /// </summary>
        /// <param name="userId"></param>
        public void DeteleRoleMemberByUserId(int userId)
        {
            ValidationHelper.GreaterThanZero(userId, MembershipMessages.UserIdInvalid);
            _roleMemberRepository.DeleteByUserId(userId);
        }

        /// <summary>
        /// Retrieves all roleMembers from the database
        /// </summary>
        /// <returns><see cref="ICollection{RoleMemberDomain}"/></returns>
        public ICollection<RoleMemberDomain> GetAllRoleMembers()
        {
            return _roleMemberRepository.GetAll();
        }

        /// <summary>
        /// Retrieves roleMember with provided ID
        /// </summary>
        /// <param name="roleMemberId">RoleMember ID</param>
        /// <returns>RoleMember if it exists, instance of <see cref="RoleMemberDomain"/>. Else null.</returns>
        public RoleMemberDomain GetRoleMemberById(int roleMemberId)
        {
            ValidationHelper.GreaterThanZero(roleMemberId, MembershipMessages.RoleMemberIdInvalid);
            return _roleMemberRepository.GetById(roleMemberId);
        }

        /// <summary>
        /// Update Role Member
        /// </summary>
        /// <param name="roleMember"></param>
        /// <returns></returns>
        public void UpdateRoleMember(RoleMemberDomain roleMember)
        {
            ValidateRoleMemberModel(roleMember);
            ValidationHelper.GreaterThanZero(roleMember.Id, MembershipMessages.RoleMemberIdInvalid);
            ValidationHelper.NotNull(_roleMemberRepository.GetById(roleMember.Id), MembershipMessages.RoleMemberWithIdDoesNotExist);

            _roleMemberRepository.Update(roleMember);
        }

        private void ValidateRoleMemberModel(RoleMemberDomain roleMember)
        {
            ValidationHelper.NotNull(roleMember, MembershipMessages.RoleMemberNotProvided);
            ValidationHelper.NotNull(_userRepository.GetUserById(roleMember.UserId), MembershipMessages.UserWithIdDoesNotExist);
            ValidationHelper.NotNull(_roleRepository.GetById(roleMember.RoleId), MembershipMessages.RoleWithIdDoesNotExist);
        }
    }
}
