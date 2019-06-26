using NSI.Domain.Document;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.DocumentNamespace
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public DocumentTypeRepository(NsiContext context)
        {
            _context = context;
        }

        public int Add(DocumentTypeDomain documentType)
        {
            var documentTypeDb = new DocumentType().FromDomainModel(documentType);
            _context.DocumentType.Add(documentTypeDb);
            _context.SaveChanges();
            return documentTypeDb.DocumentTypeId;
        }

        /// <summary>
        /// Retrieves all document types from the database
        /// </summary>
        /// <returns><see cref="ICollection{DocumentTypeDomain}"/></returns>
        public ICollection<DocumentTypeDomain> GetAll()
        {
            var result = _context.DocumentType
                .ToList();
            return result
                .Select(x => x.ToDomainModel())
                .ToList();
        }

        /// <summary>
        /// Retrieves document type with provided ID
        /// </summary>
        /// <param name="documentTypeId">Module ID</param>
        /// <returns>Module if it exists, instance of <see cref="DocumentTypeDomain"/>. Else null.</returns>
        public DocumentTypeDomain GetById(int documentTypeId)
        {
            return _context.DocumentType
                .FirstOrDefault(x => x.DocumentTypeId == documentTypeId)
                .ToDomainModel();
        }

        /// <summary>
        /// Retrieves document type with provided ID
        /// </summary>
        /// <param name="documentTypeId">Module ID</param>
        /// <returns>Module if it exists, instance of <see cref="DocumentTypeDomain"/>. Else null.</returns>
        public DocumentTypeDomain GetByName(string documentTypeName)
        {
            return _context.DocumentType
                .FirstOrDefault(x => x.Name.ToLower() == documentTypeName.ToLower())
                .ToDomainModel();
        }

    }
}
