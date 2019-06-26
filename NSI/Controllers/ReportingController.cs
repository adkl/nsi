using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.ReportingManagement;
using NSI.BusinessLogic.ReportingManagement;
using NSI.Domain.ReportingManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NSI.Api.Controllers
{
    /// <summary>
    /// Exposes methods for Report manipulation
    /// </summary>
    [NsiAuthorization]
    public class ReportingController : ApiController
    {
        private readonly IReportingManipulation _reportingManipulation;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="reportingManipulation"></param>
        public ReportingController(IReportingManipulation reportingManipulation)
        {
            _reportingManipulation = reportingManipulation ?? throw new ArgumentNullException();
        }

        public ReportingController()
        {
            ReportingManipulation reportingManipulation = new ReportingManipulation();
            _reportingManipulation = reportingManipulation;
        }

        /// <summary>
        /// Returns a number of active users and users
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult GetActiveUsers(int tenantId)
        {
            UsersActivityWrapper activeUsers = _reportingManipulation.GetActiveUsers(tenantId);
            return Ok(activeUsers);
        }

        /// <summary>
        /// Returns frequently used devices in all tenants
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult GetFrequentDevices(int count = 0)
        {
            List<FrequentDevicesWrapper> result = _reportingManipulation.GetFrequentDevices();
            if (count == 0)
                return Ok(result);
            else if (count > 0)
                return Ok(result.Take(count));
            else
                throw new ArgumentOutOfRangeException("Invalid parameter value.");
        }

        /// <summary>
        /// Returns a list of frequent incidents in specified time
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult GetFrequentIncidents(int tenantId, DateTime dateFrom, DateTime dateTo)
        {
            List<FrequentIncidentsWrapper> result = _reportingManipulation.GetFrequentIncidents(tenantId, dateFrom, dateTo);
            return Ok(result);
        }

        /// <summary>
        /// Returns a number of user loggins in a selected period of time
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult GetUserLoggingData(DateTime dateFrom, DateTime dateTo)
        {
            List<LoggingDataWrapper> result = _reportingManipulation.GetUserLoggingData(dateFrom, dateTo);
            return Ok(result);
        }

        /// <summary>
        /// Returns a number of sms messages sent in a selected period of time
        /// </summary>
        /// <returns><see cref="IHttpActionResult"/></returns>
        public IHttpActionResult GetSmsSendingData(DateTime dateFrom, DateTime dateTo)
        {
            SmsDataWrapper result = _reportingManipulation.GetSmsSendingData(dateFrom, dateTo);
            return Ok(result);
        }
    }
}