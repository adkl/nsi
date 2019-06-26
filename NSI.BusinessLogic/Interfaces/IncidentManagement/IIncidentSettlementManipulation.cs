using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.IncidentManagement
{
    public interface IIncidentSettlementManipulation
    {
        ICollection<IncidentSettlementDomain> GetAllIncidentSettlements();
        IncidentSettlementDomain GetIncidentSettlementById(int id);
        void UpdateIncidentSettlement(IncidentSettlementDomain incidentSettlement);
        int AddIncidentSettlement(IncidentSettlementDomain incidentSettlement);
        void DeleteIncidentSettlement(int id);
    }
}
