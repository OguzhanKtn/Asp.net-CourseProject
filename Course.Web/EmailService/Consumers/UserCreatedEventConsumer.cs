using EmailService.Model;
using MassTransit;
using Shared.Events;
using System.Net;
using System.Net.Mail;
namespace EmailService.Consumers
{
    public class UserCreatedEventConsumer(IConfiguration _configuration) : IConsumer<UserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            var smptClient = new SmtpClient(smtpSettings.Host)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Port = smtpSettings.Port,
                Credentials = new NetworkCredential(smtpSettings.Email, smtpSettings.Password),
                EnableSsl = smtpSettings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings.Email),
                Subject = "Course.Web",
                Body = $"<b>Kaydınız başarıyla tamamlanmıştır. Yeni üyelere özel tek kullanımlık indirim kodunuz: {context.Message.DiscountCode}",
                IsBodyHtml = true
            };

            mailMessage.To.Add(context.Message.Email);

            await smptClient.SendMailAsync(mailMessage);

            Console.WriteLine($"Email gönderildi: {context.Message.Email}");
        }
    }

}
