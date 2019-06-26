using NSI.Domain.Membership;
using NSI.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class RoleMemberExtension
    {
        public static RoleMemberDomain ToDomainModel(this RoleMember obj)
        {
            return obj == null ? null : new RoleMemberDomain()
            {
                Id = obj.RoleMemberId,
                UserId = obj.UserInfoId,
                RoleId = obj.RoleId,
                IsActive = obj.IsActive,
                TenantId = obj.TenantId,
                UserTenantId = obj.UserInfo.TenantId,
                Name = obj.Role.Name
            };
        }
        public static RoleMember FromDomainModel(this RoleMember obj, RoleMemberDomain roleMemberDomain)
        {
            if (obj == null)
            {
                obj = new RoleMember();
            }

            obj.RoleMemberId = roleMemberDomain.Id;
            obj.UserInfoId = roleMemberDomain.UserId;
            obj.RoleId = roleMemberDomain.RoleId;
            obj.IsActive = roleMemberDomain.IsActive;
            obj.TenantId = roleMemberDomain.TenantId;
            return obj;
        }
    }
}
