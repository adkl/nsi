using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Common.Exceptions
{
    public class NsiJsonParsingException: NsiBaseException
    {
        public NsiJsonParsingException(string message, SeverityEnum severity = SeverityEnum.Error)
    : base(message, severity)
        {

        }
        public NsiJsonParsingException(string message, Exception inner, SeverityEnum severity)
            : base(message, inner, severity)
        {

        }

    }
}
