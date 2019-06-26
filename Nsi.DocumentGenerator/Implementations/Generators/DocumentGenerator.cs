using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Generators;
using NSI.DocumentGenerator.Interfaces.Helpers;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Document;
using NSI.Repository.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Common.Resources.Document;

namespace NSI.DocumentGenerator.Implementations.Generators
{
    public class DocumentGenerator : IDocumentGenerator
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IGeneratedDocumentLogger _documentLogger;
        private readonly IHtmlGenerator _htmlGenerator;
        private readonly IPdfGenerator _pdfGenerator;
        private readonly IOdtGenerator _odtGenerator;
        private readonly IDocxGenerator _docxGenerator;
        private readonly ITemplateGenerator _templateGenerator;

        public DocumentGenerator(
            IDocumentTypeRepository documentTypeRepository,
            IGeneratedDocumentLogger documentLogger,
            IHtmlGenerator htmlGenerator,
            IPdfGenerator pdfGenerator,
            IOdtGenerator odtGenerator,
            IDocxGenerator docxGenerator,
            ITemplateGenerator templateGenerator
            )
        {
            _documentTypeRepository = documentTypeRepository;
            _documentLogger = documentLogger;
            _htmlGenerator = htmlGenerator;
            _pdfGenerator = pdfGenerator;
            _odtGenerator = odtGenerator;
            _docxGenerator = docxGenerator;
            _templateGenerator = templateGenerator;
        }

        public GeneratedDocumentDomain Generate(string inputContent, string fileName, 
            DocumentTypeDomain inputDocType, DocumentTypeDomain outputDocTypeDomain, Nullable<int> templateVersionId)
        {
            if (string.IsNullOrEmpty(inputContent))
            {
                throw new NsiArgumentNullException(DocumentMessages.DocumentContentNotFound);
            }
            if (inputDocType == null || outputDocTypeDomain == null)
            {
                throw new NsiArgumentNullException(DocumentMessages.DocumentContentNotFound);
            }
            GeneratedDocumentDomain generatedDoc = new GeneratedDocumentDomain()
            {
                DateCreated = DateTime.Now,
                DocumentType = outputDocTypeDomain,
                DocumentTypeId = outputDocTypeDomain.Id,
                ExternalId = System.Guid.NewGuid(),
                Name = fileName,
                TemplateVersionId = templateVersionId
            };

            if (fileName == null)
            {
                generatedDoc.Name = string.Format(@"{0}", DateTime.Now.Ticks);
            }

            try
            {
                
                generatedDoc.Success = true;
                if (templateVersionId == null)
                {
                    generatedDoc = GenerateRegularDocument(inputDocType, outputDocTypeDomain, inputContent, generatedDoc);                   
                    
                }
                else
                {
                    generatedDoc = GenerateTemplateDocument(inputDocType, outputDocTypeDomain, inputContent, generatedDoc);
                }
                _documentLogger.Log(generatedDoc);
                return generatedDoc;
            }
            catch (Exception e)
            {
                generatedDoc.Success = false;
                generatedDoc.ByteContent = null;
                generatedDoc.Content = e.Message;
                generatedDoc.Name = DocumentMessages.UndefinedDocumentName;
                _documentLogger.Log(generatedDoc);
                throw;
            }
        }

        public GeneratedDocumentDomain Generate(string inputContent, string fileName,
            DocumentTypeEnum inputDocType, DocumentTypeEnum outputDocTypeDomain, Nullable<int> templateVersionId)
        {
            DocumentTypeDomain inputType = _documentTypeRepository.GetByName(inputDocType.ToString("g"));
            DocumentTypeDomain outputType = _documentTypeRepository.GetByName(outputDocTypeDomain.ToString("g"));
            return Generate(inputContent, fileName, inputType, outputType, templateVersionId);
        }

