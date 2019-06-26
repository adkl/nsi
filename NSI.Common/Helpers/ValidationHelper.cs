using NSI.Common.Exceptions;
using System;
using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace NSI.Common.Helpers
{
    public static class ValidationHelper
    {
        public static void NotNull<T>(T obj, string message)
        {
            if (obj == null)
            {
                throw new NsiArgumentNullException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void NotNullOrEmpty<T>(T obj, string message) where T : ICollection
        {
            if (obj == null || obj.Count == 0)
            {
                throw new NsiArgumentNullException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void NotNullOrWhitespace(string obj, string message)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                throw new NsiArgumentNullException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void GreaterThanZero(int obj, string message)
        {
            if (obj <= 0)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void GreaterThanZero(double obj, string message)
        {
            if (obj <= 0)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void NotNegative(int obj, string message)
        {
            if (obj <= 0)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void NotNegative(decimal obj, string message)
        {
            if (obj <= 0)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void IsTrue(bool obj, string message)
        {
            if (!obj)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }
        public static void IsFalse(bool obj, string message)
        {
            if (obj)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void MaxLength(string obj, int length, string message)
        {
            if (!string.IsNullOrWhiteSpace(obj) && obj.Length > length)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }

        public static void IsEmailValid(string obj, string message)
        {
                try
                {
                    MailAddress m = new MailAddress(obj);
                }
                catch (FormatException)
                {
                    throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
                }
           
        }

        public static void IsPhoneNumberValid(string obj, string message)
        {
            Regex _regex = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if(!_regex.Match(obj).Success)
            {
                throw new NsiArgumentException(message, Enumerations.SeverityEnum.Warning);
            }
        }
    }
}
