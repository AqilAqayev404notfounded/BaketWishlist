using System.Net.Mail;

namespace DataAccessLayer
{
    public class SendMailService
    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.From = new MailAddress(from);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("email@example.com", "uygulama_sifresi"),
                    EnableSsl = true
                };

                smtp.Send(mail);
                return true;
            }
            catch (Exception)
            {
                // Hata varsa false döndür
                return false;
            }
        }
    }
}
