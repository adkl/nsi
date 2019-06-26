using NSI.DocumentGenerator.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Implementations.Helpers
{
    public class HtmlGeneratorHelper : IHtmlGeneratorHelper
    {
        const string OPEN_HTML_TAG = "<html>";
        const string CLOSE_HTML_TAG = "</html>";
        const string OPEN_TITLE_TAG = "<title>";
        const string CLOSE_TITLE_TAG = "</title>";
        const string OPEN_BODY_TAG = "<body>";
        const string CLOSE_BODY_TAG = "</body>";
        const string OPEN_H1_TAG = "<h1>";
        const string CLOSE_H1_TAG = "</h1>";

        public StringBuilder GenerateClosingTags(StringBuilder strHTMLBuilder)
        {
            strHTMLBuilder.Append(CLOSE_BODY_TAG);
            strHTMLBuilder.Append(CLOSE_HTML_TAG);
            return strHTMLBuilder;
        }

        public StringBuilder GenerateOpeningTags(string name)
        {
            StringBuilder strHTMLBuilder = new StringBuilder();
            strHTMLBuilder.Append(OPEN_HTML_TAG);
            strHTMLBuilder.Append(OPEN_TITLE_TAG);
            strHTMLBuilder.Append(name);
            strHTMLBuilder.Append(CLOSE_TITLE_TAG);
            strHTMLBuilder.Append(OPEN_BODY_TAG);
            strHTMLBuilder.Append(OPEN_H1_TAG);
            strHTMLBuilder.Append(name);
            strHTMLBuilder.Append(CLOSE_H1_TAG);
            return strHTMLBuilder;
        }
    }
}
