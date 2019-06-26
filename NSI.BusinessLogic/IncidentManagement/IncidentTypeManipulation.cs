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
    public class IncidentTypeManipulation : IIncidentTypeManipulation
    {
        private readonly IIncidentTypeRepository _incidentTypeRepository;

        public IncidentTypeManipulation(IIncidentTypeRepository incidentTypeRepository)
        {
            _incidentTypeRepository = incidentTypeRepository;
        }

        public ICollection<IncidentTypeDomain> GetAllIncidentTypes()
        {
            return _incidentTypeRepository.GetAllIncidentTypes();
        }

        public IncidentTypeDomain GetIncidentTypeById(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentTypeRepository.GetIncidentTypeById(id);
        }

        public int AddIncidentType(IncidentTypeDomain incidentType)
        {
            if (incidentType == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            return _incidentTypeRepository.AddIncidentType(incidentType);
        }

        public void DeleteIncidentType(int id)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentTypeRepository.DeleteIncidentType(id);
        }

        public void UpdateIncidentType(IncidentTypeDomain incidentType)
        {
            if (incidentType == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            _incidentTypeRepository.UpdateIncidentType(incidentType);
        }
    }
}
