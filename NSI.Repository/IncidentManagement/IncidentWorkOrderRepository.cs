using NSI.Common.Exceptions;
using NSI.Common.Resources;
using NSI.Common.Resources.IncidentManagement;
using NSI.Domain.IncidentManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.IncidentManagement
{
    public class IncidentWorkOrderRepository : IIncidentWorkOrderRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public IncidentWorkOrderRepository(NsiContext context)
        {
            _context = context;
        }

        public int AddIncidentWorkOrder(IncidentWorkOrderDomain workOrder)
        {
            var incidentWorkOrderDb = new WorkOrder().FromDomainModel(workOrder);
            _context.WorkOrder.Add(incidentWorkOrderDb);
            _context.SaveChanges();
            return incidentWorkOrderDb.WorkOrderId;
        }

        public void DeleteIncidentWorkOrder(int id)
        {
            var incidentWorkOrderDb = _context.WorkOrder.FirstOrDefault(x => x.WorkOrderId == id);

            if (incidentWorkOrderDb == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentWorkOrderInvalidId);
            }

            _context.WorkOrder.Remove(incidentWorkOrderDb);
            _context.SaveChanges();
        }

        public ICollection<IncidentWorkOrderDomain> GetAllIncidentWorkOrders()
        {
            ICollection<IncidentWorkOrderDomain> listToReturn = new List<IncidentWorkOrderDomain>();

            var result = _context.WorkOrder.ToList();

            if (result == null) throw new NsiProcessingException(IncidentMessages.UnexpectedError);

            foreach (WorkOrder workOrder in _context.WorkOrder.ToList())
            {
                listToReturn.Add(workOrder.ToDomainModel());
            }

            return listToReturn;
        }

        public IncidentWorkOrderDomain GetIncidentWorkOrderById(int id)
        {
            var result = _context.WorkOrder.FirstOrDefault(x => x.WorkOrderId == id).ToDomainModel();

            if (result == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentWorkOrderInvalidId);
            }

            return result;
        }
        public IncidentSettlementDomain GetIncidentSettlementByTypeId(int id)
        {
            var result = _context.WorkOrder.FirstOrDefault(x => x.Incident.IncidentTypeId == id);
            if (result == null) return null;
            return result.IncidentSettlement.ToDomainModel();
        }
        public void UpdateIncidentWorkOrder(IncidentWorkOrderDomain workOrder)
        {
            if (workOrder == null) throw new NsiArgumentNullException(ExceptionMessages.ArgumentException);

            var incidentWorkOrderDb = _context.WorkOrder.FirstOrDefault(x => x.WorkOrderId == workOrder.WorkOrderId);

            if (incidentWorkOrderDb == null) throw new NsiArgumentNullException(IncidentMessages.IncidentWorkOrderInvalidId);

            incidentWorkOrderDb.FromDomainModel(workOrder);
            _context.SaveChanges();
        }
        
    }
}
