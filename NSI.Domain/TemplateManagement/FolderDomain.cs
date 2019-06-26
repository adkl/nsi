using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.TemplateManagement
{
    public class FolderDomain : BaseDomain
    {
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentFolderId { get; set; }
    }
}
