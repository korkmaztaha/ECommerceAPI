using ECommerceApi.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);

        }

        public async Task SendMailAsync(string[] toS, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;

            // Alıcı adreslerini kontrol ederek ekle
            foreach (var to in toS)
            {
                if (!string.IsNullOrWhiteSpace(to) && to.Contains("@") && to.Contains("."))
                    mail.To.Add(to);
                else
                    throw new ArgumentException($"Geçersiz e-posta adresi: {to}");
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress("noreply@ecommerceapi.com", "EcommerceApi", Encoding.UTF8); 

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-posta gönderim hatası: {ex.Message}");
                throw;
            }
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {

            StringBuilder mail = new();
            mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
         
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span>");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());

        }
    }
}

