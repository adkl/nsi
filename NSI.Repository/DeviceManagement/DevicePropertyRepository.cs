using NSI.Common.Exceptions;
using NSI.Common.Resources.DeviceManagement;
using NSI.Domain.DeviceManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.DeviceManagement
{
    public class DevicePropertyRepository : IDevicePropertyRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public DevicePropertyRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all properties for specific tenant id
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public ICollection<PropertyDomain> GetAllProperties(int tenantId)
        {
            if (tenantId <= 0) throw new NsiArgumentException(DeviceMessages.DeviceInvalidArgument);

            ICollection<PropertyDomain> listToReturn = new List<PropertyDomain>();

            var result = _context.Property.Where(x => x.TenantId == tenantId).ToList();

            if (result == null) throw new NsiProcessingException(DeviceMessages.UnexpectedProblem);

            foreach (Property property in result)
            {
                listToReturn.Add(property.ToDomainModel());
            }

            return listToReturn;
        }
    }
}
