using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.IncidentManagement;
using NSI.DataContracts.Incident;
using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Incident manipulation
    /// </summary>
    [NsiAuthorization]
    public class IncidentController: ApiController
    {
        private readonly IIncidentManipulation _incidentManipulation;
    
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="incidentManipulation"><see cref="IIncidentManipulation"/></param>
        public IncidentController(IIncidentManipulation incidentManipulation)
        {
            _incidentManipulation = incidentManipulation;
        }

        /// <summary>
        /// Retrieves all incidents from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentDomain}"/></returns>
        [System.Web.Http.HttpGet]
        public IEnumerable<IncidentDomain> Get()
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            return _incidentManipulation.GetAllIncidents(userTenantId);
        }

        /// <summary>
        /// Retrieves incident by provided id
        /// </summary>
        /// <returns><see cref="IncidentDomain"/></returns>
       [System.Web.Http.HttpGet]
        public IncidentDomain Get(int id)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            return _incidentManipulation.GetIncidentById(id, userTenantId);
        }

        /// <summary>
        /// Adds new incident
        /// </summary>
        /// <returns><see cref="int"/></returns>
        [System.Web.Http.HttpPost]
        public int AddIncident([FromBody] POSTIncidentDomain incident)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            return _incidentManipulation.AddIncident(incident, userTenantId);
        }

        /// <summary>
        /// Retrieves all incidents from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentDomain}"/></returns>
        public IEnumerable<IncidentDomain> SearchIncidents(SearchIncidentRequest request)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            return _incidentManipulation.SearchIncidents(request.Paging, request.FilterCriteria, request.SortCriteria, userTenantId);
        }
        /// <summary>
        /// Edit incident
        /// </summary>
        /// <returns><see cref="int"/></returns>
        [System.Web.Http.HttpPut]
        public void EditIncident([FromBody] POSTIncidentDomain incident)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            _incidentManipulation.UpdateIncident(incident, userTenantId);
        }

        /// <summary>
        /// Delete incident
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void DeleteIncident(int incidentId)
        {
            int userTenantId = (int)ActionContext.Request.Properties["UserTenantId"];

            _incidentManipulation.DeleteIncident(incidentId, userTenantId);
        }
    }
}