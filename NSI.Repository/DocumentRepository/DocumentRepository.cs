using NSI.Common.Exceptions;
using NSI.Common.Extensions;
using NSI.Common.Models;
using NSI.Common.Resources.DocumentManagement;
using NSI.Domain.DocumentManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.DocumentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NSI.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public DocumentRepository(NsiContext context)
        {
            _context = context;
        }

        public ICollection<DocumentDomain> GetAllDocuments()
        {
            ICollection<DocumentDomain> listToReturn = new List<DocumentDomain>();

            var result = _context.Document.ToList();

            if (result == null) throw new NsiProcessingException(DocumentMessages.UnexpectedProblem);

            foreach (Document Document in result)
            {
                listToReturn.Add(Document.ToDomainModel());
            }

            return listToReturn;
        }

        public DocumentDomain GetDocumentById(int id, int storageTypeId)
        {
            var result = _context.Document.FirstOrDefault(x => x.DocumentId == id && x.StorageTypeId == storageTypeId).ToDomainModel();

            if (result == null) throw new NsiNotFoundException(DocumentMessages.DocumentNotFound);

            return result;
        }

        public ICollection<DocumentDomain> GetAllDocumentsWithStorageTypeId(int id)
        {
            ICollection<DocumentDomain> listToReturn = new List<DocumentDomain>();

            var result = _context.Document.ToList();

            if (result == null) throw new NsiProcessingException(DocumentMessages.UnexpectedProblem);

            foreach (Document Document in result)
            {
                if (Document.StorageTypeId == id)
                {
                    listToReturn.Add(Document.ToDomainModel());
                }
            }

            return listToReturn;
        }

        public ICollection<DocumentDomain> Search(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.Document
               .DoFiltering(filterCriteria, FilterDocuments)
               .DoSorting(sortCriteria, SortDocuments)
               .DoPaging(paging)
               .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        private Expression<Func<Document, bool>> FilterDocuments(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Document, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "fileType":
                    fnc = x => (x.FileTypeId).Equals(filterTerm);
                    break;
                case "storageType":
                    fnc = x => (x.StorageTypeId).Equals(filterTerm);
                    break;
                case "name":
                    fnc = x => (x.Name).Equals(filterTerm);
                    break;
                case "fileSize":
                    fnc = x => (x.FileSize).CompareTo(filterTerm) >= 0;
                    break;
                case "dateCreated":
                    fnc = x => (x.DateCreated).CompareTo(filterTerm) <= 0;
                    break;
                default:
                    break;
            }
            return fnc;
        }

        private Expression<Func<Document, object>> SortDocuments(string columnName)
        {
            Expression<Func<Document, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "id":
                    fnc = x => x.DocumentId;
                    break;
                case "name":
                    fnc = x => x.Name;
                    break;
                case "fileSize":
                    fnc = x => x.FileSize;
                    break;
                case "dateCreated":
                    fnc = x => x.DateCreated;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        public int CreateDocument(DocumentDomain document)
        {
            if (document == null ) throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);
        
            if (!_context.FileType.Any(x => x.FileTypeId == document.FileTypeId))
            {
                throw new NsiArgumentException(document.toString());
            }

            if (!_context.StorageType.Any(x => x.StorageTypeId == document.StorageTypeId))
            {
                throw new NsiArgumentException(StorageTypeMessages.StorageTypeInvalidId);
            }

            var documentDb = new Document().FromDomainModel(document);

            _context.Document.Add(documentDb);
            _context.SaveChanges();

            return documentDb.DocumentId;
        }

        public int UpdateDocument(DocumentDomain document)
        {
            if (document == null ) throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);

            if (!_context.Document.Any(x => x.DocumentId == document.DocumentId))
            {
                throw new NsiArgumentException(DocumentMessages.DocumentInvalidId);
            }

            var documentDb = _context.Document.Where(x => x.DocumentId == document.DocumentId).FirstOrDefault().FromDomainModel(document);

            if (documentDb == null) throw new NsiNotFoundException(DocumentMessages.DocumentNotFound);

            _context.SaveChanges();

            return documentDb.DocumentId;
        }

        public bool DeleteDocument(int documentId)
        {
            if (documentId <= 0) throw new NsiArgumentException(DocumentMessages.DocumentInvalidArgument);

            var documentDb = _context.Document.Where(x => x.DocumentId == documentId).FirstOrDefault();

            if (documentDb == null)
            {
                throw new NsiNotFoundException(DocumentMessages.DocumentInvalidId);
            }

            _context.Document.Remove(documentDb);
            _context.SaveChanges();

            return true;
        }

    }
}