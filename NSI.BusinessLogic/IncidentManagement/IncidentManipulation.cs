using NSI.BusinessLogic.Interfaces.IncidentManagement;
using NSI.Common.Exceptions;
using NSI.Common.Models;
using NSI.Common.Resources;
using NSI.Common.Resources.IncidentManagement;
using NSI.Domain.IncidentManagement;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.IncidentManagement;
using NSI.Repository.Interfaces.Membership;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.IncidentManagement
{
    public class IncidentManipulation : IIncidentManipulation
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentSettlementRepository _incidentSettlementRepository;
        private readonly IIncidentWorkOrderRepository _incidentWorkOrderRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ITenantRepository _tenantRepository;
        public IncidentManipulation(IIncidentRepository incidentRepository, 
                                    IIncidentSettlementRepository incidentSettlementRepository,
                                    IIncidentWorkOrderRepository incidentWorkOrderRepository,
                                    INotificationRepository notificationRepository,
                                    ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
            _incidentRepository = incidentRepository;
            _incidentSettlementRepository = incidentSettlementRepository;
            _incidentWorkOrderRepository = incidentWorkOrderRepository;
            _notificationRepository = notificationRepository;
        }

        public ICollection<IncidentDomain> GetAllIncidents(int userTenantId)
        {
            if (userTenantId <= 0) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            return _incidentRepository.GetAllIncidents(userTenantId);
        }

        public ICollection<IncidentDomain> SearchIncidents(Paging paging, IList<FilterCriteria> filterCriteria, IList<SortCriteria> sortCriteria, int userTenantId)
        {
            paging.ValidatePagingCriteria();
            filterCriteria?.ToList().ForEach(x => x.ValidateFilterCriteria());
            sortCriteria?.ToList().ForEach(x => x.ValidateSortCriteria());
            return _incidentRepository.SearchIncidents(paging, filterCriteria, sortCriteria, userTenantId);
        }
        public IncidentDomain GetIncidentById(int id, int userTenantId)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            if (userTenantId <= 0) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            return _incidentRepository.GetIncidentById(id, userTenantId);
        }

        public void UpdateIncident(POSTIncidentDomain incident, int userTenantId)
        {
            if (incident == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            if(!incident.TenantId.Equals(userTenantId)) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            _incidentRepository.UpdateIncident(incident, userTenantId);
        }

        public int AddIncident(POSTIncidentDomain incident, int userTenantId)
        {
            if (incident == null) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            if (!incident.TenantId.Equals(userTenantId)) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            //Logic for already solved incidents
            //Fetch incident settlement data which has same type
            //as given incident
            IncidentSettlementDomain incidentSettlement = _incidentWorkOrderRepository.GetIncidentSettlementByTypeId(incident.IncidentType);

            if (incidentSettlement != null)
            {
                //map given incident settlement to current incident
                IncidentSettlementDomain automatedIncidentSettlement = mapIncidentSettlement(incidentSettlement, incident.TenantId);
                //save created settlement
                _incidentSettlementRepository.AddIncidentSettlement(automatedIncidentSettlement);
                //send notification
                sendNotification(automatedIncidentSettlement.Description, 
                                 automatedIncidentSettlement.TenantId);
            }
            return _incidentRepository.AddIncident(incident, userTenantId);
        }

        public void DeleteIncident(int id, int userTenantId)
        {
            if (id <= 0) throw new NsiArgumentException(ExceptionMessages.ArgumentException);

            if (userTenantId <= 0) throw new NsiArgumentException(IncidentMessages.TenantInvalidId);

            _incidentRepository.DeleteIncident(id, userTenantId);
        }

        private IncidentSettlementDomain mapIncidentSettlement(IncidentSettlementDomain incidentSettlementDomain, int tenantId)
        {
            IncidentSettlementDomain incSettlement = new IncidentSettlementDomain();
            incSettlement.FullText = incidentSettlementDomain.FullText;
            incSettlement.Description = incidentSettlementDomain.Description;
            incSettlement.IncidentStatusId = incidentSettlementDomain.IncidentStatusId;
            incSettlement.ModifiedBy = incidentSettlementDomain.ModifiedBy;
            incSettlement.DateCreated = DateTime.UtcNow;
            incSettlement.DateModified = DateTime.UtcNow;
            incSettlement.DateSettled = DateTime.UtcNow;
            incSettlement.TenantId = tenantId;

            return incSettlement;
        }
        
        private void sendNotification(String content, int tenantId)
        {
            Guid externalId = _tenantRepository.GetById(tenantId).Identifier;

            NotificationDomain notification = new NotificationDomain();
            notification.Content = content;
            notification.DateCreated = DateTime.UtcNow;
            notification.DateModified = DateTime.UtcNow;
            notification.ExternalId = externalId;
            notification.Title = "Incident resolved";
            notification.NotificationStatusId = 3;
            //type = Device Ping
            notification.NotificationTypeId = 6;

            _notificationRepository.Add(notification);
        }
    }
}

