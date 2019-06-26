using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notification;
using NSI.Domain.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers.Notifications {
    /// <summary>
    /// Exposes API methods for manipulating notifications
    /// </summary>

    //Uncomment for authorization
    [NsiAuthorization]
    public class NotificationController : ApiController {
        private readonly INotificationManipulation _notificationManipulation;

        /// <summary>
        /// Notification controller constructor
        /// </summary>
        /// <param name="notificationManipulation">Instance of <see cref="INotificationManipulation"/></param>
        public NotificationController(INotificationManipulation notificationManipulation) {
            _notificationManipulation = notificationManipulation;
        }

        /// <summary>
        /// Retrieves all notifications
        /// </summary>
        /// /// <param name="request"><see cref="GetAllNotificationsRequest"/></param>
        /// <returns><see cref="GetAllNotificationsResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllNotificationsResponse))]
        public IHttpActionResult GetAll(GetAllNotificationsRequest request) {
            var data = _notificationManipulation.GetAllNotifications();

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetAllNotificationsResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves a single notification by provided notification ID in request
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="GetNotificationByIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationByIdResponse))]
        public IHttpActionResult GetById(int id) {
            if (id < 1)
                return BadRequest(NotificationMessages.NotificationIdInvalid);

            var data = _notificationManipulation.GetNotificationById(id);

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetNotificationByIdResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all notifications by provided notification external ID in request
        /// </summary>
        /// <param name="guid"><see cref="Guid"/></param>
        /// <returns><see cref="GetNotificationByExternalIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationByExternalIdResponse))]
        public IHttpActionResult GetByExternalId(Guid guid) {
            if (guid.Equals(""))   {
                return BadRequest(NotificationMessages.NotificationExternalIdNotProvided);
            }

            var data = _notificationManipulation.GetNotificationByExternalId(guid);

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetNotificationByExternalIdResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all notifications by provided notification created date (YYYY-MM-DD) in request
        /// </summary>
        /// <param name="CreatedDate"><see cref="DateTime"/></param>
        /// <returns><see cref="GetNotificationByCreatedDateResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationByCreatedDateResponse))]
        public IHttpActionResult GetByCreatedDate(DateTime CreatedDate) {
            if (CreatedDate.Equals(""))
                return BadRequest(NotificationMessages.NotificationDateCreatedNotProvided);

            var data = _notificationManipulation.GetNotificationByCreatedDate(CreatedDate);

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetNotificationByCreatedDateResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }


        /// <summary>
        /// Adds new notification
        /// </summary>
        /// <param name="request"><see cref="AddNotificationRequest"/></param>
        /// <returns><see cref="AddNotificationResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddNotificationResponse))]
        public IHttpActionResult Add(AddNotificationRequest request) {
            request.ValidateNotNull();

            // convert from request model to domain model
            var notificationDomain = new NotificationDomain() {
                Title = request.Title,
                Content = request.Content,
                ExternalId = request.ExternalId,
                NotificationStatusId = request.NotificationStatusId,
                NotificationTypeId = request.NotificationTypeId,
            };

            return Ok(new AddNotificationResponse() {
                Data = _notificationManipulation.AddNotification(notificationDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates notification
        /// </summary>
        /// <param name="request"><see cref="UpdateNotificationRequest"/></param>
        /// <returns><see cref="UpdateNotificationResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateNotificationResponse))]
        public IHttpActionResult Update(UpdateNotificationRequest request) {
            request.ValidateNotNull();

            // convert from request model to domain model
            var notificationDomain = new NotificationDomain() {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content,
                ExternalId = request.ExternalId,
                NotificationStatusId = request.NotificationStatusId,
                NotificationTypeId = request.NotificationTypeId
            };

            _notificationManipulation.UpdateNotification(notificationDomain);

            return Ok(new UpdateNotificationResponse() {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes notification
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns><see cref="DeleteNotificationResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteNotificationResponse))]
        public IHttpActionResult Delete(int notificationId) {
            if (notificationId < 1)
                return BadRequest(NotificationMessages.NotificationIdInvalid);

            _notificationManipulation.DeleteNotification(notificationId);

            return Ok(new DeleteNotificationResponse() {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}