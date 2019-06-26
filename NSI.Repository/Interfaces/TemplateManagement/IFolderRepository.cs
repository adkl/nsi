using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.TemplateManagement
{
    public interface IFolderRepository
    {
        ICollection<FolderDomain> GetAll(Paging paging);
        ICollection<FolderDomain> GetAllRootFolders(Paging paging);
        ICollection<FolderDomain> GetAllSubFolders(int parentId, Paging paging);
        int Add(FolderDomain folder);
        bool Exists(int folderId);



    }
}
