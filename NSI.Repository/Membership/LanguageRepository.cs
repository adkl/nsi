using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.Membership;
using NSI.EF;
using NSI.Repository.Extensions;
using System.Data.Entity;
using NSI.Common.Models;
using System.Linq.Expressions;
using NSI.Common.Extensions;

namespace NSI.Repository.Membership
{
    public class LanguageRepository : ILanguageRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public LanguageRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public int Add(LanguageDomain language)
        {
            var languageDb = new Language().FromDomainModel(language);
            _context.Language.Add(languageDb);
            _context.SaveChanges();
            return languageDb.LanguageId;
        }

        /// <summary>
        /// Get All Languages
        /// </summary>
        /// <returns></returns>
        public ICollection<LanguageDomain> GetAll()
        {
            return _context.Language
                .Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Get Language By Language ID
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public LanguageDomain GetById(int languageId)
        {
            return _context.Language.FirstOrDefault(x => x.LanguageId == languageId).ToDomainModel();
        }

        /// <summary>
        /// Get Language By ISO Code
        /// </summary>
        /// <param name="languageISOCode"></param>
        /// <returns></returns>
        public LanguageDomain GetByISOCode(string languageISOCode)
        {
            return _context.Language.FirstOrDefault(x => x.ISOCode == languageISOCode).ToDomainModel();
        }

        /// <summary>
        /// Update language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public void Update(LanguageDomain language)
        {
            var languageDb = _context.Language.FirstOrDefault(x => x.LanguageId == language.Id);
            languageDb.FromDomainModel(language);
            _context.SaveChanges();
        }

        /// <summary>
        /// Search Languages
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns></returns>
        public ICollection<LanguageDomain> SearchLanguages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.Language
                .DoFiltering(filterCriteria, FilterLanguages)
                .DoSorting(sortCriteria, SortLanguages)
                .DoPaging(paging)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }
        #region Private methods
        private Expression<Func<Language, object>> SortLanguages(string columnName)
        {
            Expression<Func<Language, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "isocode":
                    fnc = x => x.ISOCode;
                    break;
                case "isactive":
                    fnc = x => (x.IsActive).ToString();
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<Language, bool>> FilterLanguages(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Language, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "isocode":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.ISOCode).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.ISOCode == filterTerm;
                    }
                    break;
            }

            return fnc;
        }
        #endregion

    }
}

