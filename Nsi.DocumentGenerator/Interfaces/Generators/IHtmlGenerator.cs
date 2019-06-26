using NSI.Domain.TemplateManagement;

namespace NSI.DocumentGenerator.Interfaces
{
    public interface IHtmlGenerator
    {
        // to do (missing parameters and return value)
        string GenerateHtmlFromJson(string content, string name);
        string GenerateTextTemplateHtml(TemplatePayloadDomain payload, string name);
        string GenerateTableTemplateHtml(TemplatePayloadDomain payload, string name);
    }
}
