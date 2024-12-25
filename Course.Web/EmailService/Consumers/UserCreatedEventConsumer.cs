using MassTransit;
using Shared.Events;
using System.Net;
using System.Net.Mail;

namespace EmailService.Consumers
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var smptClient = new SmtpClient("smtp.gmail.com");
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential("oguzhanktn0@gmail.com", "********");
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("oguzhanktn0@gmail.com");
            mailMessage.To.Add(context.Message.Email);

            mailMessage.Subject = "Course.Web";
            mailMessage.Body = $"<b>Kaydınız başarıyla tamamlanmıştır. Yeni üyelere özel tek kullanımlık indirim kodunuz:{context.Message.DiscountCode}";

            mailMessage.IsBodyHtml = true;

            await smptClient.SendMailAsync(mailMessage);

            Console.WriteLine($"Email gönderildi: {context.Message.Email}");
        }
    }
}
