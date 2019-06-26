using IronPdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;
using NSI.Common.Exceptions;
using NSI.Common.Resources.Document;
using NSI.DocumentGenerator.Implementations.Generators;
using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NSI.DocumentGenerator.Implementations
{
    public class PdfGenerator : IPdfGenerator
    {
        const string REGEX_PLACEHOLDER_DEFAULT = "(#[0-9]+#)";     
      
        byte[] IPdfGenerator.GeneratePdfFromHtml(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new NsiArgumentNullException(DocumentMessages.DocumentContentNotFound);
                }
                HtmlToPdf htmlToPdf = new IronPdf.HtmlToPdf();
                byte[] data = htmlToPdf.RenderHtmlAsPdf(content).BinaryData;
                return data;
            }
            catch (Exception e)
            {
                throw new NsiBaseException(DocumentMessages.PDFGeneratorFailed, e, Common.Enumerations.SeverityEnum.Error);
            }
        }

        byte[] IPdfGenerator.GeneratePdfFromJson(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new NsiArgumentNullException(DocumentMessages.DocumentContentNotFound);
                }
                DataTable dt = DataTableGenerator.GenerateDataTableFromJson(content);
                byte[] output;
                MemoryStream stream = new MemoryStream();
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();
                Font font5 = FontFactory.GetFont(FontFactory.HELVETICA, 5);
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                List<float> widths = new List<float>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    widths.Add(4f);
                }
                table.SetWidths(widths.ToArray());
                table.WidthPercentage = 100;
                PdfPCell cell = new PdfPCell(new Phrase())
                {
                    Colspan = dt.Columns.Count
                };
                foreach (DataColumn c in dt.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font5));
                }
                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            table.AddCell(new Phrase(r[i].ToString(), font5));
                        }
                    }
                }
                document.Add(table);
                document.Close();
                output = stream.ToArray();
                return output;
            }
            catch (Exception e)
            {
                throw new NsiBaseException(DocumentMessages.PDFGeneratorFailed, e, Common.Enumerations.SeverityEnum.Error);
            }
        }

        public byte[] GenerateTextTemplatePdf(TemplatePayloadDomain payload, string name)
        {
            string text = payload.Text;
            ICollection<TemplatePlaceholderDomain> placeholders = payload.Placeholders;                    

            byte[] output;
            MemoryStream stream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();
            Font font10 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 30);
            var paragraph1 = new Paragraph(name,font10);
            paragraph1.Alignment = Element.ALIGN_CENTER;
            Font font5 = FontFactory.GetFont(FontFactory.HELVETICA, 12);           
            var paragraph2 = new Paragraph("",font5);
            paragraph2.SpacingBefore = 50f;
            string[] lines = Regex.Split(text, REGEX_PLACEHOLDER_DEFAULT);
            foreach (var line in lines)
            {
                if (Regex.IsMatch(line, REGEX_PLACEHOLDER_DEFAULT))
                {
                    TemplatePlaceholderDomain placeholderDomain = placeholders.First(obj =>
                    {
                        return obj.Id.ToString() == line.Trim('#');
                    });
                    string place = new string('_', placeholderDomain.Length);
                    paragraph2.Add(place);
                    paragraph2.Add("(");
                    paragraph2.Add(placeholderDomain.Description);
                    paragraph2.Add(")");
                }
                else
                {
                    paragraph2.Add(line);
                }
            }
            document.Add(paragraph1);
            document.Add(paragraph2);
            document.Close();
            output = stream.ToArray();
            return output;        
            
        }

        public byte[] GenerateTableTemplatePdf(TemplatePayloadDomain payload, string name)
        {
            ICollection<TemplatePlaceholderDomain> placeholders = payload.Placeholders;
            byte[] output;
            MemoryStream stream = new MemoryStream();
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();
            Font font10 = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 30);
            var paragraph1 = new Paragraph(name, font10);
            paragraph1.Alignment = Element.ALIGN_CENTER;
            PdfPTable table = new PdfPTable(2);  
            table.SpacingBefore = 50f;
            foreach (TemplatePlaceholderDomain templatePlaceholderDomain in placeholders)
            {
                table.AddCell(templatePlaceholderDomain.Description);
                table.AddCell(table.DefaultCell);
            }                      
            document.Add(paragraph1);
            document.Add(table);
            document.Close();
            output = stream.ToArray();
            return output;
        }

    }
}
