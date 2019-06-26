using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.TemplateManagement
{
    public class TemplateDomain : BaseDomain
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int FolderId { get; set; }
        public FolderDomain Folder { get; set; }
        public ICollection<TemplateVersionDomain> Versions { get; set; }
    }
}
