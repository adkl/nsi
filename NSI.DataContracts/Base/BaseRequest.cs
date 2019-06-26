using NSI.Common.Exceptions;
using NSI.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DataContracts.Base
{
    public abstract class BaseRequest
    {
    }

    public static class BaseRequestExtension
    {
        public static void ValidateNotNull(this BaseRequest request)
        {
            if (request == null)
            {
                throw new NsiArgumentNullException(ExceptionMessages.RequestNotProvided, Common.Enumerations.SeverityEnum.Warning);
            }
        }
    }

}
