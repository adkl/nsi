using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Common.Helpers;
using NSI.DataContracts.Base;
using NSI.DataContracts.Membership.RolePermissions;
using NSI.DataContracts.Membership.Roles;
using NSI.Domain.Membership;
using NSI.Resources.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes API methods for manipulating system rolepermissions
    /// </summary>
    //Uncomment for authorization
    [NsiAuthorization]
    public class RolePermissionController : ApiController
    {
        private readonly IRolePermissionManipulation _rolePermissionManipulation;

        /// <summary>
        /// Role permission controller constructor
        /// </summary>
        /// <param name="rolePermissionManipulation">Instance of <see cref="IRolePermissionManipulation"/></param>
        public RolePermissionController(IRolePermissionManipulation rolePermissionManipulation)
        {
            _rolePermissionManipulation = rolePermissionManipulation;
        }

        /// <summary>
        /// Retrieves single role permission by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="GetRolePermissionRequest"/></param>
        /// <returns><see cref="GetRolePermissionResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetRolePermissionResponse))]
        public IHttpActionResult GetRolePermission(int id)
        {
            if (id < 1)
            {
                return BadRequest(MembershipMessages.RolePermissionIdInvalid);
            }

            var data = _rolePermissionManipulation.GetRolePermissionById(id);

            if (data == null)
            {
                return Content(HttpStatusCode.NotFound, "Role permission not found.");
            }

            return Ok (new GetRolePermissionResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

      

        /// <summary>
        /// Adds new  role permission
        /// </summary>
        /// <param name="request"><see cref="AddRolePermissionRequest"/></param>
        /// <returns><see cref="AddRolePermissionResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddRolePermissionResponse))]
        public IHttpActionResult AddRolePermission(AddRolePermissionRequest request)
        {
            request.ValidateNotNull();
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            RolePermissionDomain rolePermissionDomain = new RolePermissionDomain()
            {
                IsActive = request.IsActive,
                TenantId = userTenantId,
                PermissionId = request.PermissionId,
                RoleId = request.RoleId
            };

            return Ok(new AddRolePermissionResponse()
            {
               Data = _rolePermissionManipulation.AddRolePermission(rolePermissionDomain),
               Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates role permission
        /// </summary>
        /// <param name="request"><see cref="UpdateRolePermissionRequest"/></param>
        /// <returns><see cref="UpdateRolePermissionResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(UpdateRolePermissionResponse))]
        public IHttpActionResult UpdateRolePermission(UpdateRolePermissionRequest request)
        {
            request.ValidateNotNull();
            RolePermissionDomain rolePermissionDomain = _rolePermissionManipulation.GetRolePermissionById(request.Id);

            if (rolePermissionDomain == null)
            {
                return NotFound();
            }

            
            rolePermissionDomain.IsActive = request.IsActive;
            rolePermissionDomain.RoleId = request.RoleId;
            rolePermissionDomain.PermissionId = request.PermissionId;

            _rolePermissionManipulation.UpdateRolePermission(rolePermissionDomain);

            return Ok(new UpdateRolePermissionResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes role permissions by role ID
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns><see cref="DeleteRolePermissionResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteRolePermissionResponse))]
        public IHttpActionResult DeleteByRoleId(int roleId)
        {
            if (roleId < 1)
                return BadRequest(MembershipMessages.RoleIdInvalid);

            _rolePermissionManipulation.DeleteRolePermissionByRoleId(roleId);

            return Ok(new DeleteRolePermissionResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes role permissions by permission ID
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns><see cref="DeleteRolePermissionResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteRolePermissionResponse))]
        public IHttpActionResult DeleteByPermissionId(int permissionId)
        {
            if (permissionId < 1)
                return BadRequest(MembershipMessages.PermissionIdInvalid);

            _rolePermissionManipulation.DeleteRolePermissionByPermissionId(permissionId);

            return Ok(new DeleteRolePermissionResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}