using NSI.Common.Models;
using NSI.DataContracts.TemplateManagement;
using NSI.Domain.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.TemplateManagement
{
    public interface IFolderManipulation
    {
        ICollection<FolderDomain> GetAll(Paging paging);
        ICollection<FolderDomain> GetAllRootFolders(Paging paging);        
        ICollection<FolderDomain> GetAllSubFolders(int parentId, Paging paging);
        int Add(CreateFolderRequest folder);
        bool Exists(int folderId);

    }
}
