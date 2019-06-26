using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.SmsService.Helpers;
using NSI.SmsService.Interfaces;
using NSI.SmsService.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NSI.SmsService.Implementations
{
    /// <summary>
    /// Implementation of Twilio Sms service
    /// </summary>
    public class TwilioSmsService : ISmsService
    {
        /// <summary>
        /// Sends an sms message using Twilio REST API
        /// </summary>
        /// <param name="From">Sender phone number</param>
        /// <param name="ToPhoneNumbers">List of phone numbers of sms recipients</param>
        /// <param name="Body">Body of the sms</param>
        /// <param name="AccountSid">AccountSid for Twilio API</param>
        /// <param name="AuthToken">AuthToken for Twilio API</param>
        /// <param name="StatusCallBackUri">Optional parameter, twilio sends back status for Sms on specified URI</param>
        /// <returns>List of strings, uniquely identying each sent sms, see https://www.twilio.com/docs/sms/quickstart/csharp-dotnet-framework </returns>

        public IEnumerable<string> SendSms(string From, IEnumerable<string> ToPhoneNumbers, string Body, string AccountSid, string AuthToken, Uri StatusCallBackUri = null)
        {
            SmsHelper.ValidateSmsParameters(From, ToPhoneNumbers, Body, AccountSid, AuthToken);

            TwilioClient.Init(AccountSid, AuthToken);
            List<string> SmsIdentifierList = new List<string>();

            foreach(var ToNumber in ToPhoneNumbers)
            {
                var message = MessageResource.Create(
                  body: Body,
                  from: new Twilio.Types.PhoneNumber(From),
                  to: new Twilio.Types.PhoneNumber(ToNumber),
                  statusCallback: StatusCallBackUri
                );

                SmsIdentifierList.Add(message.Sid);
            }

            return SmsIdentifierList;

        }

    }
}
