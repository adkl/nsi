using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Exceptions;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notifications.NotificationType;
using NSI.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Exposes API methods for manipulating Notification types
    /// </summary>
    [NsiAuthorization]
    public class NotificationTypeController: ApiController
    {
        private readonly INotificationTypeManipulation _notificationTypeManipulation;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="notificationTypeManipulation"></param>
        public NotificationTypeController(INotificationTypeManipulation notificationTypeManipulation)
        {
            _notificationTypeManipulation = notificationTypeManipulation;
        }

        /// <summary>
        /// Get all notification types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllNotificationTypeResponse))]
        public IHttpActionResult GetAllNotificationTypes()
        {
            var data = _notificationTypeManipulation.GetAllNotificationTypes();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllNotificationTypeResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Get notification type with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationTypeResponse))]
        public IHttpActionResult GetNotificationType(int id)
        {
            try
            {
                var data = _notificationTypeManipulation.GetNotificationTypeById(id);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(new GetNotificationTypeResponse()
                {
                    Data = data,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch(NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get notification type by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationTypeResponse))]
        public IHttpActionResult GetNotificationTypeByName(string name)
        {
            try
            {
                var data = _notificationTypeManipulation.GetNotificationTypeByName(name);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(new GetNotificationTypeResponse()
                {
                    Data = data,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch(NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get notification type by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetNotificationTypeResponse))]
        public IHttpActionResult GetNotificationTypeByCode(string code)
        {
            try
            {
                var data = _notificationTypeManipulation.GetNotificationTypeByCode(code);

                if (data == null)
                {
                    return NotFound();
                }

                return Ok(new GetNotificationTypeResponse()
                {
                    Data = data,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch(NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add notification type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AddNotificationTypeResponse))]
        public IHttpActionResult AddNotificationType(AddNotificationTypeRequest request)
        {
            try
            {
                request.ValidateNotNull();

                NotificationTypeDomain domain = new NotificationTypeDomain();
                domain.Name = request.Name;
                domain.Code = request.Code;
                int id = _notificationTypeManipulation.AddNotificationType(domain);

                return Ok(new AddNotificationTypeResponse()
                {
                    Data = id,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch (NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update notification type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateNotificationTypeResponse))]
        public IHttpActionResult UpdateNotificationType(UpdateNotificationTypeRequest request)
        {
            try
            {
                request.ValidateNotNull();

                NotificationTypeDomain domain = _notificationTypeManipulation.GetNotificationTypeById(request.Id);
                if (domain == null)
                {
                    return NotFound();
                }

                domain.Name = request.Name;
                domain.Code = request.Code;
                _notificationTypeManipulation.UpdateNotificationType(domain);

                return Ok(new UpdateNotificationTypeResponse()
                {
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch(NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}