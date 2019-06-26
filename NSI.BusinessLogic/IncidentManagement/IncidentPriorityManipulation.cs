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
    public class IncidentPriorityManipulation : IIncidentPriorityManipulation
    {
        private readonly IIncidentPriorityRepository _incidentPriorityRepository;

        public IncidentPriorityManipulation(IIncidentPriorityRepository incidentPriorityRepository)
        {
            _incidentPriorityRepository = incidentPriorityRepository;
        }

        public ICollection<IncidentPriorityDomain> GetAllIncidentPriorities()
        {
            return _incidentPriorityRepository.GetAllIncidentPriorities();
        }

        public IncidentPriorityDomain GetIncidentPriorityById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentPriorityRepository.GetIncidentPriorityById(id);
        }

        public int AddIncidentPriority(IncidentPriorityDomain priority)
        {
            if (priority == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentPriorityRepository.AddIncidentPriority(priority);
        }

        public void DeleteIncidentPriority(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            
            _incidentPriorityRepository.DeleteIncidentPriority(id);
        }

        public void UpdateIncidentPriority(IncidentPriorityDomain priority)
        {
            if (priority == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentPriorityRepository.UpdateIncidentPriority(priority);
        }
    }
}
