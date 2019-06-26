using NSI.Common.Models;
using NSI.Domain.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.IncidentManagement
{
    public interface IIncidentRepository
    {
        ICollection<IncidentDomain> GetAllIncidents(int userTenantId);
        ICollection<IncidentDomain> SearchIncidents(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria, int userTenantId);
        IncidentDomain GetIncidentById(int id, int userTenantId);
        void UpdateIncident(POSTIncidentDomain incident, int userTenantId);
        int AddIncident(POSTIncidentDomain incident, int userTenantId);
        void DeleteIncident(int id, int userTenantId);

    }
}
