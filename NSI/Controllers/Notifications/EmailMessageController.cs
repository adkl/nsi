using NSI.BusinessLogic.Interfaces.Notifications;
using System.Web.Http;
using System.Web.Http.Description;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notifications.EmailMessage;
using NSI.Resources.Notifications;
using NSI.Domain.Notifications;
using NSI.Mailer.Implementations;
using NSI.Common.Exceptions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using NSI.Api.Authorization;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Exposes API methods for manipulating email messages
    /// </summary>
    //Uncomment for authorization
    [NsiAuthorization]
    public class EmailMessageController : ApiController
    {
        private readonly IEmailMessageManipulation _emailMessageManipulation;
       
        /// <summary>
        /// Email message controller constructor
        /// </summary>
        /// <param name="emailMessageManipulation">Instance of <see cref="IEmailMessageManipulation"/></param>
        public EmailMessageController(IEmailMessageManipulation emailMessageManipulation)
        {
            _emailMessageManipulation = emailMessageManipulation;
        }
       
        /// <summary>
        /// Retrieves all email messages
        /// </summary>
        /// <param name="request"><see cref="GetAllEmailMessagesRequest"/></param>
        /// <returns><see cref="GetAllEmailMessagesResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllEmailMessagesResponse))]
        public IHttpActionResult GetAll(GetAllEmailMessagesRequest request)
        {
            var data = _emailMessageManipulation.GetAllEmailMessages();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllEmailMessagesResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves a single email message by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="GetEmailMessageRequest"/></param>
        /// <returns><see cref="GetEmailMessageResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetEmailMessageResponse))]
        public IHttpActionResult GetById(int id)
        {
            if (id < 1)
                return BadRequest(NotificationMessages.EmailMessageIdInvalid);

            var data = _emailMessageManipulation.GetEmailMessageById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetEmailMessageResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        
        /// <summary>
        /// Searches email messages. If no parameters have been provided in request, returns all email messages.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(SearchEmailMessageResponse))]
        public IHttpActionResult Search(SearchEmailMessageRequest request)
        {
            request.ValidateNotNull();
            return Ok(new SearchEmailMessageResponse()
            {
                Data = _emailMessageManipulation.SearchEmailMessages(request.Paging, request.FilterCriteria, request.SortCriteria),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
        
        /// <summary>
        /// Adds new email message
        /// </summary>
        /// <param name="request"><see cref="AddEmailMessageRequest"/></param>
        /// <returns><see cref="AddEmailMessageResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddEmailMessageResponse))]
        public IHttpActionResult Add(AddEmailMessageRequest request)
        {
            request.ValidateNotNull();

            // convert from request model to domain model
            var emailMessageDomain = new EmailMessageDomain()
            {
                From = request.From,
                NotificationId = request.NotificationId
            };

            return Ok(new AddEmailMessageResponse()
            {
                Data = _emailMessageManipulation.AddEmailMessage(emailMessageDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Deletes email message
        /// </summary>
        /// <param name="id"><see cref="DeleteEmailMessageRequest"/></param>
        /// <returns><see cref="DeleteEmailMessageResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteEmailMessageResponse))]
        public IHttpActionResult DeleteById(int id)
        {
            if (id < 1)
                return BadRequest(NotificationMessages.EmailMessageIdInvalid);

            _emailMessageManipulation.DeleteEmailMessageById(id);

            return Ok(new DeleteEmailMessageResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }


        /// <summary>
        /// Sends email message with SMTP, body: {}
        /// </summary>
        /// <param name="request"><see cref="SendEmailRequest"/></param>
        /// <returns><see cref="SendEmailResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SendEmailResponse))]
        public IHttpActionResult SendMailSMTP(SendEmailRequest request) {
            try
            {
                request.ValidateNotNull();

                SmtpMailer sendMailSMTP = new SmtpMailer();
                sendMailSMTP.SendMail(request.ToEmails, request.Subject, request.Body, null,
                                      request.CcEmails, request.BccEmails, request.Attachments);

                return Ok(new SendEmailResponse()
                {
                    Message = NotificationMessages.EmailMessageSent,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch(NsiBaseException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Sends email message with SendGrid, body: {}
        /// </summary>
        /// <param name="request"><see cref="SendEmailRequest"/></param>
        /// <returns><see cref="SendEmailResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(SendEmailResponse))]
        public IHttpActionResult SendMailSendGrid(SendEmailRequest request) {
            try {
                request.ValidateNotNull();

                SendGridMailer sendMailSendGrid = new SendGridMailer();
                sendMailSendGrid.SendMail(request.ToEmails, request.Subject, request.Body, null,
                                      request.CcEmails, request.BccEmails, request.Attachments);

                return Ok(new SendEmailResponse() {
                    Message = NotificationMessages.EmailMessageSent,
                    Success = Common.Enumerations.ResponseStatus.Succeeded
                });
            }
            catch (NsiBaseException e) {
                return BadRequest(e.Message);
            }
        }
    }
}

