using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using StartUpProjectDemo.Models.DTO;

namespace StartUpProjectDemo.Services.EmailService
{
	public class EmailService  : IEmailService
	{
		private readonly IConfiguration _config;
		public EmailService(IConfiguration config) => _config = config;

		public async Task SendEmailAsync(EmailDTO request)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse(_config["EmailUsername"]));
			email.To.Add(MailboxAddress.Parse(request.To));
			email.Subject = request.Subject;
			email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

			using (var smtp = new SmtpClient())
			{
				await smtp.ConnectAsync(host: _config["EmailHost"], port: 587, SecureSocketOptions.StartTls);
				await smtp.AuthenticateAsync(userName: _config["EmailUsername"], password: _config["EmailPassword"]);
				await smtp.SendAsync(email);
				await smtp.DisconnectAsync(true);
			}
		}

		
	}
}
