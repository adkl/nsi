using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.TemplateManagement
{
    public class DeleteTemplateRequest: BaseRequest
    {
        /// <summary>
        /// Template Id for deletion
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Template Version Id for deletion
        /// </summary>
        public int templateVersionId { get; set; }

    }
}
