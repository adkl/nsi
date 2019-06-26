using NSI.Common.Exceptions;
using NSI.Common.Extensions;
using NSI.Common.Models;
using NSI.Common.Resources;
using NSI.Common.Resources.DeviceManagement;
using NSI.Common.Resources.IncidentManagement;
using NSI.Domain.IncidentManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace NSI.Repository.IncidentManagement
{
    public class IncidentRepository : IIncidentRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public IncidentRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<IncidentDomain> GetAllIncidents(int userTenantId)
        {
            if (userTenantId <= 0) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            ICollection<IncidentDomain> listToReturn = new List<IncidentDomain>();

            var result = _context.Incident.Where(x => x.TenantId == userTenantId).ToList();

            foreach (Incident incident in result)
            {
                listToReturn.Add(incident.ToDomainModel());
            }

            return listToReturn;
        }

        
        public ICollection<IncidentDomain> SearchIncidents(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria, int userTenantId)
        {
            var result = _context.Incident.Where(x => x.TenantId == userTenantId)
               .DoFiltering(filterCriteria, FilterIncidents)
               .DoSorting(sortCriteria, SortIncidents)
               .DoPaging(paging)
               .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        public IncidentDomain GetIncidentById(int id, int userTenantId)
        {
            var result = _context.Incident.FirstOrDefault(x => x.IncidentId == id).ToDomainModel();

            if (result == null) throw new NsiNotFoundException(IncidentMessages.IncidentNotFound);

            if (result.TenantId != userTenantId) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            return result;
        }

        public void UpdateIncident(POSTIncidentDomain incident, int userTenantId)
        {
            if (incident == null) throw new NsiArgumentException(IncidentMessages.IncidentNotFound);

            if (!_context.Device.Any(x => x.DeviceId == incident.DeviceId))
            {
                throw new NsiArgumentException(DeviceMessages.DeviceInvalidId);
            }

            if (!_context.Tenant.Any(x => x.TenantId == incident.TenantId))
            {
                throw new NsiArgumentException(IncidentMessages.TenantInvalidId);
            }

            if (!_context.Priority.Any(x => x.PriorityId == incident.Priority))
            {
                throw new NsiArgumentException(IncidentMessages.IncidentPriorityInvalidId);
            }

            if (!_context.IncidentStatus.Any(x => x.IncidentStatusId == incident.IncidentStatus))
            {
                throw new NsiArgumentException(IncidentMessages.IncidentStatusInvalidId);
            }

            if (!_context.IncidentType.Any(x => x.IncidentTypeId == incident.IncidentType))
            {
                throw new NsiArgumentException(IncidentMessages.IncidentTypeInvalidId);
            }

            if (userTenantId != incident.TenantId)
            {
                throw new NsiArgumentException(IncidentMessages.TenantInvalidId);
            }

            var incidentDb = _context.Incident.FirstOrDefault(x => x.IncidentId == incident.IncidentId);
            incidentDb.FromDomainModel(incident);
            _context.SaveChanges();
        }

        public int AddIncident(POSTIncidentDomain incident, int userTenantId)
        {
            if (incident == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            if (incident.TenantId != userTenantId) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            if (!_context.Device.Any(x => x.DeviceId == incident.DeviceId))
            {
                throw new NsiArgumentException(DeviceMessages.DeviceInvalidId);
            }

            if(!_context.Tenant.Any(x => x.TenantId == incident.TenantId))
            {
                throw new NsiArgumentException(IncidentMessages.TenantInvalidId);
            }

            if (!_context.Priority.Any(x => x.PriorityId == incident.Priority))
            {
                throw new NsiArgumentException(IncidentMessages.IncidentPriorityInvalidId);
            }

            if (!_context.IncidentStatus.Any(x => x.IncidentStatusId == incident.IncidentStatus))
            {
                throw new NsiArgumentException(IncidentMessages.IncidentStatusInvalidId);
            }

            if (!_context.IncidentType.Any(x => x.IncidentTypeId == incident.IncidentType))
            {
                throw new NsiArgumentException(IncidentMessages.IncidentTypeInvalidId);
            }
            
            var incidentDb = new Incident().FromDomainModel(incident);
            _context.Incident.Add(incidentDb);
            _context.SaveChanges();
            return incidentDb.IncidentId;
        }

        public void DeleteIncident(int id, int userTenantId)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            var incidentDb = _context.Incident.FirstOrDefault(x => x.IncidentId == id);

            if(incidentDb == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentNotFound);
            }

            if(incidentDb.TenantId != userTenantId)
            {
                throw new NsiArgumentException(IncidentMessages.TenantInvalidId);
            }
          
            _context.Incident.Remove(incidentDb);
            
            _context.SaveChanges();
        }

        private Expression<Func<Incident, bool>> FilterIncidents(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Incident, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "priority":
                        fnc = x => x.PriorityId.ToString() == filterTerm;
                    break;
                case "status":
                    fnc = x => x.IncidentStatusId.ToString() == filterTerm;
                    break;
                case "type":
                    fnc = x => x.IncidentTypeId.ToString() == filterTerm;
                    break;
                case "datefrom":
                    fnc = x => x.DateCreated.ToString().CompareTo(filterTerm) >= 0;
                    break;
                case "dateto":
                    fnc = x => x.DateCreated.ToString().CompareTo(filterTerm) <= 0;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<Incident, object>> SortIncidents(string columnName)
        {
            Expression<Func<Incident, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "id":
                    fnc = x => x.IncidentId.ToString();
                    break;
                case "datecreated":
                    fnc = x => x.DateCreated.ToString();
                    break;
                case "datemodified":
                    fnc = x => x.DateModified.ToString();
                    break;
                case "priority":
                    fnc = x => x.Priority.Name;
                    break;
                case "status":
                    fnc = x => x.IncidentStatus.Name;
                    break;
                default:
                    break;
            }

            return fnc;
        }
    }
}
