using NSI.Common.Enumerations;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace NSI.Common.Exceptions
{
    [Serializable]
    public class NsiBaseException : Exception
    {
        private readonly SeverityEnum severity;

        public NsiBaseException()
        {
        }

        public NsiBaseException(string message)
            : base(message)
        {
        }

        public NsiBaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NsiBaseException(string message, SeverityEnum severity)
            : base(message)
        {
            this.severity = severity;
        }

        public NsiBaseException(string message, Exception innerException, SeverityEnum severity)
            : base(message, innerException)
        {
            this.severity = severity;
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        protected NsiBaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.severity = (SeverityEnum)info.GetValue("Severity", typeof(SeverityEnum));
        }

        public SeverityEnum Severity
        {
            get { return this.severity; }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Severity", this.Severity);

            base.GetObjectData(info, context);
        }
    }
}
