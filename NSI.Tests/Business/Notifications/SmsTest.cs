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
    public class SmsTest
    {
        private Mock<ISmsRepository> _smsRepositoryMock;
        private Mock<INotificationRepository> _notificationRepositoryMock;
        private SmsManipulation _smsManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _smsRepositoryMock = SmsRepositoryMock.GetSmsRepositoryMock();
            _notificationRepositoryMock = NotificationRepositoryMock.GetNotificationRepositoryMock();
            _smsManipulation = new SmsManipulation(_smsRepositoryMock.Object, _notificationRepositoryMock.Object);
        }

        /*
         * 
         * ADD SMS TESTS
         * 
         */

        [TestMethod, TestCategory("Sms - AddSms")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "SMS not provided.", SeverityEnum.Warning)]
        public void AddSms_Fail_ParameterNull()
        {
            _smsManipulation.AddSms(null);
        }

        [TestMethod, TestCategory("Sms - AddSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid notification ID.", SeverityEnum.Warning)]
        public void AddSms_Fail_NegativeNotificationId()
        {
            var sms = GetValidSms();
            sms.NotificationId = -1;
            _smsManipulation.AddSms(sms);
        }

        [TestMethod, TestCategory("Sms - AddSms")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Notification with provided ID does not exists.", SeverityEnum.Warning)]
        public void AddSms_Fail_NonExistantNotificationId()
        {
            var sms = GetValidSms();
            sms.NotificationId = 5;
            _smsManipulation.AddSms(sms);
        }

        [TestMethod, TestCategory("Sms - AddSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender phone number format.", SeverityEnum.Warning)]
        public void AddSms_Fail_InvalidFromPhoneNumber()
        {
            var sms = GetValidSms();
            sms.From = "abcdef29weru904fjiw340-89";
            _smsManipulation.AddSms(sms);
        }

        [TestMethod, TestCategory("Sms - AddSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender phone number format.", SeverityEnum.Warning)]
        public void AddSms_Fail_InvalidFromPhoneNumber_1()
        {
            var sms = GetValidSms();
            sms.From = "+1234ABCED4442";
            _smsManipulation.AddSms(sms);
        }

        [TestMethod, TestCategory("Sms - AddSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid recipent phone number format.", SeverityEnum.Warning)]
        public void AddSms_Fail_InvalidRecipientPhoneNumber()
        {
            var sms = GetValidSms();
            sms.PhoneNumber = "+222223XZlaaa1";
            _smsManipulation.AddSms(sms);
        }

        [TestMethod, TestCategory("Sms - AddSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid recipent phone number format.", SeverityEnum.Warning)]
        public void AddSms_Fail_InvalidRecipientPhoneNumber_1()
        {
            var sms = GetValidSms();
            sms.PhoneNumber = "+387BA-89009099AB";
            _smsManipulation.AddSms(sms);
        }

        [TestMethod, TestCategory("Sms - AddSms")]
        public void AddSms_Success()
        {
            var sms = GetValidSms();
            _smsManipulation.AddSms(sms);
        }

        /*
         * 
         * GET SMS TEST METHODS
         * 
         */
        [TestMethod, TestCategory("Sms - GetSms")]
        public void GetSmsById_Success()
        {
            _smsManipulation.GetSmsById(1);
        }

        [TestMethod, TestCategory("Sms - GetSms")]
        public void GetSmsByNotificationId_Success()
        {
            _smsManipulation.GetByNotificationId(1);
        }

        [TestMethod, TestCategory("Sms - GetSms")]
        public void GetSmsByNotificationId_Fail_InvalidNotificationId()
        {
            var SmsByNotificationIdList =_smsManipulation.GetByNotificationId(99999);
            Assert.IsNull(SmsByNotificationIdList);
        }

        [TestMethod, TestCategory("Sms - GetSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid SMS ID.", SeverityEnum.Warning)]
        public void GetSmsById_Fail_NegativeId()
        {
            _smsManipulation.GetSmsById(-1);
        }

        [TestMethod, TestCategory("Sms - GetSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender phone number format.", SeverityEnum.Warning)]
        public void GetSmsByFrom_Fail_InvalidPhoneNumber()
        {
            _smsManipulation.GetSmsByFrom("+1a2b3c3344");
        }

        [TestMethod, TestCategory("Sms - GetSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid recipent phone number format.", SeverityEnum.Warning)]
        public void GetSmsByPhoneNumber_Fail_InvalidPhoneNumber()
        {
            _smsManipulation.GetSmsByPhoneNumber("+abc234od2267");
        }

        [TestMethod, TestCategory("Sms - GetSms")]
        public void GetSmsByPhoneNumber_Success()
        {
            var Sms = _smsManipulation.GetSmsByPhoneNumber("+38760111222");
            Assert.AreEqual("+1222333444", Sms.From);
            Assert.AreEqual("+38760111222", Sms.PhoneNumber);
        }

        [TestMethod, TestCategory("Sms - GetSms")]
        public void GetSmsByFrom_Success()
        {
            var Sms = _smsManipulation.GetSmsByFrom("+1555666777");
            Assert.IsNotNull(Sms);
        }

        /*
         * 
         * UPDATE Sms TEST METHODS
         * 
         */

        [TestMethod, TestCategory("Sms - UpdateSms")]
        public void UpdateSms_Success()
        {
            var Sms = _smsManipulation.GetSmsById(1);
            Sms.PhoneNumber = "+2333444555";
            Sms.From = "+4444999666";
            _smsManipulation.UpdateSms(Sms);
        }

        [TestMethod, TestCategory("Sms - UpdateSms")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "SMS not provided.", SeverityEnum.Warning)]
        public void UpdateSms_Fail_ArgumentNull()
        {
            _smsManipulation.UpdateSms(null);
        }

        [TestMethod, TestCategory("Sms - UpdateSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid SMS ID.", SeverityEnum.Warning)]
        public void UpdateSms_Fail_NegativeSmsId()
        {
            var Sms = _smsManipulation.GetSmsById(1);
            Sms.Id = -1;
            _smsManipulation.UpdateSms(Sms);
        }

        [TestMethod, TestCategory("Sms - UpdateSms")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "SMS with provided ID does not exists.", SeverityEnum.Warning)]
        public void UpdateSms_Fail_NonExistingeSmsId()
        {
            var Sms = _smsManipulation.GetSmsById(1);
            Sms.Id = 555;
            _smsManipulation.UpdateSms(Sms);
        }

        [TestMethod, TestCategory("Sms - UpdateSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid sender phone number format.", SeverityEnum.Warning)]
        public void UpdateSms_Fail_InvalidFromPhoneNumber()
        {
            var Sms = _smsManipulation.GetSmsById(1);
            Sms.From = "+000000sdrgjkop!";
            _smsManipulation.UpdateSms(Sms);
        }

        [TestMethod, TestCategory("Sms - UpdateSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Invalid recipent phone number format.", SeverityEnum.Warning)]
        public void UpdateSms_Fail_InvalidRecipientPhoneNumber()
        {
            var Sms = _smsManipulation.GetSmsById(1);
            Sms.PhoneNumber = "=0123h33l0w0r1d";
            _smsManipulation.UpdateSms(Sms);
        }

        /*
        * 
        * DELETE Sms TEST METHODS
        * 
        */
        [TestMethod, TestCategory("Sms - DeleteSms")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "SMS with provided ID does not exists.", SeverityEnum.Error)]
        public void DeleteSms_Fail_InvalidId()
        {
            _smsManipulation.DeleteSms(555);
        }

        [TestMethod, TestCategory("Sms - DeleteSms")]
        public void DeleteSms_Success()
        {
            _smsManipulation.DeleteSms(1);
        }

        /*
        * 
        * Search Sms TEST METHODS
        * 
        */
        [TestMethod, TestCategory("Sms - Search")]
        public void SearchSms_Success_PhoneNumberExactMatch()
        {
            var Sms = _smsManipulation.SearchSms(SmsRepositoryMock.paging, 
                                                    SmsRepositoryMock.filterCriteriaList, 
                                                   SmsRepositoryMock.sortCriteriaList);

            Assert.AreEqual("+1222333444", Sms.FirstOrDefault().From);
            Assert.AreEqual("+38760111222", Sms.FirstOrDefault().PhoneNumber);
        }

        [TestMethod, TestCategory("Sms - Search")]
        public void SearchSms_Success_PhoneNumberNonExactMatch()
        {
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = false;
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "60111222";
            var Sms = _smsManipulation.SearchSms(SmsRepositoryMock.paging,
                                                    SmsRepositoryMock.filterCriteriaList,
                                                   SmsRepositoryMock.sortCriteriaList);

            Assert.AreEqual("+1222333444", Sms.FirstOrDefault().From);
            Assert.AreEqual("+38760111222", Sms.FirstOrDefault().PhoneNumber);
        }

        [TestMethod, TestCategory("Sms - Search")]
        public void SearchSms_Success_FromExactMatch()
        {
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = true;
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "+38760111222";
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().ColumnName = "from";
            var Sms = _smsManipulation.SearchSms(SmsRepositoryMock.paging,
                                                    SmsRepositoryMock.filterCriteriaList,
                                                   SmsRepositoryMock.sortCriteriaList);

            Assert.AreEqual("+1222333444", Sms.FirstOrDefault().From);
            Assert.AreEqual("+38760111222", Sms.FirstOrDefault().PhoneNumber);
        }

        [TestMethod, TestCategory("Sms - Search")]
        public void SearchSms_Success_FromNonExactMatch()
        {
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = false;
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "60111222";
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().ColumnName = "from";
            var Sms = _smsManipulation.SearchSms(SmsRepositoryMock.paging,
                                                    SmsRepositoryMock.filterCriteriaList,
                                                   SmsRepositoryMock.sortCriteriaList);

            Assert.AreEqual("+1222333444", Sms.FirstOrDefault().From);
            Assert.AreEqual("+38760111222", Sms.FirstOrDefault().PhoneNumber);
        }

        [TestMethod, TestCategory("Sms - Search")]
        public void SearchSms_Fail_FromNonExistingNumber()
        {
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().IsExactMatch = true;
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().FilterTerm = "+60111222";
            SmsRepositoryMock.filterCriteriaList.FirstOrDefault().ColumnName = "from";
            var Sms = _smsManipulation.SearchSms(SmsRepositoryMock.paging,
                                                    SmsRepositoryMock.filterCriteriaList,
                                                   SmsRepositoryMock.sortCriteriaList);

            Assert.AreEqual("+1222333444", Sms.FirstOrDefault().From);
            Assert.AreEqual("+38760111222", Sms.FirstOrDefault().PhoneNumber);
        }

        private SmsDomain GetValidSms()
        {
            return new SmsDomain()
            {
                From = "+1555666777",
                PhoneNumber = "+38760111222",
                Id = 2,
                NotificationId = 1
            };
        }

    }
}
