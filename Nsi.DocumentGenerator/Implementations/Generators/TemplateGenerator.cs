using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSI.Common.Exceptions;
using NSI.Common.Resources.Document;
using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Implementations.Generators
{
    public class TemplateGenerator : ITemplateGenerator
    {
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IHtmlGenerator _htmlGenerator;
        public TemplateGenerator(IPdfGenerator pdfGenerator, IHtmlGenerator htmlGenerator)
        {
            _pdfGenerator = pdfGenerator;
            _htmlGenerator = htmlGenerator;
        }

        
        public string GenerateHtmlFromTemplate(string content, string name)
        {

            try
            {
                TemplateContentDomain templateContentDomain = JObject.Parse(content).ToObject<TemplateContentDomain>();
                if (templateContentDomain.Type == Common.Enumerations.TemplateType.Text)
                {
                    return _htmlGenerator.GenerateTextTemplateHtml(templateContentDomain.Payload, name);
                }
                else
                {
                    return _htmlGenerator.GenerateTableTemplateHtml(templateContentDomain.Payload, name);
                }

            }
            catch (Exception e)
            {
                throw new NsiBaseException(DocumentMessages.HtmlGeneratorFailed, e, Common.Enumerations.SeverityEnum.Error);
            }
        }

        public byte[] GeneratePdfFromTemplate(string content, string name)
        {
            try
            {
                TemplateContentDomain templateContentDomain = JObject.Parse(content).ToObject<TemplateContentDomain>();
                if (templateContentDomain.Type==Common.Enumerations.TemplateType.Text)
                {
                    return _pdfGenerator.GenerateTextTemplatePdf(templateContentDomain.Payload, name);
                }
                else
                {
                    return _pdfGenerator.GenerateTableTemplatePdf(templateContentDomain.Payload, name);
                } 

            }
            catch (Exception e)
            {
                throw new NsiBaseException(DocumentMessages.PDFGeneratorFailed, e, Common.Enumerations.SeverityEnum.Error);
            }
           
        }
    }
}
