using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class UserExtension
    {
        public static UserDomain ToDomainModel(this UserInfo obj)
        {
            return obj == null ? null : new UserDomain()
            {
                Id = obj.UserInfoId,
                Identifier = obj.Identifier,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                MiddleName = obj.MiddleName,
                TimeZoneId = obj.TimezoneId,
                Email = obj.Email,
                IsActive = obj.IsActive,
                LanguageId = obj.LanguageId,
                TenantId = obj.TenantId,
                RoleMember = obj.RoleMember.Select(rm => rm.ToDomainModel()).ToList()
            };
        }

        public static UserInfo FromDomainModel(this UserInfo obj, UserDomain userDomain)
        {
            if (obj == null)
            {
                obj = new UserInfo();
            }

            obj.UserInfoId = userDomain.Id;
            obj.FirstName = userDomain.FirstName;
            obj.LastName = userDomain.LastName;
            obj.MiddleName = userDomain.MiddleName;
            obj.TimezoneId = userDomain.TimeZoneId;
            obj.Email = userDomain.Email;
            obj.IsActive = userDomain.IsActive;
            obj.LanguageId = userDomain.LanguageId;
            obj.TenantId = userDomain.TenantId;
            obj.Identifier = userDomain.Identifier;

            return obj;
        }
    }
}
