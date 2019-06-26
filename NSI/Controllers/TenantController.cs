using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Membership;
using NSI.DataContracts.Base;
using NSI.DataContracts.Membership.Tenant;
using NSI.Domain.Membership;
using NSI.Resources.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes API methods for manipulating tenants
    /// </summary>
    //Uncomment for authorization
    [NsiAuthorization]
    public class TenantController : ApiController
    {
        private readonly ITenantManipulation _tenantManipulation;

        /// <summary>
        /// Tenant controller constructor
        /// </summary>
        /// <param name="tenantManipulation">Instance of <see cref="ITenantManipulation"/></param>
        public TenantController(ITenantManipulation tenantManipulation)
        {
            _tenantManipulation = tenantManipulation;
        }

        /// <summary>
        /// Retrieves single tenant by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="GetTenantRequest"/></param>
        /// <returns><see cref="GetTenantResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetTenantResponse))]
        public IHttpActionResult GetTenant(int id)
        {

            if (id < 1)
            {
                return BadRequest(MembershipMessages.TenantIdInvalid);
            }

            var data = _tenantManipulation.GetTenantById(id);

            if (data == null)
            {
                return Content(HttpStatusCode.NotFound, "Tenant not found.");
            }

            return Ok(new GetTenantResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Retrieves a single tenant by provided Identifier in request
        /// </summary>
        /// <param name="guid"><see cref="Guid"/></param>
        /// <returns><see cref="GetTenantByIdentifierResponse"/></returns>

        [HttpGet]
        [ResponseType(typeof(GetTenantByIdentifierResponse))]
        public IHttpActionResult GetByIdentifier(Guid guid)
        {
            var data = _tenantManipulation.GetTenantByIdentifier(guid);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetTenantByIdentifierResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Searches tenants. If no parameters have been provided in request, return all tenants.
        /// </summary>
        /// <param name="request"><see cref="SearchTenantRequest"/></param>
        /// <returns><see cref="SearchTenantResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchTenantResponse))]
        public IHttpActionResult SearchTenants(SearchTenantRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchTenantResponse()
            {
                Data = _tenantManipulation.SearchTenants(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new tenant
        /// </summary>
        /// <param name="request"><see cref="AddTenantRequest"/></param>
        /// <returns><see cref="AddTenantResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddTenantResponse))]
        public IHttpActionResult AddTenant(AddTenantRequest request)
        {
            request.ValidateNotNull();
            TenantDomain tenantDomain = new TenantDomain()
            {
                //Identifier = request.Identifier,
                Name = request.Name,
                IsActive = request.IsActive,
                DefaultLanguageId = request.DefaultLanguageId
            };

            return Ok(new AddTenantResponse()
            {
                Data = _tenantManipulation.AddTenant(tenantDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates tenant
        /// </summary>
        /// <param name="request"><see cref="UpdateTenantRequest"/></param>
        /// <returns><see cref="UpdateTenantResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(UpdateTenantResponse))]
        public IHttpActionResult UpdateTenant(UpdateTenantRequest request)
        {
            request.ValidateNotNull();
            TenantDomain tenantDomain = _tenantManipulation.GetTenantById(request.Id);

            if (tenantDomain == null)
            {
                return NotFound();
            }

            tenantDomain.Name = request.Name;
            tenantDomain.IsActive = request.IsActive;
            tenantDomain.DefaultLanguageId = request.DefaultLanguageId;

            _tenantManipulation.UpdateTenant(tenantDomain);

            return Ok(new UpdateTenantResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}