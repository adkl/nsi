using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.IncidentManagement
{
    public interface IIncidentTypeManipulation
    {
        ICollection<IncidentTypeDomain> GetAllIncidentTypes();
        IncidentTypeDomain GetIncidentTypeById(int id);
        void UpdateIncidentType(IncidentTypeDomain incidentType);
        int AddIncidentType(IncidentTypeDomain incidentType);
        void DeleteIncidentType(int id);
    }
}
