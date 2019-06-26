using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Interfaces.Helpers
{
    public interface IHtmlGeneratorHelper
    {
        StringBuilder GenerateOpeningTags(string name);
        StringBuilder GenerateClosingTags(StringBuilder strHTMLBuilder);
    }
}
