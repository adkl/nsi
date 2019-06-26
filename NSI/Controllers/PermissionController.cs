using NSI.Api.Authorization;
using NSI.Api.ViewModels;
using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Common.Helpers;
using NSI.DataContracts.Base;
using NSI.DataContracts.Membership.Permissions;
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
    /// Exposes API methods for manipulating system permissions
    /// </summary>
    //Uncomment for authorization
   [NsiAuthorization]
    public class PermissionController : ApiController
    {
        private readonly IPermissionManipulation _permissionManipulation;

        /// <summary>
        /// Permission controller constructor
        /// </summary>
        /// <param name="permissionManipulation">Instance of <see cref="IPermissionManipulation"/></param>
        public PermissionController(IPermissionManipulation permissionManipulation)
        {
            _permissionManipulation = permissionManipulation;
        }

        /// <summary>
        /// Retrieves single permission by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="GetPermissionRequest"/></param>
        /// <returns><see cref="GetPermissionResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetPermissionResponse))]
        public IHttpActionResult GetPermission(int id)
        {

            if (id<1)
            {
                return BadRequest(MembershipMessages.PermissionIdInvalid);
            }

            var data = _permissionManipulation.GetPermissionById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok (new GetPermissionResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all permissions
        /// </summary>
        /// <param name="request"><see cref="GetAllPermissionsRequest"/></param>
        /// <returns><see cref="GetAllPermissionsResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllPermissionsResponse))]
        public IHttpActionResult GetAll(GetAllPermissionsRequest request)
        {
            var data = _permissionManipulation.GetAllPermissions(null);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllPermissionsResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all permissions
        /// </summary>
        [HttpGet]
        [Route("api/permissions")]
        [ResponseType(typeof(IList<PermissionForListViewModel>))]
        public IHttpActionResult Get(string searchTerm = null)
        {
            var permissions = _permissionManipulation.GetAllPermissions(searchTerm);
            return Ok(permissions);
        }

        /// <summary>
        /// Searches permissions. If no parameters have been provided in request, return all permissions.
        /// </summary>
        /// <param name="request"><see cref="SearchPermissionsRequest"/></param>
        /// <returns><see cref="SearchPermissionsResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchPermissionsResponse))]
        public IHttpActionResult SearchPermissions(SearchPermissionsRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchPermissionsResponse()
            {
                Data = _permissionManipulation.SearchPermissions(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new permission
        /// </summary>
        /// <param name="request"><see cref="AddPermissionRequest"/></param>
        /// <returns><see cref="AddRoleResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddPermissionResponse))]
        public IHttpActionResult AddPermission(AddPermissionRequest request)
        {
            request.ValidateNotNull();

            PermissionDomain permissionDomain = new PermissionDomain()
            {
                Code = request.Code,
                IsActive = request.IsActive,
                ModuleId = request.ModuleId
            };

            return Ok(new AddPermissionResponse()
            {
               Data = _permissionManipulation.AddPermission(permissionDomain),
               Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates permission
        /// </summary>
        /// <param name="request"><see cref="UpdatePermissionRequest"/></param>
        /// <returns><see cref="UpdatePermissionResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdatePermissionResponse))]
        public IHttpActionResult UpdatePermission(UpdatePermissionRequest request)
        {
            request.ValidateNotNull();
            PermissionDomain permissionDomain = _permissionManipulation.GetPermissionById(request.Id);

            if (permissionDomain == null)
            {
                return NotFound();
            }

            permissionDomain.Code = request.Code;
            permissionDomain.IsActive = request.IsActive;
            permissionDomain.ModuleId = request.ModuleId;

            _permissionManipulation.UpdatePermission(permissionDomain);

            return Ok(new UpdatePermissionResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}