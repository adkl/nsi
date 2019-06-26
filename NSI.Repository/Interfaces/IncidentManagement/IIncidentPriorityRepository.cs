using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.IncidentManagement
{
    public interface IIncidentPriorityRepository
    {
        ICollection<IncidentPriorityDomain> GetAllIncidentPriorities();
        IncidentPriorityDomain GetIncidentPriorityById(int id);
        void UpdateIncidentPriority(IncidentPriorityDomain priority);
        int AddIncidentPriority(IncidentPriorityDomain priority);
        void DeleteIncidentPriority(int id);
    }
}
