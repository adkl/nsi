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
    public class IncidentPriorityRepository : IIncidentPriorityRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public IncidentPriorityRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<IncidentPriorityDomain> GetAllIncidentPriorities()
        {
            ICollection<IncidentPriorityDomain> listToReturn = new List<IncidentPriorityDomain>();

            var result = _context.Priority.ToList();

            if (result == null) throw new NsiProcessingException(IncidentMessages.UnexpectedError);

            foreach (Priority priority in _context.Priority.ToList())
            {
                listToReturn.Add(priority.ToDomainModel());
            }

            return listToReturn;
        }

        public IncidentPriorityDomain GetIncidentPriorityById(int id)
        {
            var result = _context.Priority.FirstOrDefault(x => x.PriorityId == id).ToDomainModel();

            if(result == null)
            {
                throw new NsiArgumentException(IncidentMessages.IncidentPriorityInvalidId);
            }

            return result;
        }

        public int AddIncidentPriority(IncidentPriorityDomain priority)
        {
            if (priority == null)
            {
                throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            }
            var incidentPriorityDb = new Priority().FromDomainModel(priority);
            _context.Priority.Add(incidentPriorityDb);
            _context.SaveChanges();
            return incidentPriorityDb.PriorityId;
        }

        public void DeleteIncidentPriority(int id)
        {
            var incidentPriorityDb = _context.Priority.FirstOrDefault(x => x.PriorityId == id);

            if (incidentPriorityDb == null)
            {           
                throw new NsiArgumentException(IncidentMessages.IncidentPriorityInvalidId);      
            }

            _context.Priority.Remove(incidentPriorityDb);
            _context.SaveChanges();
        }

        public void UpdateIncidentPriority(IncidentPriorityDomain priority)
        {
            var incidentPriorityDb = _context.Priority.FirstOrDefault(x => x.PriorityId == priority.PriorityId);

            if (incidentPriorityDb == null)
            {
                throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            }

            incidentPriorityDb.FromDomainModel(priority);
            _context.SaveChanges();
        }
        
    }
}
