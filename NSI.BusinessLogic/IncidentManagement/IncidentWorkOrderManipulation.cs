using NSI.BusinessLogic.Interfaces.IncidentManagement;
using NSI.Common.Exceptions;
using NSI.Common.Resources;
using NSI.Domain.IncidentManagement;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.IncidentManagement
{
    public class IncidentWorkOrderManipulation : IIncidentWorkOrderManipulation
    {
        private readonly IIncidentWorkOrderRepository _incidentWorkOrderRepository;

        public IncidentWorkOrderManipulation(IIncidentWorkOrderRepository incidentWorkOrderRepository)
        {
            _incidentWorkOrderRepository = incidentWorkOrderRepository;
        }

        public int AddIncidentWorkOrder(IncidentWorkOrderDomain workOrder)
        {
            if (workOrder == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentWorkOrderRepository.AddIncidentWorkOrder(workOrder);
        }

        public void DeleteIncidentWorkOrder(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentWorkOrderRepository.DeleteIncidentWorkOrder(id);
        }

        public ICollection<IncidentWorkOrderDomain> GetAllIncidentWorkOrders()
        {
            return _incidentWorkOrderRepository.GetAllIncidentWorkOrders();
        }

        public IncidentWorkOrderDomain GetIncidentWorkOrderById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentWorkOrderRepository.GetIncidentWorkOrderById(id);
        }

        public void UpdateIncidentWorkOrder(IncidentWorkOrderDomain workOrder)
        {
            if (workOrder == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentWorkOrderRepository.UpdateIncidentWorkOrder(workOrder);
        }
    }
}
