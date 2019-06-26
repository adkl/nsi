using NSI.BusinessLogic.Interfaces.Membership;
using NSI.DataContracts.Base;
using NSI.DataContracts.Membership.Modules;
using NSI.Domain.Membership;
using NSI.Resources.Membership;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes API methods for manipulating system modules
    /// </summary>
    //Uncomment for authorization
    //[NsiAuthorization]
    public class ModuleController : ApiController
    {
        private readonly IModuleManipulation _moduleManipulation;

        /// <summary>
        /// Module controller constructor
        /// </summary>
        /// <param name="moduleManipulation">Instance of <see cref="IModuleManipulation"/></param>
        public ModuleController(IModuleManipulation moduleManipulation)
        {
            _moduleManipulation = moduleManipulation;
        }

        /// <summary>
        /// Retrieves single module by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="GetModuleRequest"/></param>
        /// <returns><see cref="GetModuleResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetModuleResponse))]
        public IHttpActionResult GetModule(int id)
        {
            if (id < 1)
            {
                return BadRequest(MembershipMessages.ModuleIdInvalid);
            }

            var data = _moduleManipulation.GetModuleById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetModuleResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Searches modules. If no parameters have been provided in request, return all modules.
        /// </summary>
        /// <param name="request"><see cref="SearchModulesRequest"/></param>
        /// <returns><see cref="SearchModulesResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchModulesResponse))]
        public IHttpActionResult SearchModules(SearchModulesRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchModulesResponse()
            {
                Data = _moduleManipulation.SearchModules(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new module
        /// </summary>
        /// <param name="request"><see cref="AddModuleRequest"/></param>
        /// <returns><see cref="AddModuleResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddModuleResponse))]
        public IHttpActionResult AddModule(AddModuleRequest request)
        {
            request.ValidateNotNull();

            ModuleDomain moduleDomain = new ModuleDomain()
            {
                Name = request.Name,
                Code = request.Code,
                IsActive = request.IsActive,
            };

            return Ok(new AddModuleResponse()
            {
                Data = _moduleManipulation.AddModule(moduleDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates module
        /// </summary>
        /// <param name="request"><see cref="UpdateModuleRequest"/></param>
        /// <returns><see cref="UpdateModuleResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(UpdateModuleResponse))]
        public IHttpActionResult UpdateModule(UpdateModuleRequest request)
        {
            request.ValidateNotNull();

            ModuleDomain moduleDomain = _moduleManipulation.GetModuleById(request.Id);

            if (moduleDomain == null)
            {
                return NotFound();
            }

            moduleDomain.Name = request.Name;
            moduleDomain.Code = request.Code;
            moduleDomain.IsActive = request.IsActive;

            _moduleManipulation.UpdateModule(moduleDomain);

            return Ok(new UpdateModuleResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

    }
}