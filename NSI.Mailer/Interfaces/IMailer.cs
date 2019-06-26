using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Mailer.Interfaces
{
    public interface IMailer
    {
        void SendMail(IEnumerable<string> toEmails, string subject, string body, string fromEmail = null,
            IEnumerable<string> ccEmails = null, IEnumerable<string> bccEmails = null, IEnumerable<byte[]> attachments = null);
    }
}
