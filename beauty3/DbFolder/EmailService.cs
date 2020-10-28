using System.Threading.Tasks;


namespace beauty3.DbFolder
{
    //public class EmailService
    //{
    //    public async Task SendEmailAsync(string email, string subject, string message)
    //    {
    //        var emailMessage = new MimeMessage();

    //        emailMessage.From.Add(new MailboxAddress("Администрация сайта", "beauty_online@mail.ru"));
    //        emailMessage.To.Add(new MailboxAddress("", email));
    //        emailMessage.Subject = subject;
    //        emailMessage.Body = new BodyBuilder()
    //        {
    //            HtmlBody = "<div style=\"color: blue; font-size: 18px;\">" + message + "</div>"
    //        }.ToMessageBody();

    //        using MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
    //        await client.ConnectAsync("smtp.mail.ru", 465, true);
    //        await client.AuthenticateAsync("beauty_online@mail.ru", "b123o123");
    //        await client.SendAsync(emailMessage);

    //        await client.DisconnectAsync(true);
    //    }
    //}
}
