using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Membership;
using NSI.DataContracts.Base;
using NSI.DataContracts.Membership.RoleMember;
using NSI.Domain.Membership;
using NSI.Resources.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{

    /// <summary>
    /// Exposes methods for User role manipulation
    /// </summary>
    [NsiAuthorization]
    public class RoleMemberController : ApiController
    {
        private readonly IRoleMemberManipulation _roleMemberManipulation;

        /// <summary>
        /// Role member cotroller constructor
        /// </summary>
        /// <param name="roleMemberManipulation">Instance of<see cref="IRoleMemberManipulation"/></param>
        public RoleMemberController(IRoleMemberManipulation roleMemberManipulation)
        {
            _roleMemberManipulation = roleMemberManipulation;
        }

        /// <summary>
        /// Retrieves all role members
        /// </summary>
        /// /// <param name="request"><see cref="GetAllRoleMembersRequest"/></param>
        /// <returns><see cref="GetAllRoleMembersResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllRoleMembersResponse))]
        public IHttpActionResult GetAll(GetAllRoleMembersRequest request)
        {
            var data = _roleMemberManipulation.GetAllRoleMembers();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllRoleMembersResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Retrieves a single role member by provided role member ID in request
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="GetRoleMemberByIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetRoleMemberByIdResponse))]
        public IHttpActionResult GetById(int id)
        {
            if (id < 1)
                return BadRequest(MembershipMessages.RoleMemberIdInvalid);

            var data = _roleMemberManipulation.GetRoleMemberById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetRoleMemberByIdResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Adds new role member
        /// </summary>
        /// <param name="request"><see cref="AddRoleMemberRequest"/></param>
        /// <returns><see cref="AddRoleMemberResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddRoleMemberResponse))]
        public IHttpActionResult Add(AddRoleMemberRequest request)
        {
            request.ValidateNotNull();
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            // convert from user model to domain model
            RoleMemberDomain roleMemberDomain = new RoleMemberDomain()
            {               
                IsActive = request.IsActive,
                UserId = request.UserId,
                RoleId = request.RoleId,
                TenantId = userTenantId
            };

            return Ok(new AddRoleMemberResponse()
            {
                Data = _roleMemberManipulation.AddRoleMember(roleMemberDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Updates role member
        /// </summary>
        /// <param name="request"><see cref="UpdateRoleMemberRequest"/></param>
        /// <returns><see cref="UpdateRoleMemberResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateRoleMemberResponse))]
        public IHttpActionResult Update(UpdateRoleMemberRequest request)
        {
            request.ValidateNotNull();

            RoleMemberDomain roleMember = _roleMemberManipulation.GetRoleMemberById(request.RoleId);

            if (roleMember == null)
            {
                return NotFound();
            }
            roleMember.IsActive = request.IsActive;
            roleMember.UserId = request.UserId;
            roleMember.RoleId = request.RoleId;
            
            _roleMemberManipulation.UpdateRoleMember(roleMember);

            return Ok(new UpdateRoleMemberResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes role member by user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="DeleteRoleMemberResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteRoleMemberResponse))]
        public IHttpActionResult DeleteByUserId(int userId)
        {
            if (userId < 1)
                return BadRequest(MembershipMessages.UserIdInvalid);

            _roleMemberManipulation.DeteleRoleMemberByUserId(userId);

            return Ok(new DeleteRoleMemberResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

    }
}