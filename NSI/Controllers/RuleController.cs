using NSI.BusinessLogic.Interfaces.RuleEngine;
using NSI.Domain.RuleEngine;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using NSI.Api.Authorization;
using NSI.Common.Enumerations;
using NSI.DataContracts.RuleEngine;
using NSI.Domain.Membership;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Rule manipulation
    /// </summary>
    [NsiAuthorization]
    public class RuleController : ApiController
    {
        private readonly IRuleManipulation _ruleManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="ruleManipulation"><see cref="IRuleManipulation"/></param>
        public RuleController(IRuleManipulation ruleManipulation)
        {
            _ruleManipulation = ruleManipulation;
        }

        /// <summary>
        /// Retrieves all rules from system
        /// </summary>
        /// <returns><see cref="IEnumerable{RuleDomain}"/></returns>
        public IHttpActionResult Get([FromUri] GetRulesRequest request)
        {
            UserDomain user = (UserDomain) ActionContext.Request.Properties["UserDetails"];

            return Ok(new GetRulesResponse()
            {
                Data = _ruleManipulation.GetAllRules(user, request.FilterCriteria, request.Paging),
                Success = ResponseStatus.Succeeded,
                Paging = request.Paging
            });
        }

        /// <summary>
        /// Adds new rule
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public HttpResponseMessage AddRule([FromBody] AddRuleDomain rule)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ModelState);
            }

            UserDomain user = (UserDomain) ActionContext.Request.Properties["UserDetails"];

            _ruleManipulation.AddRule(rule, user);

            return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
        }

        /// <summary>
        /// Delete rule
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void DeleteRule(int ruleId)
        {
            UserDomain user = (UserDomain) ActionContext.Request.Properties["UserDetails"];

            _ruleManipulation.DeleteRule(ruleId, user);
        }
    }
}
