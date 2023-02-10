using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Asan.Models
{
    public class Mails
    {
        public void Mail(string sendMailAdress, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            MailAddress from = new MailAddress("mahmud.m@itbrains.edu.az");
            MailAddress to = new MailAddress("mahmud.m@itbrains.edu.az");
            MailMessage msg = new MailMessage(from, to);
            msg.IsBodyHtml = true;
            msg.Subject = subject;
            msg.Body += "" + to + " | <h1> " + body + " </h1>";
            msg.CC.Add(sendMailAdress);
            NetworkCredential info = new NetworkCredential("mahmud.m@itbrains.edu.az", "wvamnicldsckfuxz");
            client.Port = 587;
            client.Host = "smtp.yandex.com";
            client.EnableSsl = true;
            client.Credentials = info;
            client.Send(msg);

        }
    }
}
