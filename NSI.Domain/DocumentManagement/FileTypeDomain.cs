using System.Collections.Generic;

namespace NSI.Domain.DocumentManagement
{
    public class FileTypeDomain
    {
        public int FileTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Extension { get; set; }
        public List<DocumentDomain> Documents { get; set; }
    }
}
