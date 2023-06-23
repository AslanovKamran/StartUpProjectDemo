using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using StartUpProjectDemo.Models.Domain;
using StartUpProjectDemo.Models.DTO;
using StartUpProjectDemo.Models.Requests;
using StartUpProjectDemo.Repository.Interfaces;
using StartUpProjectDemo.Services.EmailService;
using StartUpProjectDemo.Tokens;

namespace StartUpProjectDemo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class AuthController : ControllerBase
	{
		private readonly ITokenGenerator _tokenGenerator;
		private readonly IUserRepository _userRepository;
		private readonly IEmailService _emailService;
		public AuthController(ITokenGenerator tokenGenerator, IUserRepository userRepository, IEmailService emailService)
		{
			_tokenGenerator = tokenGenerator;
			_userRepository = userRepository;
			_emailService = emailService;
		}

		/// <summary>
		///	Log In User and Get an Access Token
		/// </summary>
		/// <param name="request">Login and Password</param>
		/// <returns></returns>

		[HttpPost]
		[Route("login")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Login([FromForm] LoginRequest request)
		{
			try
			{
				var user = await _userRepository.LogInUserAsync(request.Login, request.Password);
				if (user is not null)
				{
					//Generate an Access Token
					var accessToken = _tokenGenerator.GenerateToken(user);
					return Ok(accessToken);
				}
				return BadRequest(new { LoginFailure = "Wrong login or password" });
			}
			catch (Exception ex)
			{
				return BadRequest(new { Error = ex.Message });
			}
		}


		/// <summary>
		/// Register a New User and Get the User's Id, Login and Role
		/// </summary>
		/// <param name="userDTO">Valid Credentials</param>
		/// <returns>New User</returns>

		[HttpPost]
		[Route("register")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Register([FromForm] PostUserDTO userDTO)
		{
			if (ModelState.IsValid)
			{
				User user = new()
				{
					Login = userDTO.Login,
					Password = userDTO.Password,
					RoleId = userDTO.RoleId
				};
				try
				{
					user = await _userRepository.RegisterUserAsync(user);

					//Send registration Email to the user
					EmailDTO emailDTO = new() 
					{ 
						To = user.Login,
						Subject = @"Registration Complited",
						Body = @$"<h1><i>{user.Login}, thanks for choosing us</i></h1>"
};
					await _emailService.SendEmailAsync(emailDTO);

					return Ok(new { user.Id, user.Login, Role = user.Role.Name });
				}
				catch (Exception ex)
				{
					return BadRequest(new { Error = ex.Message });
				}
			}
			return BadRequest(ModelState);
		}

	}
}
