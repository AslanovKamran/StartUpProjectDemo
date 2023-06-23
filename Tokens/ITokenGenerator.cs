using StartUpProjectDemo.Models.Domain;

namespace StartUpProjectDemo.Tokens
{
	public interface ITokenGenerator
	{
		string GenerateToken(User user);

	}
}
