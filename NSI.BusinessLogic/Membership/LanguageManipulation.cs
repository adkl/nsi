using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Common.Exceptions;
using NSI.Common.Extensions;
using NSI.Common.Helpers;
using NSI.Common.Models;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using NSI.Resources.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Membership
{
    public class LanguageManipulation : ILanguageManipulation
    {
        private readonly ILanguageRepository _languageRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="languageRepository"></param>
        public LanguageManipulation(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        /// <summary>
        /// Add a new language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public int AddLanguage(LanguageDomain language)
        {
            ValidateLanguageModel(language);
            //Check if code exists
            var languageWithProvidedCode = _languageRepository.GetByISOCode(language.IsoCode.SafeTrim());

            if (languageWithProvidedCode != null)
            {
                throw new NsiArgumentException(MembershipMessages.LanguageISOCodeAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            return _languageRepository.Add(language);
        }

        /// <summary>
        /// Get All Languages
        /// </summary>
        /// <returns></returns>
        public ICollection<LanguageDomain> GetAllLanguages()
        {
            return _languageRepository.GetAll();
        }

        /// <summary>
        /// Get Language By Language ID
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        public LanguageDomain GetLanguageById(int languageId)
        {
            ValidationHelper.GreaterThanZero(languageId, MembershipMessages.LanguageIdInvalid);
            return _languageRepository.GetById(languageId);
        }

        /// <summary>
        /// Search Languages
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns></returns>
        public ICollection<LanguageDomain> SearchLanguages(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _languageRepository.SearchLanguages(paging, filterCriteria, sortCriteria);

        }

        /// <summary>
        /// Update language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public void UpdateLanguage(LanguageDomain language)
        {
            ValidateLanguageModel(language);
            ValidationHelper.GreaterThanZero(language.Id, MembershipMessages.LanguageIdInvalid);
            ValidationHelper.NotNull(_languageRepository.GetById(language.Id), MembershipMessages.LanguageWithIdDoesNotExist);
            
            _languageRepository.Update(language);
        }
        private void ValidateLanguageModel(LanguageDomain language)
        {
            ValidationHelper.NotNull(language, MembershipMessages.LanguageNotProvided);
            ValidationHelper.MaxLength(language.IsoCode, 5, MembershipMessages.LanguageIsCodeLenghtExceeded);
            ValidationHelper.MaxLength(language.Name, 30, MembershipMessages.LanguageNameLenghtExceeded);
            ValidationHelper.NotNullOrWhitespace(language.Name, MembershipMessages.LanguageNameNotProvided);
            ValidationHelper.NotNullOrWhitespace(language.IsoCode, MembershipMessages.LanguageIsoCodeNotProvided);
            
        }
    }
}
