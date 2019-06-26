using NSI.Common.Models;
using NSI.Domain.Document;
using NSI.Common.Extensions;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace NSI.Repository.DocumentNamespace
{
    public class GeneratedDocumentRepository : IGeneratedDocumentRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public GeneratedDocumentRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates new record for generated document
        /// </summary>
        /// <param name="generatedDocument">Instance of <see cref="GeneratedDocumentDomain"/></param>
        /// <returns> Id of a newly generated document record. </returns>
        public int Add(GeneratedDocumentDomain generatedDocument)
        {
            var generatedDocumentDb = new GeneratedDocument().FromDomainModel(generatedDocument);
            _context.GeneratedDocument.Add(generatedDocumentDb);
            _context.SaveChanges();
            return generatedDocumentDb.DocumentTypeId;
        }

        /// <summary>
        /// Retrieves all generated documents from the database order by Date created (descending). 
        /// Pagination is supported.
        /// </summary>
        /// <returns><see cref="ICollection{GeneratedDocumentDomain}"/></returns>
        public ICollection<GeneratedDocumentDomain> GetAll(IList<FilterCriteria> filterCriteria, 
            IList<SortCriteria> sortCriteria, Paging paging)
        {
            var result = _context.GeneratedDocument
                .DoFiltering(filterCriteria, FilterDocuments)
                .DoSorting(sortCriteria, SortDocuments)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves generated document with provided ID
        /// </summary>
        /// <param name="generatedDocumentId">Genereted document ID</param>
        /// <returns>Module if it exists, instance of <see cref="GeneratedDocumentDomain"/>. Else null.</returns>
        public GeneratedDocumentDomain GetById(int generatedDocumentId)
        {
            return _context.GeneratedDocument
                .FirstOrDefault(x => x.GeneratedDocumentId == generatedDocumentId)
                .ToDomainModel();
        }

        /// <summary>
        /// Retrieves content of a generated document
        /// </summary>
        /// <param name="generatedDocumentId">Genereted document ID</param>
        /// <returns> String representation of a document content </returns>
        public string GetDocumentContent(int generatedDocumentId)
        {
            return _context.GeneratedDocument
                .FirstOrDefault(x => x.GeneratedDocumentId == generatedDocumentId)
                .Content;
        }

        private Expression<Func<GeneratedDocument, object>> SortDocuments(string columnName)
        {
            Expression<Func<GeneratedDocument, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "datecreated":
                    fnc = x => x.DateCreated.ToString();
                    break;
                case "success":
                    fnc = x => x.Success.ToString();
                    break;
                case "name":
                    fnc = x => x.Name;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<GeneratedDocument, bool>> FilterDocuments(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<GeneratedDocument, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "success":
                    fnc = x => (x.Success).ToString() == filterTerm;
                    break;
                case "name":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.Name).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.Name == filterTerm;
                    }
                    break;
                default:
                    break;
            }

            return fnc;
        }
    }
}
