using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;

namespace Nsi.TestsCore.Extensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExtendedExpectedExceptionAttribute : ExpectedExceptionBaseAttribute
    {
        private readonly Type exceptionType;
        private readonly string exceptionMessage;
        private readonly SeverityEnum exceptionSeverity;

        public ExtendedExpectedExceptionAttribute(Type exType, string exMsg)
        {
            exceptionType = exType;
            exceptionMessage = exMsg;
        }
        public ExtendedExpectedExceptionAttribute(Type exType, string exMsg, SeverityEnum severity)
        {
            exceptionType = exType;
            exceptionMessage = exMsg;
            exceptionSeverity = severity;
        }

        protected override void Verify(Exception exception)
        {
            Assert.AreEqual(exceptionType, exception.GetType());
            Assert.AreEqual(exceptionMessage, exception.Message);
            Assert.AreEqual(exceptionSeverity, ((NsiBaseException)exception).Severity);
        }
    }
}
