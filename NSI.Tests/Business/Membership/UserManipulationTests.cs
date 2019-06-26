using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Extensions;
using Nsi.TestsCore.Mocks;
using NSI.BusinessLogic.Membership;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Membership
{
    [TestClass]
    public class UserManipulationTests
    {

        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ILanguageRepository> _languageRepositoryMock;
        private UserManipulation _userManipulation;

        [TestInitialize]
        public void Initialize()
        {
            _userRepositoryMock = Nsi.TestsCore.Mocks.Notifications.UserRepositoryMock.GetUserRepositoryMock();
            _languageRepositoryMock = LanguageRepositoryMock.GetLanguageRepositoryMock();
            _userManipulation = new UserManipulation(_userRepositoryMock.Object, _languageRepositoryMock.Object);
        }


        [TestMethod, TestCategory("User - Get User By Id")]
        public void GetUserById_Success()
        {
            UserDomain module = _userManipulation.GetUserById(1);
            Assert.AreEqual(1, module.Id);
        }

        [TestMethod, TestCategory("User - Get User By Id")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided user ID is not valid.", SeverityEnum.Warning)]
        public void GetUserById_Fail_InvalidId()
        {
            _userManipulation.GetUserById(-1);
        }

        [TestMethod, TestCategory("User - Get All Users")]
        public void GetAllUsers_Success()
        {
            _userManipulation.GetAllUsers(6, null);
        }

        [TestMethod, TestCategory("User - Add User")]
        public void AddUser_Success()
        {
            int RoleId = _userManipulation.AddUser(GetValidUserDomain());
            Assert.AreEqual(1, RoleId);
        }

        [TestMethod, TestCategory("User - Add User")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User details are not provided. ", SeverityEnum.Warning)]
        public void AddUser_Fail_InvalidModule()
        {
            _userManipulation.AddUser(null);
        }

        [TestMethod, TestCategory("User - Add User")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User name is not provided.", SeverityEnum.Warning)]
        public void AddUser_Fail_NameNotProvided()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 1,
                Id = 3
            };
            _userManipulation.AddUser(user);
        }

        [TestMethod, TestCategory("User - Add User")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User last name is not provided.", SeverityEnum.Warning)]
        public void AddUser_Fail_LastNameNotProvided()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "Haso",
                LastName = "",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 1,
                Id = 3
            };
            _userManipulation.AddUser(user);
        }

        [TestMethod, TestCategory("User - Add User")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "User with email already exist.", SeverityEnum.Warning)]
        public void AddUser_Fail_EmailAlreadyExist()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "Haso",
                LastName = "Haskic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "mujke@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 1,
                Id = 3
            };
            _userManipulation.AddUser(user);
        }

        [TestMethod, TestCategory("User - Add User")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "Language with provided ID does not exist.", SeverityEnum.Warning)]
        public void AddUser_Fail_LanguageIDNotExist()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "Haso",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 100,
                Id = 3
            };
            _userManipulation.AddUser(user);
        }

        [TestMethod, TestCategory("User - Add User")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "User email can only have 240 characters maximum.", SeverityEnum.Warning)]
        public void AddUser_Fail_EmailLengthExceeded()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "Haso",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 100,
                Id = 3
            };
            _userManipulation.AddUser(user);
        }

        [TestMethod, TestCategory("User - Add User")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "User full name can only have 220 characters maximum.", SeverityEnum.Warning)]
        public void AddUser_Fail_UserFullNameLengthExceeded()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "haske1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 100,
                Id = 3
            };
            _userManipulation.AddUser(user);
        }

        [TestMethod, TestCategory("User - Update User")]
        public void UpdateUser_Success()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "Haso",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 1,
                Id = 1
            };
            _userManipulation.UpdateUser(user);
        }

        [TestMethod, TestCategory("User - Update User")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "Provided user ID is not valid.", SeverityEnum.Warning)]
        public void UpdateUser_Fail_ProvidedUserIdNotValid()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "Haso",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 1,
                Id = -1
            };
            _userManipulation.UpdateUser(user);
        }

        [TestMethod, TestCategory("User - Update User")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User with provided ID does not exist.", SeverityEnum.Warning)]
        public void UpdateUser_Fail_ProvidedUserIDNotExist()
        {
            UserDomain user = new UserDomain()
            {
                FirstName = "Haso",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 1,
                Id = 10
            };
            _userManipulation.UpdateUser(user);
        }

        [TestMethod, TestCategory("User - Get User By Email")]
        public void GetUserByEmail_Success()
        {
            UserDomain module = _userManipulation.GetUserByEmail("mujke@hotmail.com");
            Assert.AreEqual("mujke@hotmail.com", module.Email);
        }

        [TestMethod, TestCategory("User - Get User By Email")]
        [ExtendedExpectedException(typeof(NsiArgumentNullException), "User email is not valid.", SeverityEnum.Warning)]
        public void GetUserByEmail_Fail_EmailNotProvided()
        {
            _userManipulation.GetUserByEmail("");
        }

        [TestMethod, TestCategory("User - Get User By Email")]
        [ExtendedExpectedException(typeof(NsiArgumentException), "User email can only have 240 characters maximum.", SeverityEnum.Warning)]
        public void GetUserByEmail_Fail_EmailLenghtExceeded()
        {
            _userManipulation.GetUserByEmail("haske1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
        }




        private UserDomain GetValidUserDomain()
        {
            return new UserDomain()
            {
                FirstName = "Haso",
                LastName = "Hasic",
                MiddleName = "Haske",
                TimeZoneId = "0",
                Email = "haske@hotmail.com",
                IsActive = false,
                IsDeleted = false,
                LanguageId = 1,
                Id = 3
            };
        }

    }
}
