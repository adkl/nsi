using NSI.Common.Models;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Membership
{
    /// <summary>
    /// Exposes methods for manipulating languages
    /// </summary>
    public interface ILanguageManipulation
    {
        ICollection<LanguageDomain> GetAllLanguages();
        LanguageDomain GetLanguageById(int languageId);
        int AddLanguage(LanguageDomain language);
        void UpdateLanguage(LanguageDomain language);
        ICollection<LanguageDomain> SearchLanguages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria);

    }
}
