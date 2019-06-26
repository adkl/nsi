using NSI.Mailer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace NSI.Mailer.Implementations {
    public class SendGridMailer : IMailer {
        /// <summary>
        /// Sends an email message using the System.Net.Mail
        /// </summary>
        /// <param name="toEmails">Recipient email addresses</param>
        /// <param name="subject">Subject of the email message</param>
        /// <param name="body">Body of the email message(HTML)</param>
        /// <param name="fromEmail">Sender email address</param>
        /// <param name="ccEmails">Carbon copy email addresses</param>
        /// <param name="bccEmails">Blind carbon copy email addresses</param>
        public void SendMail(IEnumerable<string> toEmails, string subject, string body, string fromEmail = null, IEnumerable<string> ccEmails = null, IEnumerable<string> bccEmails = null, IEnumerable<byte[]> attachments = null) {
            // Validate parameters
            Helpers.MailHelper.ValidateMailParameters(toEmails, subject, body, fromEmail, ccEmails, bccEmails);

            #region Build the email
            MailMessage mailMessage = new MailMessage();

            if (!string.IsNullOrEmpty(fromEmail)) {
                mailMessage.From = new MailAddress(fromEmail);
            }
            else {
                mailMessage.From = new MailAddress(WebConfigurationManager.AppSettings["smtpFromEmail"]);
            }

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.Body = body;


            if (attachments != null && attachments.Any()) {
                attachments.ToList().ForEach((attachment) => {
                    MemoryStream ms = new MemoryStream(attachment);
                    mailMessage.Attachments.Add(new Attachment(ms, "See attachment..."));
                });
            }

            toEmails.ToList().ForEach(mailMessage.To.Add);

            if (ccEmails != null && ccEmails.Any()) {
                ccEmails.ToList().ForEach(mailMessage.CC.Add);
            }

            if (bccEmails != null && bccEmails.Any()) {
                bccEmails.ToList().ForEach(mailMessage.Bcc.Add);
            }

            #endregion

            // Will use mailSettings from config file, make sure that they are valid
            SmtpClient client = new SmtpClient();

            // prebaciti u web config
            client.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpClientPort"]);
            client.Host = WebConfigurationManager.AppSettings["smtpSendGridClientHost"];
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["sendGridUsername"],
                                                       WebConfigurationManager.AppSettings["sendGridApiKey"]);
            client.Send(mailMessage);
        }
        
    }
}
