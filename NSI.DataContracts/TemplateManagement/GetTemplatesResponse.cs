using NSI.Common.Models;
using NSI.DataContracts.Base;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.TemplateManagement
{
    public class GetTemplatesResponse : BaseResponse<ICollection<TemplateDomain>>
    {
        public Paging Paging { get; set; }
    }
}
