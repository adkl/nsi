using System.Collections.Generic;

namespace NSI.Domain.DocumentManagement
{
    public class StorageTypeDomain
    {
        public int StorageTypeId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<DocumentDomain> Documents { get; set; }
    }
}
