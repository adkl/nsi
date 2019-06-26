using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.ReportingManagement
{
    public class UsersActivityWrapper
    {
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public List<UserData> Users { get; set; }
    }
}
