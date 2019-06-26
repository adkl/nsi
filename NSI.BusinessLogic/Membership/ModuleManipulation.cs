using NSI.BusinessLogic.Interfaces.Membership;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.Membership;
using NSI.Common.Extensions;
using NSI.Resources.Membership;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Common.Helpers;

namespace NSI.BusinessLogic.Membership
{
    public class ModuleManipulation : IModuleManipulation
    {
        private readonly IModuleRepository _moduleRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleRepository"></param>
        public ModuleManipulation(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        /// <summary>
        /// Adds new module to the database
        /// </summary>
        /// <param name="module">Module information to be added. Instance of <see cref="ModuleDomain"/></param>
        /// <returns>ModuleId of the newly created module</returns>
        public int AddModule(ModuleDomain module)
        {
            ValidateModuleModel(module);

            var moduleWithProvidedCode = _moduleRepository.GetByCode(module.Code.SafeTrim());

            if (moduleWithProvidedCode != null)
            {
                throw new NsiArgumentException(MembershipMessages.ModuleCodeAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            return _moduleRepository.Add(module);
        }

        /// <summary>
        /// Search Modules
        /// </summary>
        /// <param name="paging, filterCriteria, sortCriteria"></param>
        /// <returns><see cref="ICollection{ModuleDomain}"/></returns>
        public ICollection<ModuleDomain> SearchModules(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _moduleRepository.SearchModules(paging, filterCriteria, sortCriteria);
        }

        /// <summary>
        /// Retrieves all modules from the database
        /// </summary>
        /// <returns><see cref="ICollection{ModuleDomain}"/></returns>
        public ICollection<ModuleDomain> GetAllModules()
        {
            return _moduleRepository.GetAll();
        }

        /// <summary>
        /// Retrieves module with provided ID
        /// </summary>
        /// <param name="moduleId">Module ID</param>
        /// <returns>Module if it exists, instance of <see cref="ModuleDomain"/>. Else null.</returns>
        public ModuleDomain GetModuleById(int moduleId)
        {
            ValidationHelper.GreaterThanZero(moduleId, MembershipMessages.ModuleIdInvalid);
            return _moduleRepository.GetById(moduleId);
        }

        /// <summary>
        /// Retrieves module with provided Code
        /// </summary>
        /// <param name="moduleCode">Module Code</param>
        /// <returns>Module if it exists, instance of <see cref="ModuleDomain"/>. Else null.</returns>
        public ModuleDomain GetModuleByCode(string code)
        {
            ValidationHelper.NotNullOrWhitespace(code, MembershipMessages.ModuleCodeInvalid);
            return _moduleRepository.GetByCode(code);
        }

        /// <summary>
        /// Update Module
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public void UpdateModule(ModuleDomain module)
        {
            ValidateModuleModel(module);
            ValidationHelper.GreaterThanZero(module.Id, MembershipMessages.ModuleIdInvalid);
            ValidationHelper.NotNull(_moduleRepository.GetById(module.Id), MembershipMessages.ModuleWithIdDoesNotExist);

            var moduleWithProvidedCode = _moduleRepository.GetByCode(module.Code);

            if (moduleWithProvidedCode != null && moduleWithProvidedCode.Id != module.Id)
            {
                throw new NsiArgumentException(MembershipMessages.ModuleCodeAlreadyExists, Common.Enumerations.SeverityEnum.Warning);
            }

            _moduleRepository.Update(module);
        }

        private void ValidateModuleModel(ModuleDomain module)
        {
            ValidationHelper.NotNull(module, MembershipMessages.ModuleNotProvided);
            ValidationHelper.MaxLength(module.Code, 100, MembershipMessages.ModuleCodeLenghtExceeded);
            ValidationHelper.MaxLength(module.Name, 100, MembershipMessages.ModuleNameLenghtExceeded);
            ValidationHelper.NotNullOrWhitespace(module.Name, MembershipMessages.ModuleNameInvalid);
            ValidationHelper.NotNullOrWhitespace(module.Code, MembershipMessages.ModuleCodeInvalid);
        }
        
    }
}
