using Newtonsoft.Json.Linq;
using NSI.Domain.TemplateManagement;

namespace NSI.DocumentGenerator.Interfaces
{
    public interface IPdfGenerator
    {        
        byte[] GeneratePdfFromHtml(string content);
        byte[] GeneratePdfFromJson(string content);
        byte[] GenerateTextTemplatePdf(TemplatePayloadDomain payload, string name);
        byte[] GenerateTableTemplatePdf(TemplatePayloadDomain payload, string name);
    }
}
