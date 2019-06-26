using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using NSI.BusinessLogic.Membership;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Membership
{
    [TestClass]
    public class LanguageManipulationTests
    {
        private Mock<ILanguageRepository> _languageRepositoryMock;
        private LanguageManipulation _languageManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _languageRepositoryMock = LanguageRepositoryMock.GetLanguageRepositoryMock();
            _languageManipulation = new LanguageManipulation(_languageRepositoryMock.Object);
        }

        [TestMethod, TestCategory("Language - Get Language By Id")]
        public void GetLanguageById_Success()
        {
            LanguageDomain module = _languageManipulation.GetLanguageById(1);
            Assert.AreEqual(1, module.Id);
        }

        [TestMethod, TestCategory("Language - Get Language By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided language ID is not valid.", SeverityEnum.Warning)]
        public void GetLanguageById_Fail_InvalidId()
        {
            _languageManipulation.GetLanguageById(-1);
        }

        [TestMethod, TestCategory("Language - Get All Languages")]
        public void GetAllLanguages_Success()
        {
            _languageManipulation.GetAllLanguages();
        }

        [TestMethod, TestCategory("Language - Add Language")]
        public void AddLanguage_Success()
        {
            int LanguageId = _languageManipulation.AddLanguage(GetValidLanguageDomain());
            Assert.AreEqual(1, LanguageId);
        }

        [TestMethod, TestCategory("Language - Add Language")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Language details are not provided.", SeverityEnum.Warning)]
        public void AddLanguage_Fail_InvalidModule()
        {
            _languageManipulation.AddLanguage(null);
        }

        [TestMethod, TestCategory("Language - Add Language")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Language ISO code can only have 5 characters maximum.", SeverityEnum.Warning)]
        public void AddLanguage_Fail_CodeLengthExceeded()
        {
            LanguageDomain language = new LanguageDomain()
            {
                Name = "Engleski jezik",
                IsoCode = "engleski",
                IsActive = true,
                IsDefault = true,
                Id = 1
            };
            _languageManipulation.AddLanguage(language);
        }

        [TestMethod, TestCategory("Language - Add Language")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Language name can only have 30 characters maximum.", SeverityEnum.Warning)]
        public void AddLanguage_Fail_NameLengthExceeded()
        {
            LanguageDomain language = new LanguageDomain()
            {
                Name = "Engleski jezikEngleski jezikEngleski jezikEngleski jezikEngleski jezik",
                IsoCode = "en",
                IsActive = true,
                IsDefault = true,
                Id = 1
            };
            _languageManipulation.AddLanguage(language);
        }

        [TestMethod, TestCategory("Language - Add Language")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Language ISO code is not provided. ", SeverityEnum.Warning)]
        public void AddLanguage_Fail_CodeInvalid()
        {
            LanguageDomain language = new LanguageDomain()
            {
                Name = "Engleski jezik",
                IsoCode = "",
                IsActive = true,
                IsDefault = true,
                Id = 1
            };
            _languageManipulation.AddLanguage(language);
        }

        [TestMethod, TestCategory("Language - Add Language")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Language name is not provided.", SeverityEnum.Warning)]
        public void AddLanguage_Fail_NameInvalid()
        {
            LanguageDomain language = new LanguageDomain()
            {
                Name = "",
                IsoCode = "en",
                IsActive = true,
                IsDefault = true,
                Id = 1
            };
            _languageManipulation.AddLanguage(language);
        }

        [TestMethod, TestCategory("Language - Add Language")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Language with provided ISO code already exists.", SeverityEnum.Warning)]
        public void AddLanguage_Fail_CodeAlreadyExists()
        {
            var language = GetValidLanguageDomain();
            language.IsoCode = "en";
            _languageManipulation.AddLanguage(language);
        }

        [TestMethod, TestCategory("Language - Update Language")]
        public void UpdateLanguage_Success()
        {
            var language = _languageManipulation.GetLanguageById(1);
            language.Name = "Updated name";
            _languageManipulation.UpdateLanguage(language);
            Assert.AreEqual("Updated name", _languageManipulation.GetLanguageById(1).Name);
        }

        [TestMethod, TestCategory("Language - Update Language")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Language details are not provided.", SeverityEnum.Warning)]
        public void UpdateLanguage_Fail_NullValue()
        {
            _languageManipulation.UpdateLanguage(null);
        }

        private LanguageDomain GetValidLanguageDomain()
        {
            return new LanguageDomain()
            {
                Name = "Engleski jezik",
                IsoCode = "eng",
                IsActive = true,
                IsDefault = true,
                Id = 1
            };
        }
    }
}
