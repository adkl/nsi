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
    public class IncidentStatusManipulation : IIncidentStatusManipulation
    {
        private readonly IIncidentStatusRepository _incidentStatusRepository;

        public IncidentStatusManipulation(IIncidentStatusRepository incidentStatusRepository)
        {
            _incidentStatusRepository = incidentStatusRepository;
        }

        public int AddIncidentStatus(IncidentStatusDomain incidentStatus)
        {
            if (incidentStatus == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentStatusRepository.AddIncidentStatus(incidentStatus);
        }

        public void DeleteIncidentStatus(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentStatusRepository.DeleteIncidentStatus(id);
        }

        public ICollection<IncidentStatusDomain> GetAllIncidentStatus()
        {
            return _incidentStatusRepository.GetAllIncidentStatus();
        }

        public IncidentStatusDomain GetIncidentStatusById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentStatusRepository.GetIncidentStatusById(id);
        }

        public void UpdateIncidentStatus(IncidentStatusDomain incidentStatus)
        {
            if (incidentStatus == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentStatusRepository.UpdateIncidentStatus(incidentStatus);
        }
    }
}
