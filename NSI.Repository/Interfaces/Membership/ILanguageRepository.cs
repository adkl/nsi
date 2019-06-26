using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Membership
{
    public interface ILanguageRepository
    {
        /// <summary>
        /// Exposes methods for languages manipulation
        /// </summary>
        ICollection<LanguageDomain> GetAll();
        LanguageDomain GetById(int languageId);
        LanguageDomain GetByISOCode(string languageISOCode);
        int Add(LanguageDomain language);
        void Update(LanguageDomain language);
        ICollection<LanguageDomain> SearchLanguages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);

    }
}
