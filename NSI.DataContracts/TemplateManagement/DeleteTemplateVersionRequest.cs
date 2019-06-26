using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.TemplateManagement
{
    public class DeleteTemplateVersionRequest: BaseRequest
    {
        /// <summary>
        /// Template Version Id for deletion
        /// </summary>
        public int Id { get; set; }

    }
}
