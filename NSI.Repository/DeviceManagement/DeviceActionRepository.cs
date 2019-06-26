using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.EF;
using NSI.Repository.Interfaces.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Repository.Extensions;

namespace NSI.Repository.DeviceManagement
{
    public class DeviceActionRepository : IDeviceActionRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public DeviceActionRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all actions for specific tenant id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ICollection<ActionDomain> GetAllActions(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            ICollection<ActionDomain> listToReturn = new List<ActionDomain>();

            var result = _context.Action.Where(x => x.TenantId == tenantId).ToList();

            if (result == null) throw new NsiProcessingException(DeviceMessages.UnexpectedProblem);

            foreach (EF.Action action in result)
            {
                listToReturn.Add(action.ToDomainModel());
            }

            return listToReturn;
        }
    }
}
