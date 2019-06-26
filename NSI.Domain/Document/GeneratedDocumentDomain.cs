using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.Document
{
    public class GeneratedDocumentDomain : BaseDomain
    {
        private byte[] _byteContent;
        public System.Guid ExternalId { get; set; }
        public string Name { get; set; }
        public bool Success { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string Content { get; set; }
        public Nullable<int> TemplateVersionId { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentTypeDomain DocumentType { get; set; }
        public byte[] ByteContent
        {
            get
            {
                if (_byteContent == null && Content != null)
                    return Encoding.UTF8.GetBytes(Content);
                else
                    return _byteContent;
            }
            set
            {
                if (value != null)
                {
                    _byteContent = value;
                    Content = Encoding.UTF8.GetString(value);
                }
            }
        }
    }
}
