using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSI.BusinessLogic.Interfaces.TemplateManagement;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Common.Resources;
using NSI.Common.Resources.TemplateManagement;
using NSI.DataContracts.TemplateManagement;
using NSI.Domain.TemplateManagement;
using NSI.Repository.Interfaces.TemplateManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NSI.BusinessLogic.TemplateManagement
{
    public class TemplateManipulation : ITemplateManipulation
    {
        public const string VERSION_CODE = "v1";
        private readonly ITemplateRepository _templateRepository;
        private readonly ITemplateVersionManipulation _templateVersionManipulation;
        private readonly IFolderManipulation _folderManipulation;
        

        public TemplateManipulation(ITemplateRepository templateRepository,ITemplateVersionManipulation templateVersionManipulation,
            IFolderManipulation folderManipulation)
        {
            _templateRepository = templateRepository;
            _templateVersionManipulation = templateVersionManipulation;
            _folderManipulation = folderManipulation;
        }

        public int Add(CreateTemplateRequest template)
        {

            if (!_folderManipulation.Exists(template.FolderId))
                throw new NsiArgumentException(string.Format(TemplateManagementMessages.FolderInvalidId));

            TemplateDomain templateDomain = new TemplateDomain
            {
                Name = template.Name,
                FolderId = template.FolderId,
                DateCreated = DateTime.Now
            };

            var result = _templateRepository.Add(templateDomain);
            if (result <= 0)
                throw new NsiBaseException(string.Format(TemplateManagementMessages.TemplateCreationFailed));
           
            TemplateVersionDomain templateVersionDomain = new TemplateVersionDomain
            {
                Code = VERSION_CODE,
                TemplateId = result,
                IsDefault = true,
                DateCreated = templateDomain.DateCreated,
                Content = JsonConvert.SerializeObject(template.Content)
            };
            var version = _templateVersionManipulation.Add(templateVersionDomain);
            if (version <= 0)
                throw new NsiBaseException(string.Format(TemplateManagementMessages.TemplateCreationFailed));

            return result;
        }

        public ICollection<TemplateDomain> GetAll(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging)
        {
            paging.ValidatePagingCriteria();
            return _templateRepository.filter(filterCriteria, sortCriteria, paging);
        }

        public ICollection<TemplateDomain> GetAllByFolderId(int folderId, Paging paging)
        {
            if (folderId <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            paging.ValidatePagingCriteria();
            IList<FilterCriteria> filterList = new List<FilterCriteria>
            {
                new FilterCriteria{ColumnName = "folder", FilterTerm = folderId.ToString()}
            };
            return _templateRepository.filter(filterList, null, paging);
        }
        public ICollection<TemplateDomain> GetAllByName(string name, Paging paging)
        {
            if (string.IsNullOrWhiteSpace(name))
                return _templateRepository.filter(null, null, paging);
            paging.ValidatePagingCriteria();
            IList<FilterCriteria> filterList = new List<FilterCriteria>
            {
                new FilterCriteria{ColumnName = "name", FilterTerm = name }
            };
            return _templateRepository.filter(filterList, null, paging);
        }
        public TemplateDomain GetById(int templateId)
        {
            try
            {
                IList<FilterCriteria> filterList = new List<FilterCriteria>
                {
                    new FilterCriteria{ColumnName = "id", FilterTerm = templateId.ToString() }
                };
                return _templateRepository.filter(filterList, null, null).Single();
            }
            catch (InvalidOperationException e)
            {
                throw new NsiArgumentException(TemplateManagementMessages.TemplateInvalidId, e, Common.Enumerations.SeverityEnum.Error);
            
            }
        }

        public string GetTemplateNameById(int templateId)
        {
            try
            {
                IList<FilterCriteria> filterList = new List<FilterCriteria>
                {
                    new FilterCriteria {ColumnName = "id", FilterTerm = templateId.ToString()}
                };
                return _templateRepository.filter(filterList, null, null).Single().Name;
            }
            catch (InvalidOperationException e)
            {
                throw new NsiArgumentException(TemplateManagementMessages.TemplateInvalidId, e, Common.Enumerations.SeverityEnum.Error);
            }
        }
        public bool Exists(int templateId)
        {
            IList<FilterCriteria> filterList = new List<FilterCriteria>
            {
                new FilterCriteria {ColumnName = "id", FilterTerm = templateId.ToString()}
            };
            return _templateRepository.filter(filterList, null, null).Any();
        }

        public object DeleteByTemplateVersionId(int templateVersionId)
        {
            try
            {
                _templateRepository.DeleteByTemplateVersion(templateVersionId);
                return null;
            }
            catch (InvalidOperationException e)
            {
                throw new NsiArgumentException(TemplateManagementMessages.TemplateVersionInvalidId, e, Common.Enumerations.SeverityEnum.Error);
            }
        }


    }
}
