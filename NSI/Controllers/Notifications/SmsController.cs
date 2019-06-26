using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notifications.Sms;
using NSI.Domain.Notifications;
using NSI.Resources.Notifications;
using NSI.SmsService.Implementations;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Exposes API methods for manipulating sms
    /// </summary>
    //Uncomment for authorization
    [NsiAuthorization]
    public class SmsController : ApiController
    {
        private readonly ISmsManipulation _smsManipulation;

        /// <summary>
        /// Sms controller constructor
        /// </summary>
        /// <param name="smsManipulation">Instance of <see cref="ISmsManipulation"/></param>
        public SmsController(ISmsManipulation smsManipulation)
        {
            _smsManipulation = smsManipulation;
        }

        /// <summary>
        /// Retrieves single Sms by provided ID in request
        /// </summary>
        /// <param name="Id"><see cref="GetSmsRequest"/></param>
        /// <returns><see cref="GetSmsResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetSmsResponse))]
        public IHttpActionResult GetSms(int Id)
        {
            if(Id < 1)
                return BadRequest(NotificationMessages.SmsInvalidId);

            var data = _smsManipulation.GetSmsById(Id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetSmsResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves single Sms by provided ID in request
        /// </summary>
        /// <param name="notificationId"><see cref="GetSmsByNotificationIdRequest"/></param>
        /// <returns><see cref="GetSmsByNotificationIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetSmsByNotificationIdResponse))]
        public IHttpActionResult GetSmsByNotificationId(int notificationId)
        {
            if (notificationId < 1)
                return BadRequest(NotificationMessages.SmsInvalidId);

            var data = _smsManipulation.GetByNotificationId(notificationId);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetSmsByNotificationIdResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Searches SMS. If no parameters have been provided in request, return all permissions.
        /// </summary>
        /// <param name="request"><see cref="SearchSmsRequest"/></param>
        /// <returns><see cref="SearchSmsResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchSmsResponse))]
        public IHttpActionResult SearchSms(SearchSmsRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchSmsResponse()
            {
                Data = _smsManipulation.SearchSms(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new sms
        /// </summary>
        /// <param name="request"><see cref="AddSmsRequest"/></param>
        /// <returns><see cref="AddSmsResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddSmsResponse))]
        public IHttpActionResult AddSms(AddSmsRequest request)
        {
            request.ValidateNotNull();
            SmsDomain smsDomain = new SmsDomain()
            {
                PhoneNumber = request.PhoneNumber,
                From = request.From,
                NotificationId = request.NotificationId
            };

            return Ok(new AddSmsResponse()
            {
                Data = _smsManipulation.AddSms(smsDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates sms
        /// </summary>
        /// <param name="request"><see cref="UpdateSmsRequest"/></param>
        /// <returns><see cref="UpdateSmsResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(UpdateSmsResponse))]
        public IHttpActionResult UpdateSms(UpdateSmsRequest request)
        {
            request.ValidateNotNull();
            SmsDomain smsDomain = _smsManipulation.GetSmsById(request.Id);

            if(smsDomain == null)
            {
                return NotFound();
            }

            smsDomain.PhoneNumber = request.PhoneNumber;
            smsDomain.From = request.From;
            smsDomain.NotificationId = request.NotificationId;
            _smsManipulation.UpdateSms(smsDomain);

            return Ok(new UpdateSmsResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes sms
        /// </summary>
        /// <param name="request"><see cref="DeleteSmsRequest"/></param>
        /// <returns><see cref="DeleteSmsResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteSmsResponse))]
        public IHttpActionResult Delete(DeleteSmsRequest request)
        {
            request.ValidateNotNull();

            _smsManipulation.DeleteSms(request.Id);

            return Ok(new DeleteSmsResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Sends sms
        /// </summary>
        /// <param name="request"><see cref="SendSmsRequest"/></param>
        /// <returns><see cref="SendSmsResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SendSmsResponse))]
        public IHttpActionResult SendTestSms(SendSmsRequest request)
        {
            TwilioSmsService twilioSms = new TwilioSmsService();
            List<String> recipientNumbers = new List<string>();
           
            recipientNumbers.Add(request.To);
            IEnumerable<String> resp = twilioSms.SendSms(request.From, recipientNumbers, request.MessageBody, System.Configuration.ConfigurationManager.AppSettings["smsAccountSid"], System.Configuration.ConfigurationManager.AppSettings["smsAuth"]);

            return Ok(new SendSmsResponse()
            {
                Data = resp,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}
