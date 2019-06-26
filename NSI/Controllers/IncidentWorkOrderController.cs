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
    /// Exposes methods for Incident WorkOrder manipulation
    /// </summary>
    [NsiAuthorization]
    public class IncidentWorkOrderController : ApiController
    {
        private readonly IIncidentWorkOrderManipulation _incidentWorkOrderManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="incidentWorkOrderManipulation"><see cref="IIncidentWorkOrderManipulation"/></param>
        public IncidentWorkOrderController(IIncidentWorkOrderManipulation incidentWorkOrderManipulation)
        {
            _incidentWorkOrderManipulation = incidentWorkOrderManipulation;
        }

        /// <summary>
        /// Retrieves all incidents work orders from system
        /// </summary>
        /// <returns><see cref="IEnumerable{IncidentWorkOrderDomain}"/></returns>
        public IEnumerable<IncidentWorkOrderDomain> Get()
        {
            return _incidentWorkOrderManipulation.GetAllIncidentWorkOrders();
        }

        /// <summary>
        /// Retrieves incident work order by provided id
        /// </summary>
        /// <returns><see cref="IncidentWorkOrderDomain"/></returns>
        public IncidentWorkOrderDomain Get(int id)
        {
            return _incidentWorkOrderManipulation.GetIncidentWorkOrderById(id);
        }

        /// <summary>
        /// Add new workOrder
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public int AddIncidentWorkOrder(IncidentWorkOrderDomain workOrder)
        {
            return _incidentWorkOrderManipulation.AddIncidentWorkOrder
                (workOrder);
        }

        /// <summary>
        /// Edit workOrder
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void EditIncidentWorkOrder(IncidentWorkOrderDomain workOrder)
        {
            _incidentWorkOrderManipulation.UpdateIncidentWorkOrder(workOrder);
        }

        /// <summary>
        /// Delete workOrder
        /// </summary>
        /// <returns><see cref="int"/></returns>
        public void DeleteIncidentWorkOrder(int incidentWorkOrderId)
        {
            _incidentWorkOrderManipulation.DeleteIncidentWorkOrder(incidentWorkOrderId);
        }
    }
}