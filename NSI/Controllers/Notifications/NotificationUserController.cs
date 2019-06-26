using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notification;
using NSI.DataContracts.Notifications.NotificationUser;
using NSI.Domain.Notifications;
using NSI.Resources.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers.Notifications {
    /// <summary>
    /// Exposes API methods for manipulating NotificationUser table
    /// </summary>

    //Uncomment for authorization
    [NsiAuthorization]
    public class NotificationUserController : ApiController {
        private readonly INotificationUserManipulation _notificationUserManipulation;
        private readonly INotificationManipulation _notificationManipulation;

        /// <summary>
        /// NotificationUser controller constructor
        /// </summary>
        /// <param name="notificationUserManipulation">Instance of <see cref="INotificationUserManipulation"/></param>
        /// <param name="notificationManipulation">Instance of <see cref="INotificationManipulation"/></param>
        public NotificationUserController(INotificationUserManipulation notificationUserManipulation, INotificationManipulation notificationManipulation) {
            _notificationUserManipulation = notificationUserManipulation;
            _notificationManipulation = notificationManipulation;
        }

        /// <summary>
        /// Retrieves all records from NotificationUser table
        /// </summary>
        /// /// <param name="request"><see cref="GetAllNotificationUserRequest"/></param>
        /// <returns><see cref="GetAllNotificationUserResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllNotificationUserResponse))]
        public IHttpActionResult GetAll(GetAllNotificationUserRequest request) {
            var data = _notificationUserManipulation.GetAllNotificationUsers();

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetAllNotificationUserResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves a single notificationUser by provided ID in request
        /// </summary>
        /// <param name="notificationUserId"><see cref="int"/></param>
        /// <returns><see cref="GetNotificationUserByIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationUserByIdResponse))]
        public IHttpActionResult GetById(int notificationUserId) {
            if (notificationUserId < 1)
                return BadRequest(NotificationMessages.NotificationUserIdInvalid);

            var data = _notificationUserManipulation.GetNotificationUserById(notificationUserId);

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetNotificationUserByIdResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }


        /// <summary>
        /// Retrieves all notificationUsers by provided notificationID in request
        /// </summary>
        /// <param name="notificationId"><see cref="int"/></param>
        /// <returns><see cref="GetNotificationUserByNotificationIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationUserByNotificationIdResponse))]
        public IHttpActionResult GetByNotificationId(int notificationId) {
            if (notificationId < 1)
                return BadRequest(NotificationMessages.NotificationIdInvalid);

            var data = _notificationUserManipulation.GetNotificationUserByNotificationId(notificationId);

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetNotificationUserByNotificationIdResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all notificationUsers by provided userID in request
        /// </summary>
        /// <param name="userId"><see cref="int"/></param>
        /// <returns><see cref="GetNotificationUserByUserIdResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationUserByUserIdResponse))]
        public IHttpActionResult GetByUserId(int userId) {
            if (userId < 1)
                return BadRequest(NotificationMessages.UserIdInvalid);

            var data = _notificationUserManipulation.GetNotificationUserByUserId(userId);

            if (data == null) {
                return NotFound();
            }

            return Ok(new GetNotificationUserByUserIdResponse() {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }



        /// <summary>
        /// Retrieves all notifications by provided userID in request
        /// </summary>
        /// <param name="userId"><see cref="int"/></param>
        /// <returns><see cref="GetAllNotificationsResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllNotificationsResponse))]
        public IHttpActionResult GetAllNotificationsByUserId(int userId)
        {
            if (userId < 1)
                return BadRequest(NotificationMessages.UserIdInvalid);

            var data = _notificationUserManipulation.GetNotificationUserByUserId(userId);

            if (data == null)
            {
                return NotFound();
            }

            ICollection<int> listOfNotificationIds = new List<int>();

            foreach(NotificationUserDomain notificationUser in data)
            {
                listOfNotificationIds.Add(notificationUser.NotificationId);
            }

            ICollection<NotificationDomain> listOfNotifications = new List<NotificationDomain>();

            foreach(int notificationId in listOfNotificationIds)
            {
                NotificationDomain tempNotification = _notificationManipulation.GetNotificationById(notificationId);
                if (!listOfNotifications.Any(x => x.Id == tempNotification.Id)){
                    listOfNotifications.Add(tempNotification);
                }                
            }

            return Ok(new GetAllNotificationsResponse()
            {
                Data = listOfNotifications,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
           
        /// <summary>
        /// Adds new NotificationUser record
        /// </summary>
        /// <param name="request"><see cref="AddNotificationUserRequest"/></param>
        /// <returns><see cref="AddNotificationUserResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddNotificationUserResponse))]
        public IHttpActionResult Add(AddNotificationUserRequest request) {
            request.ValidateNotNull();

            // convert from request model to domain model
            var notificationUserDomain = new NotificationUserDomain() {
                NotificationId = request.NotificationId,
                UserInfoId = request.UserId,
                UserTenantId = request.TenantId
            };

            return Ok(new AddNotificationUserResponse() {
                Data = _notificationUserManipulation.AddNotificationUser(notificationUserDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates NotificationUser table record
        /// </summary>
        /// <param name="request"><see cref="UpdateNotificationUserRequest"/></param>
        /// <returns><see cref="UpdateNotificationUserResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateNotificationUserResponse))]
        public IHttpActionResult Update(UpdateNotificationUserRequest request) {
            request.ValidateNotNull();

            // convert from request model to domain model
            var notificationUserDomain = new NotificationUserDomain() {
                Id = request.Id,
                NotificationId = request.NotificationId,
                UserInfoId = request.UserId,
                UserTenantId = request.TenantId
            };

            _notificationUserManipulation.UpdateNotificationUser(notificationUserDomain);

            return Ok(new UpdateNotificationUserResponse() {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes NotificationUser record
        /// </summary>
        /// <param name="notificationUserId"><see cref="int"/></param>
        /// <returns><see cref="DeleteNotificationUserResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteNotificationUserResponse))]
        public IHttpActionResult Delete(int notificationUserId) {
            if (notificationUserId < 1)
                return BadRequest(NotificationMessages.NotificationUserIdInvalid);

            _notificationUserManipulation.DeleteNotificationUser(notificationUserId);

            return Ok(new DeleteNotificationUserResponse() {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}