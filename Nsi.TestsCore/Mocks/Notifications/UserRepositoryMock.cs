using Moq;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.Notifications
{
    public static class UserRepositoryMock
    {
        public static Mock<IUserRepository> GetUserRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var userRepository = new Mock<IUserRepository> { CallBase = false };

            userRepository.Setup(x => x.GetUserById(1)).Returns(new UserDomain
            {
                Id = 1,
                TenantId = 1
            });

            userRepository.Setup(x => x.GetUsersByTenantId(1)).Returns(
                new List<UserDomain> {
                    new UserDomain
                    {
                        Id = 1,
                        TenantId = 1
                    }
                });

            userRepository.Setup(x => x.GetUserById(1)).Returns(
                new UserDomain()
                {

                    FirstName = "Mujo",
                    LastName = "Mujic",
                    MiddleName = "Mujke",
                    TimeZoneId = "0",
                    Email = "mujke@hotmail.com",
                    IsActive = false,
                    IsDeleted = false,
                    LanguageId = 1,
                    Id = 1

                });

            userRepository.Setup(x => x.GetAll(1, null)).Returns(new List<UserDomain>()
            {
                new UserDomain()
                {
                    FirstName = "Mujo",
                    LastName = "Mujic",
                    MiddleName = "Mujke",
                    TimeZoneId = "0",
                    Email = "mujke@hotmail.com",
                    IsActive = false,
                    IsDeleted = false,
                    LanguageId = 1,
                    Id = 1
                },
                new UserDomain()
                {
                    FirstName = "Suljo",
                    LastName = "Suljic",
                    MiddleName = "Suljke",
                    TimeZoneId = "0",
                    Email = "suljke@hotmail.com",
                    IsActive = false,
                    IsDeleted = false,
                    LanguageId = 1,
                    Id = 2
                }
            });

            userRepository.Setup(x => x.GetUserByEmail("mujke@hotmail.com")).Returns(
                new UserDomain()
                {
                    FirstName = "Mujo",
                    LastName = "Mujic",
                    MiddleName = "Mujke",
                    TimeZoneId = "0",
                    Email = "mujke@hotmail.com",
                    IsActive = false,
                    IsDeleted = false,
                    LanguageId = 1

                });

            userRepository.Setup(x => x.AddUser(It.IsAny<UserDomain>())).Returns(1);

            userRepository.Setup(x => x.UpdateUser(It.IsAny<UserDomain>()));

            return userRepository;
        }
    }
}
