using NSI.DataContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Notifications.Sms
{
    public class GetSmsRequest:BaseRequest
    {
        /// <summary>
        /// Sms ID for retrieval
        /// </summary>
        public int Id { get; set; }
    }
}
