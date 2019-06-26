using NSI.Common.Exceptions;
using NSI.Common.Resources.Document;
using NSI.DocumentGenerator.Implementations.Generators;
using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Helpers;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NSI.DocumentGenerator.Implementations
{
    public class HtmlGenerator : IHtmlGenerator
    {
        const string REGEX_PLACEHOLDER_DEFAULT = "(#[0-9]+#)";
        private readonly IHtmlGeneratorHelper _htmlGeneratorHelper;

        public HtmlGenerator(IHtmlGeneratorHelper htmlGeneratorHelper)
        {
            _htmlGeneratorHelper = htmlGeneratorHelper;
        }
         
        string IHtmlGenerator.GenerateHtmlFromJson(string content, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new NsiArgumentNullException(DocumentMessages.DocumentContentNotFound);
                }
                DataTable dt = DataTableGenerator.GenerateDataTableFromJson(content);
                StringBuilder strHTMLBuilder = _htmlGeneratorHelper.GenerateOpeningTags(name);
                strHTMLBuilder.Append("<table border='1px' cellpadding='1' cellspacing='1' bgcolor='white'>");

                strHTMLBuilder.Append("<tr >");
                foreach (DataColumn myColumn in dt.Columns)
                {
                    strHTMLBuilder.Append("<td >");
                    strHTMLBuilder.Append(myColumn.ColumnName);
                    strHTMLBuilder.Append("</td>");

                }
                strHTMLBuilder.Append("</tr>");


                foreach (DataRow myRow in dt.Rows)
                {

                    strHTMLBuilder.Append("<tr >");
                    foreach (DataColumn myColumn in dt.Columns)
                    {
                        strHTMLBuilder.Append("<td >");
                        strHTMLBuilder.Append(myRow[myColumn.ColumnName].ToString());
                        strHTMLBuilder.Append("</td>");

                    }
                    strHTMLBuilder.Append("</tr>");
                }

                //Close tags.  
                strHTMLBuilder.Append("</table>");
                strHTMLBuilder = _htmlGeneratorHelper.GenerateClosingTags(strHTMLBuilder);

                string Htmltext = strHTMLBuilder.ToString();

                return Htmltext;
            }
            catch (Exception e)
            {
                throw new NsiBaseException(DocumentMessages.HtmlGeneratorFailed, e, Common.Enumerations.SeverityEnum.Error);
            }
        }
        public string GenerateTableTemplateHtml(TemplatePayloadDomain payload, string name)
        {
            ICollection<TemplatePlaceholderDomain> placeholders = payload.Placeholders;
            StringBuilder strHTMLBuilder = _htmlGeneratorHelper.GenerateOpeningTags(name);

            strHTMLBuilder.Append("<table border='1px' cellpadding='1' cellspacing='1' bgcolor='white'>");
            
            foreach (TemplatePlaceholderDomain templatePlaceholderDomain in placeholders)
            {

                strHTMLBuilder.Append("<tr >");                
                strHTMLBuilder.Append("<td >");
                strHTMLBuilder.Append(templatePlaceholderDomain.Description);
                strHTMLBuilder.Append("</td>");
                strHTMLBuilder.Append("<td >");
                strHTMLBuilder.Append("<input type=\"" + templatePlaceholderDomain.Type.ToString("g") + "\" name=\"" + 
                    templatePlaceholderDomain.Id.ToString() + "\" value=\"" + templatePlaceholderDomain.Description + "\">");
                strHTMLBuilder.Append("</td>");
                strHTMLBuilder.Append("</tr>");
            }

            //Close tags.  
            strHTMLBuilder.Append("</table>");
            strHTMLBuilder = _htmlGeneratorHelper.GenerateClosingTags(strHTMLBuilder);
            string Htmltext = strHTMLBuilder.ToString();

            return Htmltext;
        }

        public string GenerateTextTemplateHtml(TemplatePayloadDomain payload, string name)
        {
            string text = payload.Text;
            ICollection<TemplatePlaceholderDomain> placeholders = payload.Placeholders;

            StringBuilder strHTMLBuilder = new StringBuilder();
            strHTMLBuilder.Append("<html>");
            strHTMLBuilder.Append("<title>");
            strHTMLBuilder.Append(name);
            strHTMLBuilder.Append("</title>");
            strHTMLBuilder.Append("<body>");
            strHTMLBuilder.Append("<h1>");
            strHTMLBuilder.Append(name);
            strHTMLBuilder.Append("</h1>");
            strHTMLBuilder.Append("<form>");
            
            string[] lines = Regex.Split(text, REGEX_PLACEHOLDER_DEFAULT);
            foreach (var line in lines)
            {
                if (Regex.IsMatch(line,REGEX_PLACEHOLDER_DEFAULT))
                {
                    TemplatePlaceholderDomain placeholderDomain = placeholders.First(obj =>
                    {
                        return obj.Id.ToString() == line.Trim('#');
                    });
                    strHTMLBuilder.Append("<input type=\""+ placeholderDomain.Type.ToString("g") + "\" name=\""+placeholderDomain.Id.ToString()+"\" value=\""+
                        placeholderDomain.Description+"\">");
                }
                else if (line.Contains("\n"))
                {
                    strHTMLBuilder.Append(line);
                    strHTMLBuilder.Append("<br>");
                }
                else
                {
                    strHTMLBuilder.Append(line);
                }
            }
            strHTMLBuilder.Append("</form>");
            strHTMLBuilder = _htmlGeneratorHelper.GenerateClosingTags(strHTMLBuilder);
            string Htmltext = strHTMLBuilder.ToString();

            return Htmltext;
        }
    }
}
