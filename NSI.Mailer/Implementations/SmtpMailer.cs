using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Helpers;
using NSI.Mailer.Helpers;
using NSI.Mailer.Interfaces;
using NSI.Mailer.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;

namespace NSI.Mailer.Implementations
{
    /// <summary>
    /// Implementation of SmtpMailer
    /// </summary>
    public class SmtpMailer : IMailer
    {
        /// <summary>
        /// Sends an email message using the System.Net.Mail
        /// </summary>
        /// <param name="toEmails">Recipient email addresses</param>
        /// <param name="subject">Subject of the email message</param>
        /// <param name="body">Body of the email message(HTML)</param>
        /// <param name="fromEmail">Sender email address</param>
        /// <param name="ccEmails">Carbon copy email addresses</param>
        /// <param name="bccEmails">Blind carbon copy email addresses</param>
        public void SendMail(IEnumerable<string> toEmails, string subject, string body, string fromEmail = null, IEnumerable<string> ccEmails = null, IEnumerable<string> bccEmails = null, IEnumerable<byte[]> attachments = null)
        {
            // Validate parameters
            MailHelper.ValidateMailParameters(toEmails, subject, body, fromEmail, ccEmails, bccEmails);

            //ValidationHelper.NotNull(fromEmail, MailerMessages.FromEmailNotProvided);

            #region Build the email
            MailMessage mailMessage = new MailMessage();

            if (!string.IsNullOrEmpty(fromEmail))
            {
                mailMessage.From = new MailAddress(fromEmail);
            }
            else
            {
                mailMessage.From = new MailAddress(WebConfigurationManager.AppSettings["smtpFromEmail"]);
            }

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.Body = body;


            if (attachments != null && attachments.Any())
            {
                attachments.ToList().ForEach((attachment) => {
                    MemoryStream ms = new MemoryStream(attachment);
                    mailMessage.Attachments.Add(new Attachment(ms, "See attachment..."));
                }); 
            }

            toEmails.ToList().ForEach(mailMessage.To.Add);

            if (ccEmails != null && ccEmails.Any())
            {
                ccEmails.ToList().ForEach(mailMessage.CC.Add);
            }

            if (bccEmails != null && bccEmails.Any())
            {
                bccEmails.ToList().ForEach(mailMessage.Bcc.Add);
            }

            #endregion

            // Will use mailSettings from config file, make sure that they are valid
            SmtpClient client = new SmtpClient();

            // prebaciti u web config
            client.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpClientPort"]);
            client.Host = WebConfigurationManager.AppSettings["smtpClientHost"];
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["smtpUsername"],
                                                       WebConfigurationManager.AppSettings["smtpPassword"]);
            client.Send(mailMessage);

        }

    }
}
