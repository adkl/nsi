﻿using NSI.Domain.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Interfaces.DeviceManagement
{
    public interface IDeviceActionRepository
    {
        ICollection<ActionDomain> GetAllActions(int tenantId);
    }
}
