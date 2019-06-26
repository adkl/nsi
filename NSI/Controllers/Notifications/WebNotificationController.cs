using NSI.Api.Authorization;
using NSI.BusinessLogic.Interfaces.Notifications;
using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notifications.WebNotification;
using NSI.Domain.Notifications;
using NSI.Resources.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Web Notifications Controller
    /// </summary>
    [NsiAuthorization]
    public class WebNotificationController: ApiController
    {
        private readonly IWebNotificationManipulation _webNotificationManipulation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webNotificationManipulation"></param>
        public WebNotificationController(IWebNotificationManipulation webNotificationManipulation)
        {
            _webNotificationManipulation = webNotificationManipulation;
        }

        /// <summary>
        /// Returns all web notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllWebNotificationResponse))]
        public IHttpActionResult GetAllWebNotifications()
        {
            var data = _webNotificationManipulation.GetAllWebNotifications();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllWebNotificationResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Returns all unseen web notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllWebNotificationResponse))]
        public IHttpActionResult GetAllUnSeenWebNotifications()
        {
            var data = _webNotificationManipulation.GetAllWebNotifications().Where(x => !x.Seen).ToList();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllWebNotificationResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Returns all web notifications addressed to a single user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllWebNotificationResponse))]
        public IHttpActionResult GetAllWebNotificationsFromUser(int userId)
        {
            var data = _webNotificationManipulation.GetAllWebNotifications()
                                                   .Where(x => x.UserInfoId == userId).ToList();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllWebNotificationResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Returns all unseen web notifications addressed to a single user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllWebNotificationResponse))]
        public IHttpActionResult GetAllUnseenWebNotificationsFromUser(int userId)
        {
            var data = _webNotificationManipulation.GetAllWebNotifications()
                                                   .Where(x => x.UserInfoId == userId && !x.Seen).ToList();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllWebNotificationResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Returns a single web notification with provided ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(GetWebNotificationResponse))]
        public IHttpActionResult GetWebNotification(int id)
        {
            var data = _webNotificationManipulation.GetWebNotificationById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetWebNotificationResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds a single web notification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(AddWebNotificationResponse))]
        public IHttpActionResult AddWebNotification(AddWebNotificationRequest request)
        {
            try
            {
                request.ValidateNotNull();

                WebNotificationDomain domain = new WebNotificationDomain();
                domain.NotificationId = request.NotificationId;
                domain.UserInfoId = request.UserInfoId;
                domain.UserTenantId = request.UserTenantId;

                int id = _webNotificationManipulation.AddWebNotification(domain);

                return Ok(new AddWebNotificationResponse()
                {
                    Data = id,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch(NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates a single unseen notification and sets the date seen to UTC now
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateWebNotificationResponse))]
        public IHttpActionResult UpdateUnseenWebNotification(UpdateWebNotificationRequest request)
        {
            try
            {
                request.ValidateNotNull();

                WebNotificationDomain domain = _webNotificationManipulation.GetWebNotificationById(request.Id);
                if (domain == null)
                {
                    return NotFound();
                }

                if (!domain.Seen)
                {
                    domain.Seen = true;
                    domain.DateSeen = DateTime.UtcNow;
                    _webNotificationManipulation.UpdateWebNotification(domain);
                }

                return Ok(new UpdateWebNotificationResponse()
                {
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch(NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates all unseen notifications and sets the date seen to UTC now
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateWebNotificationResponse))]
        public IHttpActionResult UpdateAllUnseenWebNotifications()
        {
            try
            {
                var data = _webNotificationManipulation.GetAllWebNotifications().Where(x => !x.Seen).ToList();

                if (data == null)
                {
                    return NotFound();
                }

                DateTime dateSeen = DateTime.UtcNow;

                foreach (WebNotificationDomain webNotification in data)
                {
                    webNotification.Seen = true;
                    webNotification.DateSeen = dateSeen;
                }
                _webNotificationManipulation.UpdateWebNotificationsSeen(data);

                return Ok(new UpdateWebNotificationResponse()
                {
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch (NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}