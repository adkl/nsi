using NSI.Common.Exceptions;
using NSI.Common.Resources.TemplateManagement;
using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.TemplateManagement
{
    public class CreateFolderRequest : BaseRequest
    {
        [Required]
        public string Name { get; set; }
        public int ParentFolderId { get; set; }
    }

    public static class CreateFolderRequestExtension
    {
        public static void ValidateContent(this CreateFolderRequest request)
        {

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new NsiArgumentNullException(TemplateManagementMessages.InvalidFolderName,
                    Common.Enumerations.SeverityEnum.Warning);
            }

        }
    }

}
