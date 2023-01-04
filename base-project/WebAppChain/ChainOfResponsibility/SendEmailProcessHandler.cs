using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace WebAppChain.ChainOfResponsibility
{
    public class SendEmailProcessHandler<T> : ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;

        public SendEmailProcessHandler(string fileName, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
        }

        public override object Handle(object o)
        {
            var zipMemoryStream = o as MemoryStream;
            zipMemoryStream.Position = 0;

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new NetworkCredential("serkansacma@gmail.com", "sadasdasd");

            smtpClient.EnableSsl = true;

            mailMessage.From = new MailAddress("serkansacma@gmail.com");

            mailMessage.To.Add(new MailAddress(_toEmail));

            mailMessage.Subject = "Mail Zip Deneme";
            mailMessage.Body = "<p>Html formatında güzel bir mesaj yazılabilir</p>";

            Attachment attachment = new Attachment(zipMemoryStream, _fileName,MediaTypeNames.Application.Zip);

            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);

            return base.Handle(null);
        }
    }
}
