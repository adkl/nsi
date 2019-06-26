using NSI.Common.Extensions;
using NSI.Common.Models;
using NSI.Domain.TemplateManagement;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.TemplateManagement
{
    public class FolderRepository : IFolderRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public FolderRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new folder
        /// </summary>
        /// <returns></returns>
        public int Add(FolderDomain folder)
        {
            var folderDb = new Folder().FromDomainModel(folder);
            _context.Folder.Add(folderDb);
            _context.SaveChanges();
            return folderDb.FolderId;
        }
        /// <summary>
        /// Retrieves all folders from the database
        /// </summary>
        /// <returns><see cref="ICollection{FolderDomain}"/></returns>
        public ICollection<FolderDomain> GetAll(Paging paging)
        {
            var result = _context.Folder
              .DoPaging(paging)
              .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }
        /// <summary>
        /// Retrieves all root folders
        /// </summary>
        /// <returns><see cref="ICollection{FolderDomain}"/></returns>
        public ICollection<FolderDomain> GetAllRootFolders(Paging paging)
        {
            var result = _context.Folder
                .Where(x => x.ParentFolderId == null)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves all subfolders for given folder
        /// </summary>
        /// <returns><see cref="ICollection{FolderDomain}"/></returns>
        public ICollection<FolderDomain> GetAllSubFolders(int parentId, Paging paging)
        {
            var result = _context.Folder
                .Where(x => x.ParentFolderId == parentId)
                .DoPaging(paging)
                .ToList();
            return result.Select(x => x.ToDomainModel()).ToList();
        }

        public bool Exists(int folderId)
        {
            return _context.Folder.Any(x => x.FolderId == folderId);
        }
    }
}
