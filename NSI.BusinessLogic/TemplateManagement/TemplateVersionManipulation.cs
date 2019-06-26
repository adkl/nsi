using Newtonsoft.Json;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.TemplateManagement
{
    public class TemplateVersionManipulation : ITemplateVersionManipulation
    {
        public const string REGEX_NUMBER = "[0-9]+";
        private readonly ITemplateVersionRepository _templateVersionRepository;
        public TemplateVersionManipulation(ITemplateVersionRepository templateVersionRepository)
        {
            _templateVersionRepository = templateVersionRepository;
        }
        public int Add(TemplateVersionDomain templateVersion)
        {
            if (templateVersion == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            return _templateVersionRepository.Add(templateVersion);
        }

        public int Add(CreateTemplateVersionRequest templateVersion)
        {
            TemplateVersionDomain previousTemplateVersionDomain = GetDefaultByTemplateId(templateVersion.TemplateId);
            previousTemplateVersionDomain.IsDefault = false;
            string Code = previousTemplateVersionDomain.Code;
            string newCode = Regex.Match(Code, REGEX_NUMBER).Value;            
            int codeNumber = Int32.Parse(newCode)+1;
            Regex r = new Regex(REGEX_NUMBER);
            Code = r.Replace(Code, codeNumber.ToString());
            TemplateVersionDomain templateVersionDomain = new TemplateVersionDomain
            {
                Code = Code,
                TemplateId = templateVersion.TemplateId,
                IsDefault = true,
                DateCreated = DateTime.Now,
                Content = JsonConvert.SerializeObject(templateVersion.Content)
            };
            var result = _templateVersionRepository.Add(templateVersionDomain);
            if (result <= 0)
                throw new NsiBaseException(string.Format(TemplateManagementMessages.TemplateCreationFailed));
            _templateVersionRepository.UpdateTemplateVersion(previousTemplateVersionDomain);
            return result;
        }

        public object DeleteByVersionId(int templateVersionId)
        {
            try
            {
                _templateVersionRepository.DeleteTemplateVersion(templateVersionId);
                return null;
            }
            catch(InvalidOperationException e)
            {
                throw new NsiArgumentException(TemplateManagementMessages.TemplateVersionInvalidId, e, Common.Enumerations.SeverityEnum.Error);
            }
        }

        public ICollection<TemplateVersionDomain> GetAll(IList<FilterCriteria> filterCriteria,
            IList<SortCriteria> sortCriteria, Paging paging)
        {
            paging.ValidatePagingCriteria();
            return _templateVersionRepository.filter(filterCriteria, sortCriteria, paging);
        }

        public ICollection<TemplateVersionDomain> GetAllByTemplateId(int templateId, Paging paging)
        {
            if (templateId <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);
            paging.ValidatePagingCriteria();
            IList<FilterCriteria> filterList = new List<FilterCriteria>()
            {
                new FilterCriteria{ColumnName = "template", FilterTerm = templateId.ToString()}
            };
            return _templateVersionRepository.filter(filterList, null, paging);
        }

        public TemplateVersionDomain GetByVersionId(int templateVersionId)
        {
            try
            {
                if (templateVersionId <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);
                IList<FilterCriteria> filterList = new List<FilterCriteria>()
                {
                    new FilterCriteria{ColumnName = "id", FilterTerm = templateVersionId.ToString()}
                };
                return _templateVersionRepository.filter(filterList, null, null).Single();
            }
            catch(InvalidOperationException e)
            {
                throw new NsiArgumentException(TemplateManagementMessages.TemplateVersionInvalidId, e, Common.Enumerations.SeverityEnum.Error);
            }

        }

        public TemplateVersionDomain GetDefaultByTemplateId(int templateId)
        {
            try
            {
                IList<FilterCriteria> filterList = new List<FilterCriteria>()
                {
                    new FilterCriteria{ColumnName = "isDefault", FilterTerm = true.ToString()},
                    new FilterCriteria{ColumnName = "template", FilterTerm = templateId.ToString()}
                };
                return _templateVersionRepository.filter(filterList, null, null).Single();
            }
            catch (InvalidOperationException e)
            {
                throw new NsiArgumentException(TemplateManagementMessages.MultipleDefaultTemplateVersions, e, Common.Enumerations.SeverityEnum.Error);
            }

        }
    }
}
