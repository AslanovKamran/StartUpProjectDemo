using System.ComponentModel.DataAnnotations;

namespace StartUpProjectDemo.Models.DTO
{
	public class PostUserDTO
	{
		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Login { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Password { get; set; } = string.Empty;

		[Required]
		public int RoleId { get; set; }
	}
}
