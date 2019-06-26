using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.Membership
{
    public interface IRoleMemberRepository
    {
        ICollection<RoleMemberDomain> GetAll();
        RoleMemberDomain GetById(int roleMemberId);
        int Add(RoleMemberDomain roleMember);
        void Update(RoleMemberDomain roleMember);
        void DeleteByUserId(int userId);
    }
}
