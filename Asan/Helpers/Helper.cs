using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Asan.Helpers
{
    public class Helper
    {
        public static async Task SendMessage(string messageSubject, string messageBody, string mailTo)
        {
            SmtpClient client = new SmtpClient("smtp.yandex.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("mahmud.m@itbrains.edu.az", "wvamnicldsckfuxz");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage("mahmud.m@itbrains.edu.az", mailTo);
            message.Subject = messageSubject;
            message.Body = messageBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);

        }

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }


        public enum Roles
        {
            Admin
        }
    }
}
