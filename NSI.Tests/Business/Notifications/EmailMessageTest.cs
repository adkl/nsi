using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.Notifications;
using NSI.BusinessLogic.Notifications;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Resources;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System.Linq;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class EmailMessageTest
    {
        private Mock<IEmailMessageRepository> _emailMessageRepositoryMock;
        private Mock<INotificationRepository> _notificationRepositoryMock;
        private EmailMessageManipulation _emailMessageManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _emailMessageRepositoryMock = EmailMessageRepositoryMock.GetEmailMessageRepositoryMock();
            _notificationRepositoryMock = NotificationRepositoryMock.GetNotificationRepositoryMock();
            _emailMessageManipulation = new EmailMessageManipulation(_emailMessageRepositoryMock.Object, _notificationRepositoryMock.Object);
        }

        /*
         * 
         * ADD EMAIL MESSAGE TESTS
         * 
         */
        [TestMethod, TestCategory("EmailMessage - AddEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email Message is not provided.", SeverityEnum.Warning)]
        public void AddEmailMessage_Fail_ParameterNull()
        {
            _emailMessageManipulation.AddEmailMessage(null);
        }

        [TestMethod, TestCategory("EmailMessage - AddEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid notification ID.", SeverityEnum.Warning)]
        public void AddEmailMessage_Fail_NegativeNotificationId()
        {
            var emailMessage = GetValidEmailMessage();
            emailMessage.NotificationId = -1;
            _emailMessageManipulation.AddEmailMessage(emailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - AddEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exist.", SeverityEnum.Warning)]
        public void AddEmailMessage_Fail_NonExistantNotificationId()
        {
            var emailMessage = GetValidEmailMessage();
            emailMessage.NotificationId = 5;
            _emailMessageManipulation.AddEmailMessage(emailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - AddEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender email format error.", SeverityEnum.Warning)]
        public void AddEmailMessage_Fail_InvalidFromEmailMessage()
        {
            var emailMessage = GetValidEmailMessage();
            emailMessage.From = "rdjfhjidkfhgkd23874";
            _emailMessageManipulation.AddEmailMessage(emailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - AddEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender email format error.", SeverityEnum.Warning)]
        public void AddEmailMessage_Fail_InvalidFromEmailMessage_1()
        {
            var emailMessage = GetValidEmailMessage();
            emailMessage.From = "edjshfjdjsf";
            _emailMessageManipulation.AddEmailMessage(emailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - AddEmailMessage")]
        public void AddEmailMessage_Success()
        {
            var emailMessage = GetValidEmailMessage();
            _emailMessageManipulation.AddEmailMessage(emailMessage);
        }

        /*
         * 
         * GET EMAIL MESSAGE TEST METHODS
         * 
         */
        [TestMethod, TestCategory("EmailMessage - GetEmailMessage")]
        public void GetEmailMessageById_Success()
        {
            _emailMessageManipulation.GetEmailMessageById(1);
        }

         [TestMethod, TestCategory("EmailMessage - GetEmailMessage")]
        public void GetEmailMessageByNotificationId_Fail_InvalidNotificationId()
        {
            var EmailMessageByNotificationIdList = _emailMessageManipulation.GetByNotificationId(99999);
            Assert.IsNull(EmailMessageByNotificationIdList);
        }

        [TestMethod, TestCategory("EmailMessage - GetEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided email message ID not valid.", SeverityEnum.Warning)]
        public void GetEmailMessageById_Fail_NegativeId()
        {
            _emailMessageManipulation.GetEmailMessageById(-1);
        }

        [TestMethod, TestCategory("EmailMessage - GetEmailMessage")]
        public void GetEmailMessageByNotificationId_Success()
        {
            _emailMessageManipulation.GetByNotificationId(1);
        }

        [TestMethod, TestCategory("EmailMessage - GetEmailMessage")]
        public void GetAllEmailMessages_Success()
        {
            var emailMessages = _emailMessageManipulation.GetAllEmailMessages();
        }

        /*
         * 
         * UPDATE EMAIL MESSAGE TEST METHODS
         * 
         */

        [TestMethod, TestCategory("EmailMessage - UpdateEmailMessage")]
        public void UpdateEmailMessage_Success()
        {
            var EmailMessage = _emailMessageManipulation.GetEmailMessageById(1);
            EmailMessage.From = "test@gmail.com";
            _emailMessageManipulation.UpdateEmailMessage(EmailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - UpdateEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email Message is not provided.", SeverityEnum.Warning)]
        public void UpdateEmailMessage_Fail_ArgumentNull()
        {
            _emailMessageManipulation.UpdateEmailMessage(null);
        }

        [TestMethod, TestCategory("EmailMessage - UpdateEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid Email Message ID.", SeverityEnum.Warning)]
        public void UpdateEmailMessage_Fail_NegativeEmailMessageId()
        {
            var EmailMessage = _emailMessageManipulation.GetEmailMessageById(1);
            EmailMessage.Id = -1;
            _emailMessageManipulation.UpdateEmailMessage(EmailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - UpdateEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email message with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateEmailMessage_Fail_NonExistingeEmailMessageId()
        {
            var EmailMessage = _emailMessageManipulation.GetEmailMessageById(1);
            EmailMessage.Id = 555;
            _emailMessageManipulation.UpdateEmailMessage(EmailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - UpdateEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender email format error.", SeverityEnum.Warning)]
        public void UpdateEmailMessage_Fail_InvalidFromEmailMessage()
        {
            var EmailMessage = _emailMessageManipulation.GetEmailMessageById(1);
            EmailMessage.From = "dhdjshjssid83ues";
            _emailMessageManipulation.UpdateEmailMessage(EmailMessage);
        }

        [TestMethod, TestCategory("EmailMessage - UpdateEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender email format error.", SeverityEnum.Warning)]
        public void UpdateEmailMessage_Fail_InvalidFromEmailMessage2()
        {
            var EmailMessage = _emailMessageManipulation.GetEmailMessageById(1);
            EmailMessage.From = "sdijdfdhd";
            _emailMessageManipulation.UpdateEmailMessage(EmailMessage);
        }

        /*
        * 
        * DELETE EMAIL MESSAGE TEST METHODS
        * 
        */
        [TestMethod, TestCategory("EmailMessage - DeleteEmailMessage")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Email message with provided ID does not exist.", SeverityEnum.Error)]
        public void DeleteEmailMessage_Fail_InvalidId()
        {
            _emailMessageManipulation.DeleteEmailMessageById(555);
        }

        [TestMethod, TestCategory("EmailMessage - DeleteEmailMessage")]
        public void DeleteEmailMessage_Success()
        {
            _emailMessageManipulation.DeleteEmailMessageById(1);
        }

        /*
        * 
        * SEARCH EMAIL MESSAGE TEST METHODS
        * 
        */
        [TestMethod, TestCategory("EmailMessage - SearchEmailMessage")]
        public void SearchEmailMessage_Success_FromExactMatch()
        {
            EmailMessageRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = true;
            var EmailMessage = _emailMessageManipulation.SearchEmailMessages(EmailMessageRepositoryMock.paging,
                                                    EmailMessageRepositoryMock.filterCriteriaList,
                                                   EmailMessageRepositoryMock.sortCriteriaList);

            Assert.AreEqual("test@gmail.com", EmailMessage.FirstOrDefault().From);
        }

        [TestMethod, TestCategory("EmailMessage - SearchEmailMessage")]
        public void SearchEmailMessage_Success_FromNonExactMatch()
        {
            EmailMessageRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = false;
            var EmailMessage = _emailMessageManipulation.SearchEmailMessages(EmailMessageRepositoryMock.paging,
                                                    EmailMessageRepositoryMock.filterCriteriaList,
                                                   EmailMessageRepositoryMock.sortCriteriaList);

            Assert.AreEqual("test@gmail.com", EmailMessage.FirstOrDefault().From);
        }

   /*     [TestMethod, TestCategory("EmailMessage - SearchEmailMessage")]
        public void SearchEmailMessage_Fail_FilterCriteriaGone()
        {
            EmailMessageRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = false;
            var EmailMessage = _emailMessageManipulation.SearchEmailMessages(EmailMessageRepositoryMock.paging,
                                                    null,
                                                   EmailMessageRepositoryMock.sortCriteriaList);

           Assert.AreEqual("test@gmail.com", EmailMessage.FirstOrDefault().From);
        }

        [TestMethod, TestCategory("EmailMessage - SearchEmailMessage")]
        public void SearchEmailMessage_Fail_SortCriteriaGone()
        {
            EmailMessageRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = false;
            var EmailMessage = _emailMessageManipulation.SearchEmailMessages(EmailMessageRepositoryMock.paging,
                                                   EmailMessageRepositoryMock.filterCriteriaList,
                                                   null);
            Assert.AreEqual("test@gmail.com", EmailMessage.FirstOrDefault().From);
        }*/

        private EmailMessageDomain GetValidEmailMessage()
        {
            return new EmailMessageDomain()
            {
                From = "test@gmail.com",
                Id = 2,
                NotificationId = 1
            };
        }

    }

}
