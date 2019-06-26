using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.Membership;
using NSI.EF;
using NSI.Repository.Extensions;
using System.Data.Entity;
using NSI.Common.Models;
using System.Linq.Expressions;
using NSI.Common.Extensions;


namespace NSI.Repository.Membership
{
    public class ModuleRepository : IModuleRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public ModuleRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new module to the database
        /// </summary>
        /// <param name="module">Module information to be added. Instance of <see cref="ModuleDomain"/></param>
        /// <returns>ModuleId of the newly created module</returns>
        public int Add(ModuleDomain module)
        {
            var moduleDb = new Module().FromDomainModel(module);
            _context.Module.Add(moduleDb);
            _context.SaveChanges();
            return moduleDb.ModuleId;
        }

        /// <summary>
        /// Retrieves all modules from the database
        /// </summary>
        /// <returns><see cref="ICollection{ModuleDomain}"/></returns>
        public ICollection<ModuleDomain> GetAll()
        {
            return _context.Module
                .Include(x => x.Permission)
                .Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves module with provided ID
        /// </summary>
        /// <param name="moduleId">Module ID</param>
        /// <returns>Module if it exists, instance of <see cref="ModuleDomain"/>. Else null.</returns>
        public ModuleDomain GetById(int moduleId)
        {
            return _context.Module
                .Include(x => x.Permission)
                .FirstOrDefault(x => x.ModuleId == moduleId).ToDomainModel();
        }

        /// <summary>
        /// Retrieves module with provided Code
        /// </summary>
        /// <param name="moduleCode">Module Code</param>
        /// <returns>Module if it exists, instance of <see cref="ModuleDomain"/>. Else null.</returns>
        public ModuleDomain GetByCode(string moduleCode)
        {
            return _context.Module.FirstOrDefault(x => x.Code == moduleCode).ToDomainModel();
        }

        /// <summary>
        /// Retrieves all modules belonging to Tenant with provided Tenant ID
        /// </summary>
        /// <param name="tenantId">Tenant ID</param>
        /// <returns><see cref="ICollection{ModuleDomain}"/></returns>
        public ICollection<ModuleDomain> GetTenantModules(int tenantId)
        {
            return _context.TenantModule
               .Include(x => x.Module)
               .Where(x => x.TenantId == tenantId)
               .Select(x => x.Module.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Update Module
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public void Update(ModuleDomain module)
        {
            var moduleDb = _context.Module.FirstOrDefault(x => x.ModuleId == module.Id);
            moduleDb.FromDomainModel(module);
            _context.SaveChanges();
        }

        /// <summary>
        /// Search Modules
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{ModuleDomain}"/></returns>
        public ICollection<ModuleDomain> SearchModules(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            var result = _context.Module
                .DoFiltering(filterCriteria, FilterModules)
                .DoSorting(sortCriteria, SortModules)
                .DoPaging(paging)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        #region Private methods
        private Expression<Func<Module, object>> SortModules(string columnName)
        {
            Expression<Func<Module, object>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "code":
                    fnc = x => x.Code;
                    break;
                case "isactive":
                    fnc = x => (x.IsActive).ToString();
                    break;
                default:
                    break;
            }

            return fnc;
        }

        private Expression<Func<Module, bool>> FilterModules(string columnName, string filterTerm, bool isExactMatch)
        {
            Expression<Func<Module, bool>> fnc = null;

            switch (columnName.ToLowerInvariant())
            {
                case "code":
                    if (!isExactMatch)
                    {
                        fnc = x => (x.Code).Contains(filterTerm);
                    }
                    else
                    {
                        fnc = x => x.Code == filterTerm;
                    }
                    break;
            }

            return fnc;
        }
        #endregion
    }
}
