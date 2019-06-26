using NSI.Common.Interfaces;
using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Common.Models;
using NSI.Domain.Membership;

namespace NSI.DataContracts.Membership.Users
{
    public class SearchUserResponse : BaseResponse<ICollection<UserDomain>>
    {
    }
}
