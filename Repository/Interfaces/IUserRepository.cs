﻿using StartUpProjectDemo.Models.Domain;

namespace StartUpProjectDemo.Repository.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<User>> GetUsersAsync();
		Task<User> GetUserAsync(int id);

		Task<User> RegisterUserAsync(User user);
		Task<User> LogInUserAsync(string login, string password);
	}
}
