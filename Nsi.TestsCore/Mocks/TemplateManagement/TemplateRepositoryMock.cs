using Moq;
using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using NSI.Repository.Interfaces.TemplateManagement;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.TemplateManagement
{
    public class TemplateRepositoryMock
    {
        public static Mock<ITemplateRepository> GetTemplateRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var templateRepository = new Mock<ITemplateRepository> { CallBase = false };

            #region Add 
            templateRepository.Setup(x => x.Add(It.IsAny<TemplateDomain>())).Returns(1);
            #endregion

           List<TemplateDomain> listToReturn = new List<TemplateDomain>();
            listToReturn.Add(new TemplateDomain()
            {
                Id = 1,
                Name = "TestName",
                DateCreated = new System.DateTime(),
                FolderId = 1,
                Folder = new FolderDomain()
                {
                    Name = "Root",
                    ParentFolderId = 0
                },
                Versions = new List<TemplateVersionDomain>()
            });

            
            #region filter
            templateRepository.Setup(x => x.filter(It.IsAny<List<FilterCriteria>>(), It.IsAny<List<SortCriteria>>(), It.IsAny<Paging>())).Returns(listToReturn);
           // templateRepository.Setup(x => x.filter(It.Is<List<FilterCriteria>>(f=> Equals(f[0].FilterTerm, "5")), It.IsAny<List<SortCriteria>>(), It.IsAny<Paging>())).Returns(dummy);
            #endregion

            #region DeleteByTemplateVersion
            templateRepository.Setup(x => x.DeleteByTemplateVersion(It.IsAny<int>()));
            #endregion

            #region FilterTemplates
            // templateRepository.Setup(x => x.FilterTemplates(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()));
            #endregion

            #region SortTemplates
            // templateRepository.Setup(x => x.SortTemplates(It.IsAny<string>()));
            #endregion

            return templateRepository;
        }
    }
}
