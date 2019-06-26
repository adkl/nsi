using Moq;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks
{
    public static class LanguageRepositoryMock
    {
        public static Mock<ILanguageRepository> GetLanguageRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var languageRepository = new Mock<ILanguageRepository> { CallBase = false };

            languageRepository.Setup(x => x.GetById(1)).Returns(
                new LanguageDomain()
                {
                    Name = "Engleski jezik",
                    IsoCode = "en",
                    IsActive = true,
                    IsDefault = true,
                    Id = 1
                });

            languageRepository.Setup(x => x.GetAll()).Returns(new List<LanguageDomain>()
            {
                new LanguageDomain()
                {
                    Name = "Engleski jezik",
                    IsoCode = "en",
                    IsActive = true,
                    IsDefault = true,
                    Id = 1
                },
                new LanguageDomain()
                {
                    Name = "Bosanski jezik",
                    IsoCode = "bs",
                    IsActive = true,
                    IsDefault = true,
                    Id = 2
                }
            });

            languageRepository.Setup(x => x.GetByISOCode("en")).Returns(
                new LanguageDomain()
                {
                    Name = "Engleski jezik",
                    IsoCode = "en",
                    IsActive = true,
                    IsDefault = true,
                    Id = 1
                });

            languageRepository.Setup(x => x.Add(It.IsAny<LanguageDomain>())).Returns(1);

            languageRepository.Setup(x => x.Update(It.IsAny<LanguageDomain>()));

            return languageRepository;
        }
    }
}
