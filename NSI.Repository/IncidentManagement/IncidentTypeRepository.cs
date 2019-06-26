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
    public class IncidentTypeRepository : IIncidentTypeRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public IncidentTypeRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<IncidentTypeDomain> GetAllIncidentTypes()
        {
            ICollection<IncidentTypeDomain> listToReturn = new List<IncidentTypeDomain>();

            var result = _context.IncidentType.ToList();

            if (result == null) throw new NsiProcessingException(IncidentMessages.UnexpectedError);

            foreach (IncidentType incidentType in _context.IncidentType.ToList())
            {
                listToReturn.Add(incidentType.ToDomainModel());
            }

            return listToReturn;
        }

        public IncidentTypeDomain GetIncidentTypeById(int id)
        {
            var result = _context.IncidentType.FirstOrDefault(x => x.IncidentTypeId == id).ToDomainModel();

            if (result == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentTypeInvalidId);
            }

            return result;
        }

        public int AddIncidentType(IncidentTypeDomain incidentType)
        {
            var incidentTypeDb = new IncidentType().FromDomainModel(incidentType);
            _context.IncidentType.Add(incidentTypeDb);
            _context.SaveChanges();
            return incidentTypeDb.IncidentTypeId;
        }

        public void DeleteIncidentType(int id)
        {
            var incidentTypeDb = _context.IncidentType.FirstOrDefault(x => x.IncidentTypeId == id);

            if (incidentTypeDb == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentStatusInvalidId);
            }

            _context.IncidentType.Remove(incidentTypeDb);
            _context.SaveChanges();
        }

        public void UpdateIncidentType(IncidentTypeDomain incidentType)
        {
            if (incidentType == null) throw new NsiArgumentNullException(ExceptionMessages.ArgumentException);

            var incidentTypeDb = _context.IncidentType.FirstOrDefault(x => x.IncidentTypeId == incidentType.IncidentTypeId);

            if (incidentTypeDb == null) throw new NsiArgumentNullException(IncidentMessages.IncidentTypeInvalidId);

            incidentTypeDb.FromDomainModel(incidentType);
            _context.SaveChanges();
        }
        
    }
}
