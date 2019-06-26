using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks.Notifications;
using NSI.BusinessLogic.Notifications;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using NSI.Resources.Notifications;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class EmailRecipientTest
    {
        private Mock<IEmailRecipientRepository> _emailRecipientMock;
        private Mock<IEmailRecipientTypeRepository> _emailRecipientTypeMock;
        private Mock<IEmailMessageRepository> _emailMessageMock;
        private EmailRecipientManipulation _emailRecipientManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _emailRecipientMock = EmailRecipientRepositoryMock.GetEmailRecipientRepositoryMock();
            _emailRecipientTypeMock = EmailRecipientTypeRepositoryMock.GetEmailRecipientTypeRepositoryMock();
            _emailMessageMock = EmailMessageRepositoryMock.GetEmailMessageRepositoryMock();
            _emailRecipientManipulation = new EmailRecipientManipulation(_emailRecipientMock.Object, _emailRecipientTypeMock.Object, _emailMessageMock.Object);
        }

        /*
         * 
         * Add Email Recipient tests
         * 
         */

        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email recipient details are not provided", severity: SeverityEnum.Warning)]
        public void AddEmailRecipient_Fail_ParameterNull()
        {
            _emailRecipientManipulation.AddEmailRecipient(null);
        }

        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email addres for recipient not provided", severity: SeverityEnum.Warning)]
        public void AddEmailRecipient_Fail_EmailAddress_Empty()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmailAddress = "";
            _emailRecipientManipulation.AddEmailRecipient(emailRecipient);
        }

        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Email address for recipient is not valid.", severity: SeverityEnum.Warning)]
        public void AddEmailRecipient_Fail_EmailAddress_Invalid()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmailAddress = "aaa.com";
            _emailRecipientManipulation.AddEmailRecipient(emailRecipient);
        }
      
        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided email message ID not valid.", severity: SeverityEnum.Warning)]
        public void AddEmailRecipient_Fail_EmailMessageId_Invalid()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmaiMessagelId = -1;
            _emailRecipientManipulation.AddEmailRecipient(emailRecipient);
        }

        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email message with provided ID does not exist.", severity: SeverityEnum.Warning)]
        public void AddEmailRecipient_Fail_EmailMessage_DoesNotExist()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmaiMessagelId = 44;
            _emailRecipientManipulation.AddEmailRecipient(emailRecipient);
        }

        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided email recipient type ID not valid.", severity: SeverityEnum.Warning)]
        public void AddEmailRecipient_Fail_EmailRecipientTypeId_Invalid()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmailRecipientTypeId = -1;
            _emailRecipientManipulation.AddEmailRecipient(emailRecipient);
        }

        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email recipient type with provided ID does not exist.", severity: SeverityEnum.Warning)]
        public void AddEmailRecipient_Fail_EmailRecipientType_DoesNotExist()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmailRecipientTypeId = 44;
            _emailRecipientManipulation.AddEmailRecipient(emailRecipient);
        }

        [TestMethod, TestCategory("EmailRecipient - AddEmailRecipient")]       
        public void AddEmailRecipient_Success()
        {
            var emailRecipient = GetValidEmailRecipient();            
            _emailRecipientManipulation.AddEmailRecipient(emailRecipient);
        }

        /* 
         * 
         * Get Email Recipient tests
         * 
         */

        [TestMethod, TestCategory("EmailRecipient - GetEmailRecipient")]
        public void GetAllEmailRecipients_Success()
        {
            _emailRecipientManipulation.GetAllEmailRecipients();
        }

        [TestMethod, TestCategory("EmailRecipient - GetEmailRecipient")]
        public void GetEmailRecipientById_Success()
        {
            _emailRecipientManipulation.GetEmailRecipientById(1);
        }

        [TestMethod, TestCategory("EmailRecipient - GetEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided email recipient ID is not valid.", SeverityEnum.Warning)]
        public void GetEmailRecipientById_Fail_NegativeId()
        {
            _emailRecipientManipulation.GetEmailRecipientById(-1);
        }

        /*
         * 
         * Search email recipient tests
         * 
         * 
         */

        [TestMethod, TestCategory("EmailRecipient - Search")]
        public void SearchEmailRecipient_Success_EmailNotExactMatch()
        {
            var emailRecipients = _emailRecipientManipulation.SearchEmailRecipients(EmailRecipientRepositoryMock.paging,
                                                    EmailRecipientRepositoryMock.filterCriteriaList,
                                                   EmailRecipientRepositoryMock.sortCriteriaList);

            Assert.AreEqual("test@email.com", emailRecipients.FirstOrDefault().EmailAddress);            
        }

        [TestMethod, TestCategory("EmailRecipient - Search")]
        public void SearchEmailRecipient_Success_EmailExactMatch()
        {
            EmailRecipientRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = true;
            EmailRecipientRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "test@email.com";
            var emailRecipients = _emailRecipientManipulation.SearchEmailRecipients(EmailRecipientRepositoryMock.paging,
                                                    EmailRecipientRepositoryMock.filterCriteriaList,
                                                   EmailRecipientRepositoryMock.sortCriteriaList);

            Assert.AreEqual("test@email.com", emailRecipients.FirstOrDefault().EmailAddress);
        }

        [TestMethod, TestCategory("EmailRecipient - Search")]
        public void SearchEmailRecipient_Fail_EmailExactMatch()
        {
            EmailRecipientRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = true;
            EmailRecipientRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "tset@email.com";
            var emailRecipients = _emailRecipientManipulation.SearchEmailRecipients(EmailRecipientRepositoryMock.paging,
                                                    EmailRecipientRepositoryMock.filterCriteriaList,
                                                   EmailRecipientRepositoryMock.sortCriteriaList);

            Assert.AreEqual("test@email.com", emailRecipients.FirstOrDefault().EmailAddress);
        }

        /*
         * 
         * Update email recipient tests
         * 
         */

        [TestMethod, TestCategory("EmailRecipient - UpdateEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email recipient details are not provided", severity: SeverityEnum.Warning)]
        public void UpdateEmailRecipient_Fail_ParameterNull()
        {
            _emailRecipientManipulation.UpdateEmailRecipient(null);
        }

        [TestMethod, TestCategory("EmailRecipient - UpdateEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email addres for recipient not provided", severity: SeverityEnum.Warning)]
        public void UpdateEmailRecipient_Fail_EmailAddress_Empty()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmailAddress = "";
            _emailRecipientManipulation.UpdateEmailRecipient(emailRecipient);
        }

        [TestMethod, TestCategory("EmailRecipient - UpdateEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Email address for recipient is not valid.", severity: SeverityEnum.Warning)]
        public void UpdateEmailRecipient_Fail_EmailAddress_Invalid()
        {
            var emailRecipient = GetValidEmailRecipient();
            emailRecipient.EmailAddress = "aaa.com";
            _emailRecipientManipulation.UpdateEmailRecipient(emailRecipient);
        }

        [TestMethod, TestCategory("EmailRecipient - UpdateEmailRecipient")]
        public void UpdateEmailRecipient_Success()
        {
            var emailRecipient = GetValidEmailRecipient();
            _emailRecipientManipulation.UpdateEmailRecipient(emailRecipient);
        }

        /*
         * 
         * Delete email recipient test
         * 
         */
        [TestMethod, TestCategory("EmailRecipient - DeleteEmailRecipient")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email recipient with provided ID does not exist.", SeverityEnum.Warning)]
        public void DeleteEmailRecipientById_Fail_InvalidId()
        {
            _emailRecipientManipulation.DeleteEmailRecipient(55);
        }

        [TestMethod, TestCategory("EmailRecipient - DeleteEmailRecipient")]
        
        public void DeleteEmailRecipientById_Success()
        {
            _emailRecipientManipulation.DeleteEmailRecipient(1);
        }

        private EmailRecipientDomain GetValidEmailRecipient()
        {
            return new EmailRecipientDomain()
            {
                EmailAddress = "test@email.com",
                EmaiMessagelId = 1,
                EmailRecipientTypeId = 1,
                Id = 1
            };
        }
    }
}
