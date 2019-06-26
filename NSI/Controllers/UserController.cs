using NSI.Api.ViewModels;
using NSI.BusinessLogic.Interfaces.Membership;
using NSI.BusinessLogic.Membership;
using NSI.Common.Exceptions;
using NSI.Common.Resources.Authorization;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using NSI.DataContracts.Membership.Users;
using NSI.DataContracts.Base;
using NSI.Resources.Membership;
using NSI.Domain.Membership;
using NSI.Api.Extensions;
using NSI.Api.Authorization;

namespace NSI.Controllers
{
    /// <summary>
    /// Exposes methods for User manipulation
    /// </summary>
    [NsiAuthorization]
    public class UserController : ApiController
    {
        private readonly IUserManipulation _userManipulation;

        /// <summary>
        /// User cotroller constructor
        /// </summary>
        /// <param name="userManipulation"><see cref="IUserManipulation"/></param>
        public UserController(IUserManipulation userManipulation)
        {
            _userManipulation = userManipulation;            
        }

        /// <summary>
        /// Retrieves all users
        /// </summary>
        /// <param name="request"><see cref="GetAllUsersRequest"/></param>
        /// <returns><see cref="GetAllUsersResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllUsersResponse))]
        public IHttpActionResult GetAll(GetAllUsersRequest request)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            var data = _userManipulation.GetAllUsers(userTenantId, null);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllUsersResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves a single user by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="GetUserByIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetUserByIdResponse))]
        public IHttpActionResult GetById(int id)
        {
            if (id < 1)
                return BadRequest(MembershipMessages.UserIdInvalid);

            var data = _userManipulation.GetUserById(id);

            if (data == null)
            {
                return Content(HttpStatusCode.NotFound, "User not found.");
            }

            return Ok(new GetUserByIdResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves a single user by provided Identifier in request
        /// </summary>
        /// <param name="guid"><see cref="Guid"/></param>
        /// <returns><see cref="GetUserByIdentifierResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetUserByIdentifierResponse))]
        public IHttpActionResult GetByIdentifier(Guid guid)
        {
            var data = _userManipulation.GetUserByIdentifier(guid);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetUserByIdentifierResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves a single user by provided email in request
        /// </summary>
        /// <param name="email"><see cref="string"/></param>
        /// <returns><see cref="GetUserByEmailResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetUserByEmailResponse))]
        public IHttpActionResult GetByEMail(string email)
        {
            if (email == null)
                return BadRequest(MembershipMessages.UserEmailInvalid);

            var data = _userManipulation.GetUserByEmail(email);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetUserByEmailResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all users with references
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users")]
        [ResponseType(typeof(IList<UserForListViewModel>))]
        public IHttpActionResult Get(string searchTerm = null)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];
            var users = _userManipulation.GetAllUsers(userTenantId, searchTerm);
            var models = users.Select(u => UserForListViewModel.MapFromDbObject(u)).ToList();
            return Ok(models);
        }

        /// <summary>
        /// Adds new user
        /// </summary>
        /// <param name="request"><see cref="AddUserRequest"/></param>
        /// <returns><see cref="AddUserResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddUserResponse))]
        public IHttpActionResult Add(AddUserRequest request)
        {
            request.ValidateNotNull();
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            // convert from user model to domain model
            UserDomain userDomain = new UserDomain()
            {   
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Email = request.Email,
                IsActive = request.IsActive,
                IsDeleted = request.IsDeleted,
                LanguageId = request.LanguageId,
                //Identifier = request.Identifier,
                TenantId = userTenantId
            };

            return Ok(new AddUserResponse()
            {
                Data = _userManipulation.AddUser(userDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="request"><see cref="UpdateUserRequest"/></param>
        /// <returns><see cref="UpdateUserResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateUserResponse))]
        public IHttpActionResult Update(UpdateUserRequest request)
        {
            request.ValidateNotNull();

            UserDomain user = _userManipulation.GetUserById(request.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.MiddleName = request.MiddleName;
            user.Email = request.Email;
            user.IsActive = request.IsActive;
            user.IsDeleted = request.IsDeleted;
            user.LanguageId = request.LanguageId;

            _userManipulation.UpdateUser(user);

            return Ok(new UpdateUserResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Searches user. If no parameters have been provided in request, return all users.
        /// </summary>
        /// <param name="request"><see cref="SearchUserRequest"/></param>
        /// <returns><see cref="SearchUserResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchUserResponse))]
        public IHttpActionResult SearchUser(SearchUserRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchUserResponse()
            {
                Data = _userManipulation.SearchUsers(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}
