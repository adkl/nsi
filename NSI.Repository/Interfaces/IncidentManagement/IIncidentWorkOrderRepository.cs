﻿using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.IncidentManagement
{
    public interface IIncidentWorkOrderRepository
    {
        ICollection<IncidentWorkOrderDomain> GetAllIncidentWorkOrders();
        IncidentWorkOrderDomain GetIncidentWorkOrderById(int id);
        IncidentSettlementDomain GetIncidentSettlementByTypeId(int id);
        void UpdateIncidentWorkOrder(IncidentWorkOrderDomain workOrder);
        int AddIncidentWorkOrder(IncidentWorkOrderDomain workOrder);
        void DeleteIncidentWorkOrder(int id);
    }
}
