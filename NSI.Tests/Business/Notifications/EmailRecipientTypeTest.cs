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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Notifications
{
    [TestClass]
    public class EmailRecipientTypeTest
    {

        private Mock<IEmailRecipientTypeRepository> _emailRecipientTypeRepositoryMock;
        private EmailRecipientTypeManipulation _emailRecipientTypeManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _emailRecipientTypeRepositoryMock = EmailRecipientTypeRepositoryMock.GetEmailRecipientTypeRepositoryMock();
            _emailRecipientTypeManipulation = new EmailRecipientTypeManipulation(_emailRecipientTypeRepositoryMock.Object);
        }

        /*
         * 
         * ADD EMAIL RECIPIENT TYPE TESTS
         * 
         */
        [TestMethod, TestCategory("EmailRecipientType - AddEmailRecipientType")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Email recipient type details are not provided.", SeverityEnum.Warning)]
        public void AddEmailRecipientType_Fail_ParameterNull()
        {
            _emailRecipientTypeManipulation.AddEmailRecipientType(null);
        }

        [TestMethod, TestCategory("EmailRecipientType - AddEmailRecipientType")]
        public void AddEmailRecipientType_Success()
        {
            var emailRecipientType = GetValidEmailRecipientType();
            _emailRecipientTypeManipulation.AddEmailRecipientType(emailRecipientType);
        }

        [TestMethod, TestCategory("EmailRecipientType - AddEmailRecipientType")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Email recipient type with provided code already exists.", SeverityEnum.Warning)]
        public void AddEmailRecipientType_Exists()
        {
            var emailRecipientType = GetExistsEmailRecipientType();
            _emailRecipientTypeManipulation.AddEmailRecipientType(emailRecipientType);
        }

        /*
         * 
         * GET EMAIL RECIPIENT TYPE TEST METHODS
         * 
         */
        [TestMethod, TestCategory("EmailRecipientType - GetEmailRecipientType")]
        public void GetEmailRecipientTypeById_Success()
        {
            _emailRecipientTypeManipulation.GetEmailRecipientTypeById(1);
        }

        [TestMethod, TestCategory("EmailRecipientType - GetEmailRecipientType")]
        public void GetAllEmailRecipientTypes_Success()
        {
            var emailRecipientTypes = _emailRecipientTypeManipulation.GetAllEmailRecipientTypes();
        }

        [TestMethod, TestCategory("EmailRecipientType - GetEmailRecipientType")]
        public void GetAllEmailRecipientTypeByCode_Success()
        {
            var emailRecipientTypes = _emailRecipientTypeManipulation.GetEmailRecipientTypeByCode("code1");
        }

        [TestMethod, TestCategory("EmailRecipientType - GetEmailRecipientType")]
        public void GetAllEmailRecipientTypeByCode_Fail()
        {
            var emailRecipientTypes = _emailRecipientTypeManipulation.GetEmailRecipientTypeByCode("code2");
        }

        [TestMethod, TestCategory("EmailRecipientType - GetEmailRecipientType")]
        public void GetEmailRecipientTypeById_Fail()
        {
            _emailRecipientTypeManipulation.GetEmailRecipientTypeById(888);
        }

        [TestMethod, TestCategory("EmailRecipientType - GetEmailRecipientType")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided email recipient type ID not valid.", SeverityEnum.Warning)]
        public void GetEmailRecipientTypeById_Fail_Negative()
        {
            _emailRecipientTypeManipulation.GetEmailRecipientTypeById(-5);
        }

        /*
        * 
        * DELETE EMAIL RECIPIENT TYPE TEST METHODS
        * 
        */
        [TestMethod, TestCategory("EmailRecipientType - DeleteEmailRecipientType")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Email recipient type with provided ID does not exist.", SeverityEnum.Error)]
        public void DeleteEmailMessage_Fail_InvalidId()
        {
            _emailRecipientTypeManipulation.DeleteEmailRecipientType(555);
        }

        [TestMethod, TestCategory("EmailRecipientType - DeleteEmailRecipientType")]
        public void DeleteEmailRecipientType_Success()
        {
            _emailRecipientTypeManipulation.DeleteEmailRecipientType(1);
        }

        /*
        * 
        * UPDATE EMAIL RECIPIENT TYPE TEST METHODS
        * 
        */

        [TestMethod, TestCategory("EmailRecipientType - UpdateEmailRecipientType")]
        public void UpdateEmailRecipientType_Success()
        {
            var EmailRecipientType = _emailRecipientTypeManipulation.GetEmailRecipientTypeById(1);
            EmailRecipientType.Code = "code2";
            _emailRecipientTypeManipulation.UpdateEmailRecipientType(EmailRecipientType);
        }

        [TestMethod, TestCategory("EmailRecipientType - UpdateEmailRecipientType")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Email recipient type with provided ID does not exist.", SeverityEnum.Error)]
        public void UpdateEmailRecipientType_Fail_ArgumentNull()
        {
            _emailRecipientTypeManipulation.UpdateEmailRecipientType(null);
        }

        private EmailRecipientTypeDomain GetValidEmailRecipientType()
        {
            return new EmailRecipientTypeDomain()
            {
                Name = "Name",
                Id = 1,
                Code = "code5"
            };
        }

        private EmailRecipientTypeDomain GetExistsEmailRecipientType()
        {
            return new EmailRecipientTypeDomain()
            {
                Name = "Name",
                Id = 1,
                Code = "code1"
            };
        }

        

    }

}
