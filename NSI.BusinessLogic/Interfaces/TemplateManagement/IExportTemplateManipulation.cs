using NSI.Common.Enumerations;
using NSI.Domain.Document;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.TemplateManagement
{
    public interface IExportTemplateManipulation
    {
        GeneratedDocumentDomain ExportTemplate(int templateVersionId, DocumentTypeEnum outputDocType);
    }
}
