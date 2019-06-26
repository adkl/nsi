using Moq;
using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.TemplateManagement
{
    public class FolderRepositoryMock
    {
        public static Mock<IFolderRepository> GetFolderRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var folderRepository = new Mock<IFolderRepository> { CallBase = false };

            #region Add 
            folderRepository.Setup(x => x.Add(It.IsAny<FolderDomain>())).Returns(1);
            #endregion

            #region Get all 
            folderRepository.Setup(x => x.GetAll(It.IsAny<Paging>())).Returns(new List<FolderDomain>());
            #endregion

            #region GetAllRootFolders 
            folderRepository.Setup(x => x.GetAllRootFolders(It.IsAny<Paging>())).Returns(new List<FolderDomain>());
            #endregion

            #region GetAllSubFolders
            folderRepository.Setup(x => x.GetAllSubFolders(It.IsAny<int>(), It.IsAny<Paging>())).Returns(new List<FolderDomain>());
            #endregion

            #region Exists
            folderRepository.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
            #endregion

            return folderRepository;
        }
    }
}
