using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Domain.ReportingManagement;

namespace NSI.Repository.Interfaces.ReportingManagement
{
    public interface IReportingRepository
    {
        UsersActivityWrapper GetActiveUsers(int tenantId);
        List<LoggingDataWrapper> GetUserLoggingData(DateTime dateFrom, DateTime dateTo);
        SmsDataWrapper GetSmsSendingData(DateTime dateFrom, DateTime dateTo);
        List<FrequentDevicesWrapper> GetFrequentDevices();
        List<FrequentIncidentsWrapper> GetFrequentIncidents(int tenantId, DateTime dateFrom, DateTime dateTo);
    }
}