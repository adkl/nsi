using NSI.Common.Exceptions;
using NSI.Common.Extensions;
using NSI.Common.Interfaces;
using NSI.Common.Models;
using NSI.Common.Resources.TemplateManagement;
using NSI.Domain.TemplateManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.TemplateManagement
{
    public class TemplateRepository : ITemplateRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public TemplateRepository(NsiContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Adds new template
        /// </summary>
        /// <returns></returns>
        public int Add(TemplateDomain template)
        {
            if (!_context.Folder.Any(x => x.FolderId == template.FolderId))
            {
                throw new NsiArgumentException(TemplateManagementMessages.FolderInvalidId);
            }
            var templateDb = new Template().FromDomainModel(template);
            _context.Template.Add(templateDb);
            _context.SaveChanges();
            return templateDb.TemplateId;
        }
        /// <summary>
        /// Retrieves all templates from the database by a criteria
        /// </summary>
        /// <returns><see cref="ICollection{TemplateDomain}"/></returns>
        public ICollection<TemplateDomain> filter(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging)
        {
            var result = _context.Template
                .Include(x => x.Folder)
                .Include(x => x.TemplateVersion)
                .DoFiltering(filterCriteria, FilterTemplates)
                .DoSorting(sortCriteria, SortTemplates)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }
        public bool Exists(int templateId)
        {
            return _context.Template.Any(x => x.TemplateId == templateId);
        }

        private Expression<Func<Template, object>> SortTemplates(string columnName)
        {
            Expression<Func<Template, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "datecreated":
                    fnc = x => x.DateCreated.ToString();
                    break;
                case "name":
                    fnc = x => x.Name;
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<Template, bool>> FilterTemplates(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Template, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "id":
                    fnc = x => x.TemplateId.ToString() == filterTerm;
                    break;
                case "folder":
                    fnc = x => (x.FolderId).ToString() == filterTerm;
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

        public void DeleteByTemplateVersion(int templateVersionId)
        {
            var templateVersionDb = _context.TemplateVersion.Single(x => x.TemplateVersionId == templateVersionId);
            var template = _context.Template.Single(x => x.TemplateId == templateVersionDb.TemplateId);
            // first delete child objects
            var result = _context.TemplateVersion
                .Where(x => x.TemplateId == template.TemplateId)
                .ToList();
            for(int i = 0; i < result.Count; i++)
            {
                var tempVer = result[i];
                List<GeneratedDocument> generatedDocumentsDb = _context.GeneratedDocument.Where(x => x.TemplateVersionId == tempVer.TemplateVersionId).ToList();
                for (int j = 0; j < generatedDocumentsDb.Count; j++)
                {
                    generatedDocumentsDb[j].TemplateVersionId = null;
                    generatedDocumentsDb[j].TemplateVersion = null;
                }
            }
            _context.TemplateVersion.RemoveRange(result);
            _context.Template.Remove(template);
            _context.SaveChanges();

        }
    }
}
