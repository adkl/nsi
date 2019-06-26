using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Logger.Interfaces
{
    public interface ILoggerAdapter
    {
        void LogException<T>(Exception ex, T request, SeverityEnum severity = SeverityEnum.Error);
        void LogException<T>(Exception ex, SeverityEnum severity = SeverityEnum.Error);
        void LogException<T>(NsiBaseException ex, T request);
        void LogException<T>(NsiBaseException ex);
        void LogDebug(String message);
    }
}
