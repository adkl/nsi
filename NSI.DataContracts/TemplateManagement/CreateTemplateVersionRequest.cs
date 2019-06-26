using NSI.Common.Exceptions;
using NSI.Common.Resources.TemplateManagement;
using NSI.DataContracts.Base;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.TemplateManagement
{
    public class CreateTemplateVersionRequest : BaseRequest
    {
       
        [Required]
        public int TemplateId { get; set; }
        [Required]
        public TemplateContentDomain Content { get; set; }
    }

    public static class CreateTemplateVersionRequestExtension
    {
        public static void ValidateContent(this CreateTemplateVersionRequest request)
        {
            TemplateContentDomain content = request.Content;
            
            if (content.Type != Common.Enumerations.TemplateType.Table &&
                content.Type != Common.Enumerations.TemplateType.Text)
            {
                throw new NsiArgumentException(TemplateManagementMessages.InvalidTemplateType,
                    Common.Enumerations.SeverityEnum.Warning);
            }
            if (content.Payload.Text == null && content.Type == Common.Enumerations.TemplateType.Text)
            {
                throw new NsiArgumentException(TemplateManagementMessages.InvalidTemplatePayload,
                   Common.Enumerations.SeverityEnum.Warning);
            }
        }
    }
}
