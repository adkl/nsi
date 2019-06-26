using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.DataContracts;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notifications.NotificationStatus;
using NSI.Domain.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Exposes API methods for manipulating Notification statuses
    /// </summary>
    //Uncomment for authorization
    [NsiAuthorization]
    public class NotificationStatusController : ApiController
    {
        private readonly INotificationStatusManipulation _notificationStatusManipulation;

        /// <summary>
        /// NotificationStatus controller constructor
        /// </summary>
        /// <param name="notificationStatusManipulation">Instance of <see cref="INotificationStatusManipulation"/></param>
        public NotificationStatusController(INotificationStatusManipulation notificationStatusManipulation)
        {
            _notificationStatusManipulation = notificationStatusManipulation;
        }

        /// <summary>
        /// Retrieves all NotificationStatus
        /// </summary>
        /// <param name="request"><see cref="GetAllNotificationStatusRequest"/></param>
        /// <returns><see cref="GetAllNotificationStatusResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllNotificationStatusResponse))]
        public IHttpActionResult GetAllNotificationStatus(GetAllNotificationStatusRequest request)
        {
            var data = _notificationStatusManipulation.GetAllNotificationStatuses();
            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllNotificationStatusResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves single NotificationStatus by provided code in request
        /// </summary>
        /// <param name="Code"><see cref="GetNotificationStatusByCodeRequest"/></param>
        /// <returns><see cref="GetNotificationStatusByCodeResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationStatusByCodeResponse))]
        public IHttpActionResult GetNotificationStatusByCode(string Code)
        {

            var data = _notificationStatusManipulation.GetNotificationStatusByCode(Code);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetNotificationStatusByCodeResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        /// <summary>
        /// Retrieves single NotificationStatus by provided ID in request
        /// </summary>
        /// <param name="Id"><see cref="GetNotificationStatusRequest"/></param>
        /// <returns><see cref="GetNotificationStatusResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationStatusResponse))]
        public IHttpActionResult GetNotificationStatus(int Id)
        {
            if (Id < 1)
                return BadRequest(NotificationMessages.NotificationStatusInvalidId);

            var data = _notificationStatusManipulation.GetNotificationStatusById(Id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetNotificationStatusResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Searches notification statuses. If no parameters have been provided in request, return all permissions.
        /// </summary>
        /// <param name="request"><see cref="SearchNotificationStatusRequest"/></param>
        /// <returns><see cref="SearchNotificationStatusResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SearchNotificationStatusResponse))]
        public IHttpActionResult SearchNotificationStatus(SearchNotificationStatusRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchNotificationStatusResponse()
            {
                Data = _notificationStatusManipulation.SearchNotificationStatus(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new notification status
        /// </summary>
        /// <param name="request"><see cref="AddNotificationStatusRequest"/></param>
        /// <returns><see cref="AddNotificationStatusResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddNotificationStatusResponse))]
        public IHttpActionResult AddNotificationStatus(AddNotificationStatusRequest request)
        {
            request.ValidateNotNull();

            NotificationStatusDomain notificationStatus = new NotificationStatusDomain
            {
                Name = request.Name,
                Code = request.Code
            };

            return Ok(new AddNotificationStatusResponse()
            {
                Data = _notificationStatusManipulation.AddNotificationStatus(notificationStatus),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates notification status
        /// </summary>
        /// <param name="request"><see cref="UpdateNotificationStatusRequest"/></param>
        /// <returns><see cref="UpdateNotificationStatusResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(UpdateNotificationStatusResponse))]
        public IHttpActionResult UpdateNotificationStatus(UpdateNotificationStatusRequest request)
        {
            request.ValidateNotNull();

            NotificationStatusDomain notificationStatus = _notificationStatusManipulation.GetNotificationStatusById(request.Id);

            if(notificationStatus == null)
            {
                return NotFound();
            }

            notificationStatus.Name = request.Name;
            notificationStatus.Code = request.Code;

            _notificationStatusManipulation.UpdateNotificationStatus(notificationStatus);

            return Ok(new UpdateNotificationStatusResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes notification status
        /// </summary>
        /// <param name="request"><see cref="DeleteNotificationStatusRequest"/></param>
        /// <returns><see cref="DeleteNotificationStatusResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteNotificationStatusResponse))]
        public IHttpActionResult Delete(DeleteNotificationStatusRequest request)
        {
            request.ValidateNotNull();

            _notificationStatusManipulation.DeleteNotificationStatus(request.Id);

            return Ok(new DeleteNotificationStatusResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}
