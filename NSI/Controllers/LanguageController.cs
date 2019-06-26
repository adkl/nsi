using NSI.BusinessLogic.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using NSI.DataContracts.Base;
using NSI.DataContracts.Membership.Languages;
using NSI.Resources.Membership;
using NSI.Domain.Membership;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes API methods for manipulating system languages
    /// </summary>
    //Uncomment for authorization
    //[NsiAuthorization]
    public class LanguageController : ApiController
    {
        private readonly ILanguageManipulation _languageManipulation;

        /// <summary>
        /// Language controller constructor
        /// </summary>
        /// <param name="languageManipulation">Instance of <see cref="ILanguageManipulation"/></param>
        public LanguageController(ILanguageManipulation languageManipulation)
        {
            _languageManipulation = languageManipulation;
        }

        /// <summary>
        /// Retrieves single language by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="GetLanguageRequest"/></param>
        /// <returns><see cref="GetLanguageResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetLanguageResponse))]
        public IHttpActionResult GetLanguage(int id)
        {
            if (id < 1)
            {
                return BadRequest(MembershipMessages.LanguageIdInvalid);
            }

            var data = _languageManipulation.GetLanguageById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetLanguageResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Searches languages. If no parameters have been provided in request, return all languages.
        /// </summary>
        /// <param name="request"><see cref="SearchLanguagesRequest"/></param>
        /// <returns><see cref="SearchLanguagesResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchLanguagesResponse))]
        public IHttpActionResult SearchLanguages(SearchLanguagesRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchLanguagesResponse()
            {
                Data = _languageManipulation.SearchLanguages(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new language
        /// </summary>
        /// <param name="request"><see cref="AddLanguageRequest"/></param>
        /// <returns><see cref="AddLanguageResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddLanguageResponse))]
        public IHttpActionResult AddLanguage(AddLanguageRequest request)
        {
            request.ValidateNotNull();

            LanguageDomain languageDomain = new LanguageDomain()
            {
                Name = request.Name,
                IsoCode = request.IsoCode,
                IsActive = request.IsActive,
                IsDefault = request.IsDefault
            };

            return Ok(new AddLanguageResponse()
            {
                Data = _languageManipulation.AddLanguage(languageDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates language
        /// </summary>
        /// <param name="request"><see cref="UpdateLanguageRequest"/></param>
        /// <returns><see cref="UpdateLanguageResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(UpdateLanguageResponse))]
        public IHttpActionResult UpdateLanguage(UpdateLanguageRequest request)
        {
            request.ValidateNotNull();

            LanguageDomain languageDomain = _languageManipulation.GetLanguageById(request.Id);

            if (languageDomain == null)
            {
                return NotFound();
            }

            languageDomain.Name = request.Name;
            languageDomain.IsoCode = request.IsoCode;
            languageDomain.IsActive = request.IsActive;
            languageDomain.IsDefault = request.IsDefault;

            _languageManipulation.UpdateLanguage(languageDomain);

            return Ok(new UpdateLanguageResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}
