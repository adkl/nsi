using NSI.BusinessLogic.Interfaces.TemplateManagement;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Resources.TemplateManagement;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.Domain.Document;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.TemplateManagement
{
    public class ExportTemplateManipulation : IExportTemplateManipulation
    {
        private readonly ITemplateManipulation _templateManipulation;
        private readonly ITemplateVersionManipulation _templateVersionManipulation;
        private readonly IDocumentGenerator _documentGenerator;

        public ExportTemplateManipulation(ITemplateManipulation templateManipulation, ITemplateVersionManipulation templateVersionManipulation, IDocumentGenerator documentGenerator)
        {
            _templateManipulation = templateManipulation;
            _templateVersionManipulation = templateVersionManipulation;
            _documentGenerator = documentGenerator;
        }        

        public GeneratedDocumentDomain ExportTemplate( int templateVersionId, DocumentTypeEnum outputDocType)
        {
            TemplateVersionDomain templateVersionDomain = _templateVersionManipulation.GetByVersionId(templateVersionId);
            if (templateVersionDomain == null)
            {
                throw new NsiArgumentNullException(TemplateManagementMessages.TemplateVersionInvalidId);
            }
            string name = _templateManipulation.GetTemplateNameById(templateVersionDomain.TemplateId);
            GeneratedDocumentDomain generatedDocumentDomain = _documentGenerator.Generate(templateVersionDomain.Content, name,
                DocumentTypeEnum.Json, outputDocType, templateVersionId);
            return generatedDocumentDomain;
        }
    }
}
