using System.Net.Mail;
using System;

namespace RentAPI.Helper
{
    public static class HelperClass
    {
        public static void PosaljiMail(string email)
        {

            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential("automobili2139@gmail.com", "kgevyjbdlrvrqdmj");

            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress("automobili2139@gmail.com");

            mailMessage.To.Add(new MailAddress(email));

            string path = "EmailTemplate/EmailNaRezervaciji.html"; 
            string body = System.IO.File.ReadAllText(path);

            mailMessage.Body = body;
            //mailMessage.Body = "<html><body> " +
            //    "<h3>Test Body</h3>" +
            //    "<a href=" + "facebook.com" + ">Tekst</a>" +
            //    " </body></html>";
            mailMessage.Subject = "REZERVACIJA AUTOMOBILA. " + DateTime.Now;
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            client.SendMailAsync(mailMessage);

        }
    }
}
