using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.TemplateManagement
{
    public class TemplatePayloadDomain
    {
        
        public string Text { get; set; }
        [Required]
        public ICollection<TemplatePlaceholderDomain> Placeholders { get; set; }
    }
}