        /// <summary>
        /// For the document generation to work there has to be predefined 'document types' in database.
        /// If there aren't any this method creates types: pdf, html, odt, json, docx
        /// </summary>
        public void AddBasicDocTypesToDb()
        {
            List<string> list = new List<string> { "pdf", "html", "json", "odt", "docx" };
            foreach (string item in list)
            {
                DocumentTypeDomain docType = _documentTypeRepository.GetByName(item);
                if (docType == null)
                    _documentTypeRepository.Add(new DocumentTypeDomain()
                    {
                        Name = item,
                        Code = item,
                        Version = "1.0",
                        Encoding = "utf-8"
                    });
            }
        }

        public ICollection<DocumentTypeDomain> GetAllTypes()
        {
            return _documentTypeRepository.GetAll();
        }
        
        // Helper method for readability
        // Returns true if input == expectedInput AND output == expectedOutput
        private bool CompareDocTypes(DocumentTypeDomain inputDocType, DocumentTypeDomain outputDocTypeDomain,
            DocumentTypeEnum expectedInput, DocumentTypeEnum expectedOutput)
        {
            // "g" means that the conversion of enum to string will be the most intuitive one
            // For example, if the enum value is MyValue then ToString("g") will return "MyValue"
            return string.Equals(inputDocType.Name, expectedInput.ToString("g"), StringComparison.CurrentCultureIgnoreCase)
                    && string.Equals(outputDocTypeDomain.Name, expectedOutput.ToString("g"), StringComparison.CurrentCultureIgnoreCase);
        }
        private GeneratedDocumentDomain GenerateRegularDocument(DocumentTypeDomain inputDocType, DocumentTypeDomain outputDocTypeDomain,
            string inputContent, GeneratedDocumentDomain generatedDoc)
        {
            if (CompareDocTypes(inputDocType, outputDocTypeDomain, DocumentTypeEnum.Html, DocumentTypeEnum.Pdf))
                generatedDoc.ByteContent = _pdfGenerator.GeneratePdfFromHtml(inputContent);
            else if (CompareDocTypes(inputDocType, outputDocTypeDomain, DocumentTypeEnum.Json, DocumentTypeEnum.Pdf))
                generatedDoc.ByteContent = _pdfGenerator.GeneratePdfFromJson(inputContent);
            else if (CompareDocTypes(inputDocType, outputDocTypeDomain, DocumentTypeEnum.Html, DocumentTypeEnum.Odt))
                generatedDoc.ByteContent = _odtGenerator.GenerateOdtFromHtml(inputContent);
            else if (CompareDocTypes(inputDocType, outputDocTypeDomain, DocumentTypeEnum.Html, DocumentTypeEnum.Docx))
                generatedDoc.ByteContent = _docxGenerator.GenerateDocxFromHtml(inputContent);
            else if (CompareDocTypes(inputDocType, outputDocTypeDomain, DocumentTypeEnum.Json, DocumentTypeEnum.Html))
                generatedDoc.Content = _htmlGenerator.GenerateHtmlFromJson(inputContent, generatedDoc.Name);
            else
            {
                generatedDoc.Success = false;
                generatedDoc.Content = DocumentMessages.ArgumentInvalidDocType;
                generatedDoc.Name = DocumentMessages.UndefinedDocumentName;
            }
            return generatedDoc;
        }

        private GeneratedDocumentDomain GenerateTemplateDocument(DocumentTypeDomain inputDocType, DocumentTypeDomain outputDocTypeDomain,
            string inputContent, GeneratedDocumentDomain generatedDoc)
        {
            if (CompareDocTypes(inputDocType, outputDocTypeDomain, DocumentTypeEnum.Json, DocumentTypeEnum.Pdf))
                generatedDoc.ByteContent = _templateGenerator.GeneratePdfFromTemplate(inputContent, generatedDoc.Name);
            else if (CompareDocTypes(inputDocType, outputDocTypeDomain, DocumentTypeEnum.Json, DocumentTypeEnum.Html))
                generatedDoc.Content = _templateGenerator.GenerateHtmlFromTemplate(inputContent, generatedDoc.Name);
            else
            {
                generatedDoc.Success = false;
                generatedDoc.Content = DocumentMessages.ArgumentInvalidDocType;
                generatedDoc.Name = DocumentMessages.UndefinedDocumentName;
            }
            return generatedDoc;
        }
        
    }
}
