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
    public class CreateTemplateRequest : BaseRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int FolderId { get; set; }
        [Required]
        public TemplateContentDomain Content { get; set; }
    }

    public static class CreateTemplateRequestExtension
    {
        public static void ValidateContent(this CreateTemplateRequest request)
        {
            TemplateContentDomain content = request.Content;

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new NsiArgumentNullException(TemplateManagementMessages.InvalidTemplateName,
                    Common.Enumerations.SeverityEnum.Warning);
            }
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
