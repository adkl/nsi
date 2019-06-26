
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Common.Resources.DocumentManagement;
using NSI.DocumentRepository.Interfaces;
using NSI.Domain.DocumentManagement;
using NSI.Repository.Interfaces.DocumentManagement;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NSI.DocumentRepository.Implementations
{
    public class DocumentManipulation : IDocumentManipulation
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentManipulation(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public ICollection<DocumentDomain> GetAllDocuments()
        { 
            return _documentRepository.GetAllDocuments();
        }

        public ICollection<DocumentDomain> GetAllDocumentsWithStorageTypeId(int id)
        {
            if (id <= 0)
                throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidArgument);
        
            return _documentRepository.GetAllDocumentsWithStorageTypeId(id);
        }


        public DocumentDomain GetDocumentById(int id, int storageTypeId)
        {
            if (id <= 0) throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);

            return _documentRepository.GetDocumentById(id, storageTypeId);
        }

        public IEnumerable<DocumentDomain> Search(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());

            return _documentRepository.Search(paging, filterCriteria, sortCriteria);
        }

        public int CreateDocument(DocumentDomain document)
        {
            if (document == null) throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);

            return _documentRepository.CreateDocument(document);
        }

        public int UpdateDocument(DocumentDomain document)
        {
            if (document == null) throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);

            return _documentRepository.UpdateDocument(document);
        }

        public bool DeleteDocument(int documentId)
        {
            if (documentId <= 0) throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);

            return _documentRepository.DeleteDocument(documentId);
        }

        public static bool IsSafe(string path)
        {
            if (ConfigurationManager.AppSettings["antimalware:Enabled"] == "false")
                return true;

            string antimalwarePath = ConfigurationManager.AppSettings["antimalware:Path"];

            if (antimalwarePath == null || !File.Exists(antimalwarePath))
                antimalwarePath = ConfigurationManager.AppSettings["antimalware:PathAlternative"];

            if (antimalwarePath == null || !File.Exists(antimalwarePath))
                throw new NsiBaseException("Antimalware software not found");

            Process antivirusProcess = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = antimalwarePath,
                Arguments = "-Scan -File " + path
            };

            antivirusProcess.StartInfo = processStartInfo;
            antivirusProcess.Start();
            antivirusProcess.WaitForExit();

            // Exit code 2 means there was a problem (malware found or file could not be opened)
            // MpCmdRun.exe returns 0 otherwise

            return antivirusProcess.ExitCode == 0;
        }
    }
}
