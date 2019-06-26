using NSI.Common.Exceptions;
using NSI.SmsService.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSI.SmsService.Helpers
{
    internal static class SmsHelper
    {
        public static void IsPhoneNumberValid(string obj)
        {
            Regex _regex = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            if (!_regex.Match(obj).Success)
            {
                throw new NsiArgumentException(SmsMessages.InvalidPhoneNumber, Common.Enumerations.SeverityEnum.Warning);
            }
        }

        public static void ValidateSmsParameters(string From, IEnumerable<string> ToPhoneNumbers, string Body, string AccountSid, string AuthToken)
        {
            if(string.IsNullOrEmpty(AccountSid))
            {
                throw new NsiArgumentNullException(SmsMessages.NullOrEmptyAccountSid, Common.Enumerations.SeverityEnum.Error);
            }

            if(string.IsNullOrEmpty(AuthToken))
            {
                throw new NsiArgumentNullException(SmsMessages.NullOrEmptyAuthToken, Common.Enumerations.SeverityEnum.Error);
            }

            if(string.IsNullOrEmpty(Body))
            {
                throw new NsiArgumentNullException(SmsMessages.NullOrEmptyBody, Common.Enumerations.SeverityEnum.Warning);
            }

            if (string.IsNullOrEmpty(From))
            {
                throw new NsiArgumentNullException(SmsMessages.NullOrEmptyFromPhoneNumber, Common.Enumerations.SeverityEnum.Error);
            }

            if(ToPhoneNumbers == null)
            {
                throw new NsiArgumentNullException(SmsMessages.NullToPhoneNumbers, Common.Enumerations.SeverityEnum.Error);
            }

            SmsHelper.IsPhoneNumberValid(From);
 
            if (ToPhoneNumbers != null && ToPhoneNumbers.Any())
            {
                ToPhoneNumbers.ToList().ForEach(SmsHelper.IsPhoneNumberValid);
            }
        }
    }
}
