using NSI.BusinessLogic.Interfaces.Notifications;
using System.Web.Http;
using System.Web.Http.Description;
using NSI.DataContracts.Base;
using NSI.DataContracts.Notifications.Attachment;
using NSI.Resources.Notifications;
using NSI.Domain.Notifications;
using NSI.Api.Authorization;

namespace NSI.Api.Controllers.Notifications
{
    /// <summary>
    /// Exposes API methods for manipulating attachments
    /// </summary>
    //Uncomment for authorization
    [NsiAuthorization]
    public class AttachmentController : ApiController
    {
        private readonly IAttachmentManipulation _attachmentManipulation;

        /// <summary>
        /// Attachment controller constructor
        /// </summary>
        /// <param name="attachmentManipulation">Instance of <see cref="IAttachmentManipulation"/></param>
        public AttachmentController(IAttachmentManipulation attachmentManipulation)
        {
            _attachmentManipulation = attachmentManipulation;
        }

        /// <summary>
        /// Retrieves all attachments
        /// </summary>
        /// <param name="request"><see cref="GetAllAttachmentsRequest"/></param>
        /// <returns><see cref="GetAllAttachmentsResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAllAttachmentsResponse))]
        public IHttpActionResult GetAll(GetAllAttachmentsRequest request)
        {
            var data = _attachmentManipulation.GetAllAttachments();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAllAttachmentsResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Retrieves a single attachment by provided ID in request
        /// </summary>
        /// <param name="id"><see cref="GetAttachmentRequest"/></param>
        /// <returns><see cref="GetAttachmentResponse"/></returns>
        [HttpGet]
        [ResponseType(typeof(GetAttachmentResponse))]
        public IHttpActionResult GetById(int id)
        {
            if (id < 1)
                return BadRequest(NotificationMessages.AttachmentIdInvalid);

            var data = _attachmentManipulation.GetAttachmentById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(new GetAttachmentResponse()
            {
                Data = data,
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }

        /// <summary>
        /// Adds new attachment
        /// </summary>
        /// <param name="request"><see cref="AddAttachmentRequest"/></param>
        /// <returns><see cref="AddAttachmentResponse"/></returns>
        [HttpPost]
        [ResponseType(typeof(AddAttachmentResponse))]
        public IHttpActionResult Add(AddAttachmentRequest request)
        {
            request.ValidateNotNull();

            // convert from request model to domain model
            var attachmentDomain = new AttachmentDomain()
            {
                File = request.File
               
            };

            return Ok(new AddAttachmentResponse()
            {
                Data = _attachmentManipulation.AddAttachment(attachmentDomain),
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
          
        }

        /// <summary>
        /// Deletes attachment
        /// </summary>
        /// <param name="id"><see cref="DeleteAttachmentRequest"/></param>
        /// <returns><see cref="DeleteAttachmentResponse"/></returns>
        [HttpDelete]
        [ResponseType(typeof(DeleteAttachmentResponse))]
        public IHttpActionResult DeleteById(int id)
        {
            if (id < 1)
                return BadRequest(NotificationMessages.AttachmentIdInvalid);

            _attachmentManipulation.DeleteAttachmentById(id);

            return Ok(new DeleteAttachmentResponse()
            {
                Success = Common.Enumerations.ResponseStatus.Succeeded
            });
        }
    }
}
