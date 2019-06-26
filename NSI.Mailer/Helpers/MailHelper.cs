using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Mailer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NSI.Mailer.Helpers
{
    /// <summary>
    /// E-mail relatedHelper class
    /// </summary>
    internal static class MailHelper
    {
        /// <summary>
        /// Determines whether an email address is valid.
        /// </summary>
        /// <param name="emailAddress">The email address to validate.</param>
        public static void ValidateEmailAddress(string emailAddress)
        {
            // An empty or null string is not valid
            if (String.IsNullOrEmpty(emailAddress))
            {
                throw new NsiArgumentNullException(MailerMessages.EmailAddressNotProvided, SeverityEnum.Warning);
            }

            // Regular expression to match valid email address
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            // Match the email address using a regular expression
            Regex re = new Regex(emailRegex);

            if (!re.IsMatch(emailAddress))
            {
                throw new NsiArgumentException(string.Format(MailerMessages.InvalidEmailAddress, $"Email {emailAddress}"), SeverityEnum.Warning);
            }
        }

        public static void ValidateMailParameters(IEnumerable<string> toEmails, string subject, string body, string fromEmail, IEnumerable<string> ccEmails, IEnumerable<string> bccEmails)
        {
            // Validate that their is at least one receipient and that the email addresses are valid
            if (toEmails == null || !toEmails.Any())
            {
                throw new NsiArgumentNullException(MailerMessages.RecipientNotProvided, SeverityEnum.Warning);
            }

            toEmails.ToList().ForEach(ValidateEmailAddress);

            // Validate email subject
            if (string.IsNullOrEmpty(subject))
            {
                throw new NsiArgumentNullException(MailerMessages.SubjectNotProvided, SeverityEnum.Warning);
            }

            // Validate sender email. If not provided, email from config will be used.
            if (!string.IsNullOrEmpty(fromEmail))
            {
                ValidateEmailAddress(fromEmail);
            }

            // Validate the body of email
            if (string.IsNullOrEmpty(body))
            {
                throw new NsiArgumentNullException(MailerMessages.EmailBodyNotProvided, SeverityEnum.Warning);
            }

            // Validate CC emails, if there are any.
            if (ccEmails != null && ccEmails.Any())
            {
                ccEmails.ToList().ForEach(ValidateEmailAddress);
            }

            // Validate BCC emails, if there are any.
            if (bccEmails != null && bccEmails.Any())
            {
                bccEmails.ToList().ForEach(ValidateEmailAddress);
            }
        }

    }
}
