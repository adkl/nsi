using NSI.Api.Authorization;
using NSI.Api.ViewModels;
using NSI.BusinessLogic.Interfaces.Membership;
using NSI.DataContracts.Base;
using NSI.DataContracts.Membership.Roles;
using NSI.Domain.Membership;
using NSI.Resources.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes API methods for manipulating roles
    /// </summary>
    [NsiAuthorization]
    public class RoleController : ApiController
    {
        private readonly IRoleManipulation _roleManipulation;

        /// <summary>
        /// Notification controller constructor
        /// </summary>
        /// <param name="roleManipulation">Instance of <see cref="IRoleManipulation"/></param>
        public RoleController(IRoleManipulation roleManipulation)
        {
            _roleManipulation = roleManipulation;
        }

        /// <summary>
        /// Retrieves a single role by provided role ID in request
        /// </summary>
        /// <param name="roleId"><see cref="int"/></param>
        /// <returns><see cref="GetRoleResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetRoleResponse))]
        public IHttpActionResult GetById(int roleId)
        {
            try
            {
                if (roleId < 1)
                {
                    return BadRequest(MembershipMessages.RoleIdInvalid);
                }

                UserDomain user = (UserDomain)ActionContext.Request.Properties["UserDetails"];

                var data = _roleManipulation.GetRoleById(roleId);

                if (data == null)
                {
                    return Content(HttpStatusCode.NotFound, "Role not found.");
                }

                if (data.TenantId != user.TenantId)
                {
                    return Content(HttpStatusCode.NotFound, MembershipMessages.NotAuthorizedAction);
                }

                return Ok(new GetRoleResponse()
                {
                    Data = data,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Retrieves all roles
        /// </summary>
        /// /// <param name="request"><see cref="GetAllRolesRequest"/></param>
        /// <returns><see cref="GetAllRolesResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllRolesResponse))]
        public IHttpActionResult GetAll(GetAllRolesRequest request)
        {
            try
            {
                int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

                var data = _roleManipulation.GetAllRoles(userTenantId, null);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(new GetAllRolesResponse()
                {
                    Data = data,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Retrieves all roles
        /// </summary>
        [HttpGet]
        [Route("api/roles")]
        [ResponseType(typeof(IList<RoleForListViewModel>))]
        public IHttpActionResult Get(string searchTerm = null)
        {
            try
            {
                int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];
                var roles = _roleManipulation.GetAllRoles(userTenantId, searchTerm);
                var models = roles.Select(u => RoleForListViewModel.MapFromDbObject(u)).ToList();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Searches roles. If no parameters have been provided in request, return all roles.
        /// </summary>
        /// <param name="request"><see cref="SearchRolesRequest"/></param>
        /// <returns><see cref="SearchRolesResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchRolesResponse))]
        public IHttpActionResult SearchRoles(SearchRolesRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchRolesResponse()
            {
                Data = _roleManipulation.SearchRoles(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new role
        /// </summary>
        /// <param name="request"><see cref="AddRoleRequest"/></param>
        /// <returns><see cref="AddRoleResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddRoleResponse))]
        public IHttpActionResult AddRole([FromBody] AddRoleRequest request)
        {
            try
            {
                request.ValidateNotNull();

                int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

                RoleDomain roleDomain = new RoleDomain()
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    ManipulationLogId = request.ManipulationLogId,
                    TenantId = userTenantId
                };

                return Ok(new AddRoleResponse()
                {
                    Data = _roleManipulation.AddRole(roleDomain),
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Updates role
        /// </summary>
        /// <param name="request"><see cref="UpdateRoleRequest"/></param>
        /// <returns><see cref="UpdateRoleResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateRoleResponse))]
        public IHttpActionResult UpdateRole([FromBody] UpdateRoleRequest request)
        {
            try
            {
                request.ValidateNotNull();

                RoleDomain roleDomain = _roleManipulation.GetRoleById(request.Id);

                if (roleDomain == null)
                {
                    return NotFound();
                }

                roleDomain.Name = request.Name;
                roleDomain.IsActive = request.IsActive;
                roleDomain.ManipulationLogId = request.ManipulationLogId;

                _roleManipulation.UpdateRole(roleDomain);

                return Ok(new UpdateRoleResponse()
                {
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }

}