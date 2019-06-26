using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.IncidentManagement
{
    public interface IIncidentSettlementRepository
    {
        ICollection<IncidentSettlementDomain> GetAllIncidentSettlements();
        IncidentSettlementDomain GetIncidentSettlementById(int id);
        void UpdateIncidentSettlement(IncidentSettlementDomain incident);
        int AddIncidentSettlement(IncidentSettlementDomain incident);
        void DeleteIncidentSettlement(int id);
    }
}
