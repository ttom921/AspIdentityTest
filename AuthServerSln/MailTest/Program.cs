using System;
using System.Net;
using System.Net.Mail;

namespace MailTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SendEmail();
        }
        static public void SendEmail()
        {
            //設定smtp主機
            string smtpAddress = "mail.gomo2o.com";
            //設定Port
            int portNumber = 25;
            bool enableSSL = false;
            //填入寄送方email和密碼
            string emailFrom = "ttom@gomo2o.com";
            string password = "vYmr903*";
            //收信方email
            string emailTo = "ttom921@hotmail.com";
            //主旨
            string subject = "Hello";
            //內容
            string body = "Hello, I'm just writing this to say Hi!";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                // 若你的內容是HTML格式，則為True
                mail.IsBodyHtml = false;

                //夾帶檔案
                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }

    }
}
