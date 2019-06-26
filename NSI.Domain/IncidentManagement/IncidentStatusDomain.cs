﻿using NSI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Domain.IncidentManagement
{
    public class IncidentStatusDomain : BaseDomain
    {
        public int IncidentStatusId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
