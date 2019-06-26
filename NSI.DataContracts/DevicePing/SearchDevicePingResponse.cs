using NSI.Common.Models;
using NSI.DataContracts.Base;
using NSI.Domain.DevicePing;
using System.Collections.Generic;

namespace NSI.DataContracts.DevicePing
{
    public class SearchDevicePingResponse : BaseResponse<IEnumerable<DevicePingDomain>>
    {
        public Paging Paging { get; set; }
    }
}
