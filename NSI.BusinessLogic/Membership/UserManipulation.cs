using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.Common.Models;
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
    public class UserManipulation : IUserManipulation
    {
        private readonly IUserRepository _userRepository;
        private readonly ILanguageRepository _languageRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository, languageRepository"></param>
        public UserManipulation(IUserRepository userRepository, ILanguageRepository languageRepository)
        {
            _userRepository = userRepository;
            _languageRepository = languageRepository;
        }

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        /// <returns><see cref="ICollection{UserDomain}"/></returns>
        public ICollection<UserDomain> GetAllUsers(int tenantId, string searchTerm)
        {
            return _userRepository.GetAll(tenantId, searchTerm);
        }

        /// <summary>
        /// Adds new user to the database
        /// </summary>
        /// <param name="user">User information to be added. Instance of <see cref="ModuleDomain"/></param>
        /// <returns>UserId of the newly created user</returns>
        public int AddUser(UserDomain user)
        {
            ValidateUserModel(user);
            user.Identifier = Guid.NewGuid();

            var userWithProvidedEmail = _userRepository.GetUserByEmail(user.Email);

            if(userWithProvidedEmail != null)
            {
                throw new NsiArgumentException(MembershipMessages.UserWithEmailExists, Common.Enumerations.SeverityEnum.Warning);
            }
            var userWithProvidedIdentifier = _userRepository.GetUserByIdentifier(user.Identifier);

            if (userWithProvidedIdentifier != null)
            {
                throw new NsiArgumentException(MembershipMessages.UserWithIdentifierExists, Common.Enumerations.SeverityEnum.Warning);
            }
            return _userRepository.AddUser(user);
        }

        /// <summary>
        /// Retrieves user with provided Id
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User if it exists, instance of <see cref="UserDomain"/>. Else null.</returns>
        public UserDomain GetUserById(int id)
        {
            ValidationHelper.GreaterThanZero(id, MembershipMessages.UserIdInvalid);
            return _userRepository.GetUserById(id);
        }

        /// <summary>
        /// Retrieves user with provided identifier
        /// </summary>
        /// <param name="identifier">User identifier</param>
        /// <returns>User if it exists, instance of <see cref="UserDomain"/>. Else null.</returns>
        public UserDomain GetUserByIdentifier(Guid identifier)
        {
            ValidationHelper.NotNull(identifier, MembershipMessages.UserIdentifierInvalid);
            return _userRepository.GetUserByIdentifier(identifier);
        }

        /// <summary>
        /// Retrieves user with provided email
        /// </summary>
        /// <param name="email">User ID</param>
        /// <returns>User if it exists, instance of <see cref="UserDomain"/>. Else null.</returns>
        public UserDomain GetUserByEmail(string email)
        {
            ValidationHelper.NotNullOrWhitespace(email, MembershipMessages.UserEmailInvalid);
            ValidationHelper.MaxLength(email, 240, MembershipMessages.UserEmailLengthExceeded);
            return _userRepository.GetUserByEmail(email);
        }

        /// <summary>
        /// Retrieves users with provided tenant ID
        /// </summary>
        /// <param name="tenantId">Tenant IDr</param>
        /// <returns>Users if it exists, instance of <see cref="ICollection{UserDomain}"/>. Else null.</returns>
        public ICollection<UserDomain> GetUsersByTenantId(int tenantId)
        {
            ValidationHelper.GreaterThanZero(tenantId, MembershipMessages.TenantIdInvalid);
            return _userRepository.GetUsersByTenantId(tenantId);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void UpdateUser(UserDomain user)
        {
            ValidateUserModel(user);
            ValidationHelper.GreaterThanZero(user.Id, MembershipMessages.UserIdInvalid);
            ValidationHelper.NotNull(_userRepository.GetUserById(user.Id), MembershipMessages.UserWithIdDoesNotExist);
            _userRepository.UpdateUser(user);
        }

        /// <summary>
        /// Search Users
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{UserDomain}"/></returns>
        public ICollection<UserDomain> SearchUsers(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _userRepository.SearchUsers(paging, filterCriteria, sortCriteria);
        }

        private void ValidateUserModel(UserDomain user)
        {
            ValidationHelper.NotNull(user, MembershipMessages.UserNotProvided);
            ValidationHelper.NotNullOrWhitespace(user.FirstName, MembershipMessages.UserNameNotProvided);
            ValidationHelper.NotNullOrWhitespace(user.LastName, MembershipMessages.UserLastNameNotProvided);
            ValidationHelper.MaxLength(user.FullName, 220, MembershipMessages.UserFullNameLengthExceeded);
            ValidationHelper.MaxLength(user.Email, 240, MembershipMessages.UserEmailLengthExceeded);
            ValidationHelper.NotNull(_languageRepository.GetById(user.LanguageId), MembershipMessages.LanguageWithIdDoesNotExist);
        }
    }
}
