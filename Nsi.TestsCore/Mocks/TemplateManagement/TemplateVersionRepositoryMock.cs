using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using NSI.Repository.Interfaces.TemplateManagement;

namespace Nsi.TestsCore.Mocks.TemplateManagement
{
    public class TemplateVersionRepositoryMock
    {
        public static Mock<ITemplateVersionRepository> GetTemplateVersionRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var templateVersionRepository = new Mock<ITemplateVersionRepository> { CallBase = true };

            #region Add 
            templateVersionRepository.Setup(x => x.Add(It.IsAny<TemplateVersionDomain>())).Returns(1);
            #endregion

            #region UpdateTemplateVersion 
            templateVersionRepository.Setup(x => x.UpdateTemplateVersion(It.IsAny<TemplateVersionDomain>()));
            #endregion

            #region DeleteTemplateVersion 
            templateVersionRepository.Setup(x => x.DeleteTemplateVersion(1));
            #endregion

            List<TemplateVersionDomain> listToReturn = new List<TemplateVersionDomain>();
            listToReturn.Add(new TemplateVersionDomain() {
                DateCreated = DateTime.Now,
                IsDefault = true,
                TemplateId = 1,
                Content ="{\"Type\":0,\"Payload\":{\"Text\":\"URL: #1#\nNAME: #2#\",\"Placeholders\":[{\"Id\":1,\"Description\":\"Url\",\"Type\":4,\"Length\":16},{\"Id\":2,\"Description\":\"firstname\",\"Type\":0,\"Length\":20}]}}",
                Code = "v1"
            });

            List<TemplateVersionDomain> tableContentList = new List<TemplateVersionDomain>();
            tableContentList.Add(new TemplateVersionDomain() {
                DateCreated = DateTime.Now,
                IsDefault = true,
                TemplateId = 20,
                Content ="{\"Type\":1,\"Payload\":{\"Placeholders\":[{\"Id\":1,\"Description\":\"Url\",\"Type\":4,\"Length\":16},{\"Id\":2,\"Description\":\"firstname\",\"Type\":0,\"Length\":20}]}}",
                Code = "v1"
            });

            List<TemplateVersionDomain> nullList = null;
            #region filter
            templateVersionRepository.Setup(x => x.filter(It.IsAny<List<FilterCriteria>>(), It.IsAny<List<SortCriteria>>(), It.IsAny<Paging>())).Returns(listToReturn);

            templateVersionRepository.Setup(x => x.filter(It.Is<List<FilterCriteria>>(f => Equals(f[0].FilterTerm, "20")), It.IsAny<List<SortCriteria>>(), It.IsAny<Paging>())).Returns(tableContentList);

            templateVersionRepository.Setup(x => x.filter(It.Is<List<FilterCriteria>>(f => Equals(f[0].FilterTerm, "5")), It.IsAny<List<SortCriteria>>(), It.IsAny<Paging>())).Returns(nullList);

            templateVersionRepository.Setup(x => x.filter(It.Is<List<FilterCriteria>>(f => Equals(f[0].FilterTerm, "10")), It.IsAny<List<SortCriteria>>(), It.IsAny<Paging>())).Returns(new List<TemplateVersionDomain>());

            #endregion

            #region SortTemplateVersions
            //templateVersionRepository.Setup(x => x.FilterTemplateVersions(It.IsAny<string>()));
            #endregion
            return templateVersionRepository;
        }
    }
}
