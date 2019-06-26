using NSI.Common.Exceptions;
using NSI.Common.Resources;
using NSI.Common.Resources.IncidentManagement;
using NSI.Domain.IncidentManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.IncidentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.IncidentManagement
{
    public class IncidentStatusRepository : IIncidentStatusRepository 
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public IncidentStatusRepository(NsiContext context)
        {
            _context = context;
        }

        public int AddIncidentStatus(IncidentStatusDomain incidentStatus)
        {
            var incidentStatusDb = new IncidentStatus().FromDomainModel(incidentStatus);
            _context.IncidentStatus.Add(incidentStatusDb);
            _context.SaveChanges();
            return incidentStatusDb.IncidentStatusId;
        }

        public void DeleteIncidentStatus(int id)
        {
            var incidentStatusDb = _context.IncidentStatus.FirstOrDefault(x => x.IncidentStatusId == id);

            if (incidentStatusDb == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentStatusInvalidId);
            }

            _context.IncidentStatus.Remove(incidentStatusDb);
            _context.SaveChanges();
        }

        public ICollection<IncidentStatusDomain> GetAllIncidentStatus()
        {
            ICollection<IncidentStatusDomain> listToReturn = new List<IncidentStatusDomain>();

            var result = _context.Incident.ToList();

            if (result == null) throw new NsiProcessingException(IncidentMessages.UnexpectedError);

            foreach (IncidentStatus incidentStatus in _context.IncidentStatus.ToList())
            {
                listToReturn.Add(incidentStatus.ToDomainModel());
            }

            return listToReturn;
        }

        public IncidentStatusDomain GetIncidentStatusById(int id)
        {
            var result = _context.IncidentStatus.FirstOrDefault(x => x.IncidentStatusId == id).ToDomainModel();

            if (result == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentStatusInvalidId);
            }

            return result;
        }

        public void UpdateIncidentStatus(IncidentStatusDomain incidentStatus)
        {
            if (incidentStatus == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            var incidentStatusDb = _context.IncidentStatus.FirstOrDefault(x => x.IncidentStatusId == incidentStatus.IncidentStatusId);

            if (incidentStatusDb == null) throw new NsiArgumentNullException(IncidentMessages.IncidentStatusInvalidId);

            incidentStatusDb.FromDomainModel(incidentStatus);
            _context.SaveChanges();
        }
        
    }
}
