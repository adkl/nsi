using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Interfaces.Generators
{
    public interface ITemplateGenerator
    {
        byte[] GeneratePdfFromTemplate(string content, string name);
        string GenerateHtmlFromTemplate(string content, string name);
    }
}
