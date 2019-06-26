using NSI.BusinessLogic.Interfaces.TemplateManagement;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Common.Resources;
using NSI.DataContracts.TemplateManagement;
using NSI.Domain.TemplateManagement;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.TemplateManagement
{
    public class FolderManipulation : IFolderManipulation
    {
        private readonly IFolderRepository _folderRepository;

        public FolderManipulation(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }

        public int Add(CreateFolderRequest folder)
        {
            if (folder == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            FolderDomain folderDomain = new FolderDomain
            {
                DateCreated = DateTime.Now,
                Name = folder.Name,
                ParentFolderId = folder.ParentFolderId
            };
            if (!Exists(folder.ParentFolderId) && folder.ParentFolderId != 0) 
                throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            return _folderRepository.Add(folderDomain);
        }

        public ICollection<FolderDomain> GetAll(Paging paging)
        {
            paging.ValidatePagingCriteria();
            return _folderRepository.GetAll(paging);
        }

        public ICollection<FolderDomain> GetAllRootFolders(Paging paging)
        {
            paging.ValidatePagingCriteria();
            return _folderRepository.GetAllRootFolders(paging);
        }
        public ICollection<FolderDomain> GetAllSubFolders(int parentId, Paging paging)
        {
            paging.ValidatePagingCriteria();
            return _folderRepository.GetAllSubFolders(parentId,paging);
        }

        public bool Exists(int folderId)
        {
            return _folderRepository.Exists(folderId);
        }
    }
}
