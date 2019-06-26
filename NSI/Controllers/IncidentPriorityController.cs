using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.IncidentManagement;
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
    /// Exposes methods for Incident Priority manipulation
    /// </summary>
    [NsiAuthorization]
    public class IncidentPriorityController : ApiController
    {
        private readonly IIncidentPriorityManipulation _incidentPriorityManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="incidentPriorityManipulation"><see cref="IIncidentPriorityManipulation"/></param>
        public IncidentPriorityController(IIncidentPriorityManipulation incidentPriorityManipulation)
        {
            _incidentPriorityManipulation = incidentPriorityManipulation;
        }

        /// <summary>
        /// Retrieves all incidents priorities from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentPriorityDomain}"/></returns>
        public IEnumerable<IncidentPriorityDomain> Get()
        {
            return _incidentPriorityManipulation.GetAllIncidentPriorities();
        }

        /// <summary>
        /// Retrieves incident priority by provided id
        /// </summary>
        /// <returns><see cref="IncidentPriorityDomain"/></returns>
        public IncidentPriorityDomain Get(int id)
        {
            return _incidentPriorityManipulation.GetIncidentPriorityById(id);
        }

        /// <summary>
        /// Add new Priority
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public int AddIncidentPriority(IncidentPriorityDomain priority)
        {
            return _incidentPriorityManipulation.AddIncidentPriority(priority);
        }

        /// <summary>
        /// Edit Priority
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void EditIncidentPriority(IncidentPriorityDomain priority)
        {
            _incidentPriorityManipulation.UpdateIncidentPriority(priority);
        }

        /// <summary>
        /// Delete Priority
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void DeleteIncidentPriority(int incidentPriorityId)
        {
            _incidentPriorityManipulation.DeleteIncidentPriority(incidentPriorityId);
        }
    }
}