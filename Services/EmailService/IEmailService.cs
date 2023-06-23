using StartUpProjectDemo.Models.DTO;

namespace StartUpProjectDemo.Services.EmailService
{
	public interface IEmailService
	{
		Task SendEmailAsync(EmailDTO request);
	}
}
