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
    /// Exposes methods for Incident manipulation
    /// </summary>
    [NsiAuthorization]
    public class IncidentSettlementController : ApiController
    {
        private readonly IIncidentSettlementManipulation _incidentSettlementManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="incidentSettlementManipulation"><see cref="IIncidentSettlementManipulation"/></param>
        public IncidentSettlementController(IIncidentSettlementManipulation incidentSettlementManipulation)
        {
            _incidentSettlementManipulation = incidentSettlementManipulation;
        }

        /// <summary>
        /// Retrieves all incidents settlements from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentSettlementDomain}"/></returns>
        public IEnumerable<IncidentSettlementDomain> Get()
        {
            return _incidentSettlementManipulation.GetAllIncidentSettlements();
        }

        /// <summary>
        /// Retrieves incident settlement by provided id
        /// </summary>
        /// <returns><see cref="IncidentSettlementDomain"/></returns>
        public IncidentSettlementDomain Get(int id)
        {
            return _incidentSettlementManipulation.GetIncidentSettlementById(id);
        }

        /// <summary>
        /// Adds new incident
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public int AddIncidentSettlement(IncidentSettlementDomain incidentSettlement)
        {
            return _incidentSettlementManipulation.AddIncidentSettlement(incidentSettlement);
        }

        /// <summary>
        /// Edit incident
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void EditIncidentSettlement(IncidentSettlementDomain incidentSettlement)
        {
            _incidentSettlementManipulation.UpdateIncidentSettlement(incidentSettlement);
        }

        /// <summary>
        /// Delete incident
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void DeleteIncidentSettlement(int incidentSettlementId)
        {
            _incidentSettlementManipulation.DeleteIncidentSettlement(incidentSettlementId);
        }
    }
}