using NSI.Common.Exceptions;
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
    public class IncidentSettlementRepository : IIncidentSettlementRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public IncidentSettlementRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<IncidentSettlementDomain> GetAllIncidentSettlements()
        {
            ICollection<IncidentSettlementDomain> listToReturn = new List<IncidentSettlementDomain>();

            var result = _context.IncidentSettlement.ToList();

            if (result == null) throw new NsiProcessingException(IncidentMessages.UnexpectedError);

            foreach (IncidentSettlement incidentSettlement in _context.IncidentSettlement.ToList())
            {
                listToReturn.Add(incidentSettlement.ToDomainModel());
            }

            return listToReturn;
        }

        public IncidentSettlementDomain GetIncidentSettlementById(int id)
        {
            var result = _context.IncidentSettlement.FirstOrDefault(x => x.IncidentSettlementId == id).ToDomainModel();

            if(result == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentSettlementInvalidId);
            }

            return result;
        }

        public void UpdateIncidentSettlement(IncidentSettlementDomain incident)
        {
            var incidentSettlementDb = _context.IncidentSettlement.FirstOrDefault(x => x.IncidentSettlementId == incident.IncidentSettlementId);
            incidentSettlementDb.FromDomainModel(incident);
            _context.SaveChanges();
        }

        public int AddIncidentSettlement(IncidentSettlementDomain incident)
        {
            var incidentSettlementDb = new IncidentSettlement().FromDomainModel(incident);
            _context.IncidentSettlement.Add(incidentSettlementDb);
            _context.SaveChanges();
            return incidentSettlementDb.IncidentSettlementId;
        }

        public void DeleteIncidentSettlement(int id)
        {
            var incidentSettlementDb = _context.IncidentSettlement.FirstOrDefault(x => x.IncidentSettlementId == id);

            if (incidentSettlementDb == null)
            {
                throw new NsiNotFoundException(IncidentMessages.IncidentSettlementInvalidId);
            }

            _context.IncidentSettlement.Remove(incidentSettlementDb);
            _context.SaveChanges();
        }

    }
}
