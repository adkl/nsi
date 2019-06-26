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
    /// Exposes methods for Incident Type manipulation
    /// </summary>
    [NsiAuthorization]
    public class IncidentTypeController : ApiController
    {
        private readonly IIncidentTypeManipulation _incidentTypeManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="incidentTypeManipulation"><see cref="IIncidentTypeManipulation"/></param>
        public IncidentTypeController(IIncidentTypeManipulation incidentTypeManipulation)
        {
            _incidentTypeManipulation = incidentTypeManipulation;
        }

        /// <summary>
        /// Retrieves all incidents types from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentTypeDomain}"/></returns>
        public IEnumerable<IncidentTypeDomain> Get()
        {
            return _incidentTypeManipulation.GetAllIncidentTypes();
        }

        /// <summary>
        /// Retrieves incident type by provided id
        /// </summary>
        /// <returns><see cref="IncidentTypeDomain"/></returns>
        public IncidentTypeDomain Get(int id)
        {
            return _incidentTypeManipulation.GetIncidentTypeById(id);
        }

        /// <summary>
        /// Add new incident Type
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public int AddIncidentType(IncidentTypeDomain incidentType)
        {
            return _incidentTypeManipulation.AddIncidentType(incidentType);
        }

        /// <summary>
        /// Edit incident Type
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void EditIncidentType(IncidentTypeDomain incidentType)
        {
            _incidentTypeManipulation.UpdateIncidentType(incidentType);
        }

        /// <summary>
        /// Delete incident Type
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void DeleteIncidentType(int incidentTypeId)
        {
            _incidentTypeManipulation.DeleteIncidentType(incidentTypeId);
        }
    }
}