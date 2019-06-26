using NSI.BusinessLogic.Interfaces.Notifications;
using System.Web.Http;
using System.Web.Http.Description;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notifications.EmailRecipient;
using NSI.Resources.Notifications;
using NSI.Domain.Notifications;
using NSI.Api.Authorization;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Exposes API methods for manipulating email recipients
    /// </summary>
    //Uncomment for authorization
    [NsiAuthorization]
    public class EmailRecipientController : ApiController
    {
        private readonly IEmailRecipientManipulation _emailRecipientManipulation;

        /// <summary>
        /// Email recipient controller constructor
        /// </summary>
        /// <param name="emailRecipientManipulation">Instance of <see cref="IEmailRecipientManipulation"/></param>
        public EmailRecipientController(IEmailRecipientManipulation emailRecipientManipulation)
        {
            _emailRecipientManipulation = emailRecipientManipulation;
        }

        /// <summary>
        /// Retrieves a single email recipient by provided ID in request
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="GetEmailRecipientResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetEmailRecipientResponse))]
        public IHttpActionResult GetEmailRecipient(int id)
        {
            if (id < 1)
                return BadRequest(NotificationMessages.EmailRecipientIdInvalid);

            var data = _emailRecipientManipulation.GetEmailRecipientById(id);

            if(data == null)
            {
                return NotFound();
            }

            return Ok(new GetEmailRecipientResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves all email recipients
        /// </summary>
        /// <param name="request"><see cref="GetAllEmailRecipientsRequest"/></param>
        /// <returns><see cref="GetAllEmailRecipientsResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllEmailRecipientsResponse))]
        public IHttpActionResult GetAllEmailRecipients(GetAllEmailRecipientsRequest request)
        {
            var data = _emailRecipientManipulation.GetAllEmailRecipients();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllEmailRecipientsResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Searches email recipients. If no parameters have been provided in request, returns all email recipients.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(SearchEmailRecipientResponse))]
        public IHttpActionResult SearchEmailRecipients(SearchEmailRecipientRequest request)
        {
            request.ValidateNotNull();

            return Ok(new SearchEmailRecipientResponse()
            {
                Data = _emailRecipientManipulation.SearchEmailRecipients(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new email recipient
        /// </summary>
        /// <param name="request"><see cref="AddEmailRecipientRequest"/></param>
        /// <returns><see cref="AddEmailRecipientResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddEmailRecipientResponse))]
        public IHttpActionResult AddEmailRecipient(AddEmailRecipientRequest request)
        {
            request.ValidateNotNull();

            // convert from request model to domain model
            var emailRecipientDomain = new EmailRecipientDomain()
            {
                EmailAddress = request.EmailAddress,
                EmaiMessagelId = request.EmaiMessagelId,
                EmailRecipientTypeId = request.EmailRecipientTypeId
            };

            return Ok(new AddEmailRecipientResponse()
            {
                Data = _emailRecipientManipulation.AddEmailRecipient(emailRecipientDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Updates email recipient
        /// </summary>
        /// <param name="request"><see cref="UpdateEmailRecipientRequest"/></param>
        /// <returns><see cref="UpdateEmailRecipientResponse"/></returns>
        [HttpPut]
        [ResponseType(typeof(UpdateEmailRecipientResponse))]
        public IHttpActionResult UpdateEmailRecipient(UpdateEmailRecipientRequest request)
        {
            var emailRecipientDomain = new EmailRecipientDomain()
            {
                Id = request.Id,
                EmailAddress = request.EmailAddress,                                
            };

            request.ValidateNotNull();
        
            _emailRecipientManipulation.UpdateEmailRecipient(emailRecipientDomain);

            return Ok(new UpdateEmailRecipientResponse()
            {
                Data = NotificationMessages.Updated,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes email recipient
        /// </summary>
        /// <param name="emailRecipientId"></param>
        /// <returns><see cref="DeleteEmailRecipientResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteEmailRecipientResponse))]
        public IHttpActionResult Delete(int emailRecipientId)
        {
            if (emailRecipientId < 1)
                return BadRequest(NotificationMessages.EmailRecipientIdInvalid);
           
            _emailRecipientManipulation.DeleteEmailRecipient(emailRecipientId);

            return Ok(new DeleteEmailRecipientResponse()
            {
                Data = NotificationMessages.Deleted,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

    }
}
