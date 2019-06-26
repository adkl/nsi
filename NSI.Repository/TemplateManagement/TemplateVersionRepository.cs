using NSI.Common.Extensions;
using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.TemplateManagement
{
    public class TemplateVersionRepository : ITemplateVersionRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public TemplateVersionRepository(NsiContext context)
        {
            _context = context;

        }

        /// <summary>
        /// Adds new templateversion
        /// </summary>
        /// <returns></returns>
        public int Add(TemplateVersionDomain templateVersion)
        {
            var templateVersionDb = new TemplateVersion().FromDomainModel(templateVersion);
            _context.TemplateVersion.Add(templateVersionDb);
            _context.SaveChanges();
            return templateVersionDb.TemplateVersionId;
        }
        public void UpdateTemplateVersion(TemplateVersionDomain templateVersion)
        {
            var templateVersionDb = _context.TemplateVersion.FirstOrDefault(x => x.TemplateVersionId == templateVersion.Id);
            templateVersionDb.FromDomainModel(templateVersion);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes template version from the database by templateID
        /// </summary>
        public void DeleteTemplateVersion(int templateVersionId)
        {
            var templateVersionDb = _context.TemplateVersion.Single(x => x.TemplateVersionId == templateVersionId);
            _context.TemplateVersion.Remove(templateVersionDb);
            List<GeneratedDocument> generatedDocumentsDb = _context.GeneratedDocument.Where(x => x.TemplateVersionId == templateVersionId).ToList();
            for(int i = 0; i < generatedDocumentsDb.Count; i++)
            {
                generatedDocumentsDb[i].TemplateVersionId = null;
                generatedDocumentsDb[i].TemplateVersion = null;
            }
            if (_context.TemplateVersion.Count(x => x.TemplateId == templateVersionDb.TemplateId) == 1) //  last template version of template
            {
                var template = _context.Template.Single(x => x.TemplateId == templateVersionDb.TemplateId);
                _context.Template.Remove(template);
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// Retrieves all template versions from the database by a criteria
        /// </summary>
        /// <returns><see cref="ICollection{TemplateVersionDomain}"/></returns>
        public ICollection<TemplateVersionDomain> filter(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging)
        {
            var result = _context.TemplateVersion
                .DoFiltering(filterCriteria, FilterTemplateVersions)
                .DoSorting(sortCriteria, SortTemplateVersions)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        private Expression<Func<TemplateVersion, object>> SortTemplateVersions(string columnName)
        {
            Expression<Func<TemplateVersion, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "datecreated":
                    fnc = x => x.DateCreated.ToString();
                    break;
                case "isdefault":
                    fnc = x => x.IsDefault.ToString();
                    break;
                case "code":
                    fnc = x => x.Code;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<TemplateVersion, bool>> FilterTemplateVersions(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<TemplateVersion, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "id":
                    fnc = x => x.TemplateVersionId.ToString() == filterTerm;
                    break;
                case "template":
                    fnc = x => (x.TemplateId).ToString() == filterTerm;
                    break;
                case "code":
                    fnc = x => x.Code == filterTerm;
                    break;
                case "content":
                    fnc = x => (x.Content).Contains(filterTerm);
                    break;
                case "isdefault":
                    fnc = x => x.IsDefault;
                    break;
                default:
                    break;
            }
            return fnc;
        }



    }
}
