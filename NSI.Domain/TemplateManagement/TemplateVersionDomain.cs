using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.TemplateManagement
{
    public class TemplateVersionDomain : BaseDomain
    {
        public DateTime DateCreated { get; set; }
        public bool IsDefault { get; set; }
        public int TemplateId { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }
    }
}
