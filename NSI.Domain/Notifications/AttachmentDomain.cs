using NSI.Domain.Base;
using System.Collections.Generic;

namespace NSI.Domain.Notifications
{
    public class AttachmentDomain : BaseDomain
    {
        public byte[] File { get; set; }
        public System.DateTime DateCreated { get; set; }
        
    }
}
