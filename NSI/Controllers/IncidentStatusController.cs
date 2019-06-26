using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.IncidentManagement;
using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NSI.Api.Controllers
{
    [NsiAuthorization]
    public class IncidentStatusController: ApiController
    {
        private readonly IIncidentStatusManipulation _incidentStatusManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="incidentStatusManipulation"><see cref="IIncidentStatusManipulation"/></param>
        public IncidentStatusController(IIncidentStatusManipulation incidentStatusManipulation)
        {
            _incidentStatusManipulation = incidentStatusManipulation;
        }

        /// <summary>
        /// Retrieves all incidentStatus from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentStatusDomain}"/></returns>
        public IEnumerable<IncidentStatusDomain> Get()
        {
            return _incidentStatusManipulation.GetAllIncidentStatus();
        }

        /// <summary>
        /// Retrieves incident by provided id
        /// </summary>
        /// <returns><see cref="IncidentStatusDomain"/></returns>
        public IncidentStatusDomain Get(int id)
        {
            return _incidentStatusManipulation.GetIncidentStatusById(id);
        }

        /// <summary>
        /// Add new incidentStatus
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public int AddIncidentStatus(IncidentStatusDomain incidentStatus)
        {
            return _incidentStatusManipulation.AddIncidentStatus(incidentStatus);
        }

        /// <summary>
        /// Edit incidentStatus
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void EditIncidentStatus(IncidentStatusDomain incidentStatus)
        {
            _incidentStatusManipulation.UpdateIncidentStatus(incidentStatus);
        }

        /// <summary>
        /// Delete incidentStatus
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void DeleteIncidentStatus(int incidentStatusId)
        {
            _incidentStatusManipulation.DeleteIncidentStatus(incidentStatusId);
        }
    }
}