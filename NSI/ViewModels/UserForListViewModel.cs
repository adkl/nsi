using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSI.Api.ViewModels
{
    /// <summary>
    /// List view model for user
    /// </summary>
    public class UserForListViewModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int UserInfoId { get; set; }
        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// User roles
        /// </summary>
        public IList<Role> Roles { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public class Role
        {
            /// <summary>
            /// Role Id
            /// </summary>
            public int RoleId { get; set; }
            /// <summary>
            /// Role name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Map from DataBase RoleMember
            /// </summary>
            public static Role MapFromDbRoleObject(RoleMemberDomain rm)
            {
                return new Role
                {
                    RoleId = rm.RoleId,
                    Name = rm.Name
                };
            }
        }

        /// <summary>
        /// Map from DataBase User
        /// </summary>
        public static UserForListViewModel MapFromDbObject(UserDomain user)
        {
            return new UserForListViewModel
            {
                UserInfoId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsActive = user.IsActive,
                Roles = user.RoleMember?.Select(r => Role.MapFromDbRoleObject(r)).ToList()
            };
        }
    }
}