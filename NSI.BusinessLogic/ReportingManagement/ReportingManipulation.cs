using NSI.BusinessLogic.Interfaces.ReportingManagement;
using NSI.Domain.ReportingManagement;
using NSI.Repository.Interfaces.ReportingManagement;
using NSI.Repository.ReportingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.ReportingManagement
{
    public class ReportingManipulation : IReportingManipulation
    {
        private readonly ReportingRepository reportingRepository = new ReportingRepository();
        private IReportingRepository @object;

        public ReportingManipulation(IReportingRepository @object)
        {
            this.@object = @object;
        }

        public ReportingManipulation()
        {
        }

        public UsersActivityWrapper GetActiveUsers(int tenantId)
        {
            return reportingRepository.GetActiveUsers(tenantId);
        }

        public List<FrequentIncidentsWrapper> GetFrequentIncidents(int tenantId, DateTime dateFrom, DateTime dateTo)
        {
            return reportingRepository.GetFrequentIncidents(tenantId, dateFrom, dateTo);
        }

        public List<FrequentDevicesWrapper> GetFrequentDevices()
        {
            return reportingRepository.GetFrequentDevices();
        }

        public List<LoggingDataWrapper> GetUserLoggingData(DateTime dateFrom, DateTime dateTo)
        {
            return reportingRepository.GetUserLoggingData(dateFrom, dateTo);
        }

        public SmsDataWrapper GetSmsSendingData(DateTime dateFrom, DateTime dateTo)
        {
            return reportingRepository.GetSmsSendingData(dateFrom, dateTo);
        }
    }
}
