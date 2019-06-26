using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.BusinessLogic.Interfaces.Membership
{
    public interface IRoleMemberManipulation
    {
        ICollection<RoleMemberDomain> GetAllRoleMembers();
        RoleMemberDomain GetRoleMemberById(int roleMemberId);
        int AddRoleMember(RoleMemberDomain roleMember);
        void UpdateRoleMember(RoleMemberDomain roleMember);
        void DeteleRoleMemberByUserId(int userId);
    }
}
