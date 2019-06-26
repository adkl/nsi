using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.Notifications;
using NSI.BusinessLogic.Notifications;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class AttachmentTest
    {
        private Mock<IAttachmentRepository> _attachmentRepositoryMock;
        private AttachmentManipulation _attachmentManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _attachmentRepositoryMock = AttachmentRepositoryMock.GetAttachmentRepositoryMock();
            _attachmentManipulation = new AttachmentManipulation(_attachmentRepositoryMock.Object);
        }

        /*
         * 
         * ADD ATTACHMENT TESTS
         * 
         */
        [TestMethod, TestCategory("Attachment - AddAttachment")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Attachment not provided.", SeverityEnum.Warning)]
        public void AddAttachment_Fail_ParameterNull()
        {
            _attachmentManipulation.AddAttachment(null);
        }

        [TestMethod, TestCategory("Attachment - AddAttachment")]
        public void AddAttachment_Success()
        {
            var attachment = GetValidAttachment();
            _attachmentManipulation.AddAttachment(attachment);
        }

        /*
         * 
         * GET ATTACHMENT TEST METHODS
         * 
         */
        [TestMethod, TestCategory("Attachment - GetAttachment")]
        public void GetAttachmentById_Success()
        {
            _attachmentManipulation.GetAttachmentById(1);
        }

        [TestMethod, TestCategory("Attachment - GetAttachment")]
        public void GetAllAttachments_Success()
        {
            var attachments = _attachmentManipulation.GetAllAttachments();
        }

        /*
        * 
        * DELETE ATTACHMENT TEST METHODS
        * 
        */
        [TestMethod, TestCategory("Attachment - DeleteAttachment")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Attachment with provided ID does not exist.", SeverityEnum.Error)]
        public void DeleteEmailMessage_Fail_InvalidId()
        {
            _attachmentManipulation.DeleteAttachmentById(555);
        }

        [TestMethod, TestCategory("Attachment - DeleteAttachment")]
        public void DeleteAttachment_Success()
        {
            _attachmentManipulation.DeleteAttachmentById(1);
        }

        private AttachmentDomain GetValidAttachment()
        {
            return new AttachmentDomain()
            {
                File = AttachmentRepositoryMock.getByteArray(),
                Id = 2,
                DateCreated = DateTime.Now
            };
        }

    }
}
