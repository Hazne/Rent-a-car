using System.Net.Mail;
using System;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.Extensions.Configuration;

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
            mailMessage.Subject = "REZERVACIJA AUTOMOBILA. " + DateTime.Now;
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            client.SendMailAsync(mailMessage);

        }

        public static void PosaljiMailNoviPassword(string email,string sifra)
        {

            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential("automobili2139@gmail.com", "kgevyjbdlrvrqdmj");

            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress("automobili2139@gmail.com");

            mailMessage.To.Add(new MailAddress(email));


            mailMessage.Body =
                "<html><body>" +
                "<h1>Vasa nova sifra je.</h1><br>" +
                "<h1 style:"+ "font-size:30px; font-weight: bold; " + ">" + sifra + "</h1>" +
                "</body></html>";
            mailMessage.Subject = "NOVA SIFRA. " + DateTime.Now;
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            client.SendMailAsync(mailMessage);

        }

        public static void PosaljiMailTwoWayAuth(string email, string sifra)
        {

            var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential("automobili2139@gmail.com", "kgevyjbdlrvrqdmj");

            var mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.From = new System.Net.Mail.MailAddress("automobili2139@gmail.com");

            mailMessage.To.Add(new MailAddress(email));


            mailMessage.Body =
                "<html><body>" +
                "<h1>Vasa code za 2-way auth je.</h1><br>" +
                "<h1 style:" + "font-size:30px; font-weight: bold; " + ">" + sifra + "</h1>" +
                "</body></html>";
            mailMessage.Subject = "2-way auth potvrda. " + DateTime.Now;
            mailMessage.IsBodyHtml = true;

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            client.SendMailAsync(mailMessage);

        }

        public static string NovaSifraGenerator()
        {
            Random rand= new Random();
            StringBuilder sb=new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                char c = (char)rand.Next('0','z' + 1);
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
                else
                {
                    i--;
                }
            }

            string novaSifra= sb.ToString();

            return novaSifra;

        }

        
    }
}
