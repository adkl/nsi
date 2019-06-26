using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.TemplateManagement
{
    public class TemplateContentDomain
    {
        [Required]
        public TemplateType Type { get; set; }
        [Required]
        public TemplatePayloadDomain Payload { get; set; }
    }
}
