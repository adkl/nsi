using System;

namespace NSI.Domain.DocumentManagement
{
    public class DocumentDomain
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int FileSize { get; set; }
        public Guid ExternalId { get; set; }
        public string LocationExternalId { get; set; }
        public DateTime DateCreated { get; set; }
        public int StorageTypeId { get; set; }
        public int FileTypeId { get; set; }
        public string Description { get; set; }

        public string toString()
        {
            return "Document: " + this.Name + "\nFileTypeId: " + this.FileTypeId;
        }
    }
}
