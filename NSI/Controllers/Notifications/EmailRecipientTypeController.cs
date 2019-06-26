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
using NSI.DataContracts.Notifications.EmailRecipientType;
using NSI.Domain.Notifications;
using NSI.Resources.Notifications;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Exposes Api methods for manipulating email recipient types
    /// </summary>

    [NsiAuthorization]
    public class EmailRecipientTypeController : ApiController
    {
        private readonly IEmailRecipientTypeManipulation _emailRecipientTypeManipulation;

        /// <summary>
        /// Email recipient type controller constructor
        /// </summary>
        /// <param name="emailRecipientTypeManipulation"></param>
        public EmailRecipientTypeController(IEmailRecipientTypeManipulation emailRecipientTypeManipulation)
        {
            _emailRecipientTypeManipulation = emailRecipientTypeManipulation;
        }

        /// <summary>
        /// Retrieves single email recipient type by provided ID in request
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="GetEmailRecipientTypeResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetEmailRecipientTypeResponse))]
        public IHttpActionResult GetEmailRecipientType(int id)
        {
            if (id < 1)
                return BadRequest(NotificationMessages.EmailRecipientTypeIdInvalid);

            var data = _emailRecipientTypeManipulation.GetEmailRecipientTypeById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetEmailRecipientTypeResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves single email recipient type by provided code in request
        /// </summary>
        /// <param name="code"></param>
        /// <returns><see cref="GetEmailRecipientTypeResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetEmailRecipientTypeResponse))]
        public IHttpActionResult GetEmailRecipientTypeByCode(string code)
        {
            var data = _emailRecipientTypeManipulation.GetEmailRecipientTypeByCode(code);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetEmailRecipientTypeResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all email recipient types
        /// </summary>
        /// <param name="request"><see cref="GetAllEmailRecipientTypesRequest"/></param>
        /// <returns><see cref="GetAllEmailRecipientTypesResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllEmailRecipientTypesResponse))]
        public IHttpActionResult GetAllEmailRecipientTypes(GetAllEmailRecipientTypesRequest request)
        {
            var data = _emailRecipientTypeManipulation.GetAllEmailRecipientTypes();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllEmailRecipientTypesResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new email recipient type
        /// </summary>
        /// <param name="request"><see cref="AddEmailRecipientTypeRequest"/></param>
        /// <returns><see cref="AddEmailRecipientTypeResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddEmailRecipientTypeResponse))]
        public IHttpActionResult AddEmailRecipientType(AddEmailRecipientTypeRequest request)
        {
            request.ValidateNotNull();

            var emailRecipientTypeDomain = new EmailRecipientTypeDomain()
            {
                Name = request.Name,
                Code = request.Code
            };

            return Ok(new AddEmailRecipientTypeResponse()
            {
                Data = _emailRecipientTypeManipulation.AddEmailRecipientType(emailRecipientTypeDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates email recipient type
        /// </summary>
        /// <param name="request"><see cref="UpdateEmailRecipientTypeRequest"/></param>
        /// <returns><see cref="UpdateEmailRecipientTypeResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateEmailRecipientTypeResponse))]
        public IHttpActionResult UpdateEmailRecipientType(UpdateEmailRecipientTypeRequest request)
        {
            request.ValidateNotNull();

            var emailRecipientTypeDomain = new EmailRecipientTypeDomain()
            {
                Id = request.Id,
                Name = request.Name                
            };

            _emailRecipientTypeManipulation.UpdateEmailRecipientType(emailRecipientTypeDomain);

            return Ok(new UpdateEmailRecipientTypeResponse()
            {
                Data = NotificationMessages.Updated,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes email recipient type
        /// </summary>
        /// <param name="emailRecipientTypeId"></param>
        /// <returns><see cref="DeleteEmailRecipientTypeResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteEmailRecipientTypeResponse))]
        public IHttpActionResult Delete(int emailRecipientTypeId)
        {
            if (emailRecipientTypeId < 1)
                return BadRequest(NotificationMessages.EmailRecipientTypeIdInvalid);

            _emailRecipientTypeManipulation.DeleteEmailRecipientType(emailRecipientTypeId);

            return Ok(new DeleteEmailRecipientTypeResponse()
            {
                Data = NotificationMessages.Deleted,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

    }
}
