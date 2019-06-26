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
    public class IncidentSettlementManipulation : IIncidentSettlementManipulation
    {
        private readonly IIncidentSettlementRepository _incidentSettlementRepository;

        public IncidentSettlementManipulation(IIncidentSettlementRepository incidentSettlementRepository)
        {
            _incidentSettlementRepository = incidentSettlementRepository;
        }

        public ICollection<IncidentSettlementDomain> GetAllIncidentSettlements()
        {
            return _incidentSettlementRepository.GetAllIncidentSettlements();
        }

        public IncidentSettlementDomain GetIncidentSettlementById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentSettlementRepository.GetIncidentSettlementById(id);
        }

        public void UpdateIncidentSettlement(IncidentSettlementDomain incidentSettlement)
        {
            if (incidentSettlement == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentSettlementRepository.UpdateIncidentSettlement(incidentSettlement);

        }
        public int AddIncidentSettlement(IncidentSettlementDomain incidentSettlement)
        {
            if (incidentSettlement == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentSettlementRepository.AddIncidentSettlement(incidentSettlement);
        }

        public void DeleteIncidentSettlement(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentSettlementRepository.DeleteIncidentSettlement(id);
        }
    }
}
