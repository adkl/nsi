using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.IncidentManagement
{
    public interface IIncidentStatusManipulation
    {
        ICollection<IncidentStatusDomain> GetAllIncidentStatus();
        IncidentStatusDomain GetIncidentStatusById(int id);
        void UpdateIncidentStatus(IncidentStatusDomain incidentStatus);
        int AddIncidentStatus(IncidentStatusDomain incidentStatus);
        void DeleteIncidentStatus(int id);
    }
}
