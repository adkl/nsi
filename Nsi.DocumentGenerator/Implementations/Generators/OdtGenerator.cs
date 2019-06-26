using IronPdf;
using Microsoft.Office.Interop.Word;
using NSI.Common.Exceptions;
using NSI.Common.Resources.Document;
using NSI.DocumentGenerator.Interfaces;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NSI.DocumentGenerator.Implementations.Generators;
using System.Data;
using NSI.DocumentGenerator.Implementations.Helpers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics.CodeAnalysis;

namespace NSI.DocumentGenerator.Implementations
{
    public class OdtGenerator : IOdtGenerator
    {
        private readonly FileGenerator _fileHelper;
        private readonly string workingDir;

        public OdtGenerator()
        {
            _fileHelper = new FileGenerator();
            workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
        }

        [ExcludeFromCodeCoverage]
        public byte[] Generate(List<string> list, string fileName, string content)
        {
            // Save content to html file
            System.IO.File.WriteAllText(Path.Combine(workingDir, fileName + ".html"), content);

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDoc = new Microsoft.Office.Interop.Word.Document();
            Object oMissing = System.Reflection.Missing.Value;
            wordDoc = word.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            word.Visible = false;
            Object readOnly = false;

            Object htmlFilePath = Path.Combine(workingDir, fileName + ".html");
            Object odtFilePath = Path.Combine(workingDir, fileName + ".odt");

            wordDoc = word.Documents.Open(ref htmlFilePath, ref oMissing,
                ref readOnly);
            object finalFileFormat = WdSaveFormat.wdFormatOpenDocumentText;
            wordDoc.SaveAs(ref odtFilePath, ref finalFileFormat, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing);
            foreach (Table table in wordDoc.Tables)
            {
                table.Borders.OutsideColor = WdColor.wdColorBlack;
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
            }
            wordDoc.Save();
            wordDoc.Close();

            // Read file bytes 
            byte[] data = File.ReadAllBytes(Path.Combine(workingDir, fileName + ".odt"));

            // Remove previously created files
            _fileHelper.RemoveLocalFiles(fileName, list);

            return data;
        }

        byte[] IOdtGenerator.GenerateOdtFromHtml(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new NsiArgumentNullException(DocumentMessages.DocumentContentNotFound);
                }

                // Possible extensions
                List<string> list = new List<string> { ".html", ".odt" };
                // File path to save files
                string fileName = _fileHelper.GenerateRandomFileName();
                // Remove files with the same name
                _fileHelper.RemoveLocalFiles(fileName, list);

                return Generate(list, fileName, content);
            }
            catch (Exception e)
            {
                throw new NsiBaseException(DocumentMessages.OdtGeneratorFailed, e, Common.Enumerations.SeverityEnum.Error);
            }
        }
    }
}
