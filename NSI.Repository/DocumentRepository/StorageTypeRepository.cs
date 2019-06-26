using NSI.Domain.DocumentManagement;
using NSI.EF;
using NSI.Repository.Interfaces.DocumentManagement;
using System.Collections.Generic;
using System.Linq;
using NSI.Repository.Extensions;
using NSI.Common.Exceptions;
using NSI.Common.Resources.DocumentManagement;

namespace NSI.Repository
{
    public class StorageTypeRepository : IStorageTypeRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public StorageTypeRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<StorageTypeDomain> GetAllStorageTypes()
        {
            ICollection<StorageTypeDomain> listToReturn = new List<StorageTypeDomain>();

            var result = _context.StorageType.ToList();

            if (result == null) throw new NsiProcessingException(StorageTypeMessages.UnexpectedProblem);

            foreach (StorageType StorageType in result)
            {
                listToReturn.Add(StorageType.ToDomainModel());
            }

            return listToReturn;
        }

        public StorageTypeDomain GetStorageTypeById(int id)
        {
            var result = _context.StorageType.FirstOrDefault(x => x.StorageTypeId == id).ToDomainModel();

            if (result == null) throw new NsiNotFoundException(StorageTypeMessages.StorageTypeNotFound);

            return result;
        }

        public int CreateStorageType(StorageTypeDomain storageType)
        {

            if (storageType == null) throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);

            var storageTypeDb = new StorageType().FromDomainModel(storageType);

            _context.StorageType.Add(storageTypeDb);
            _context.SaveChanges();

            return storageTypeDb.StorageTypeId;
        }

        public int UpdateStorageType(StorageTypeDomain storageType)
        {
            if (storageType == null) throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);

            if (!_context.StorageType.Any(x => x.StorageTypeId == storageType.StorageTypeId))
            {
                throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidId);
            }

            var storageTypeDb = _context.StorageType.Where(x => x.StorageTypeId == storageType.StorageTypeId).FirstOrDefault().FromDomainModel(storageType);

            if (storageTypeDb == null) throw new NsiNotFoundException(StorageTypeMessages.StorageTypeNotFound);

            _context.SaveChanges();

            return storageTypeDb.StorageTypeId;
        }

        public bool DeleteStorageType(int storageTypeId)
        {
            if (storageTypeId <= 0) throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);

            var storageTypeDb = _context.StorageType.Where(x => x.StorageTypeId == storageTypeId).FirstOrDefault();

            if (storageTypeDb == null)
            {
                throw new NsiNotFoundException(StorageTypeMessages.StorageTypeInvalidId);
            }

            _context.StorageType.Remove(storageTypeDb);
            _context.SaveChanges();

            return true;
        }
    }
}