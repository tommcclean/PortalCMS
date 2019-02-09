﻿using Microsoft.AspNet.Identity.EntityFramework;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Entities.Models;
using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Authentication
{
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(string emailAddress, string password, string givenName, string familyName);

        Task ChangePasswordAsync(string userId, string newPassword);
    }

	public class RegistrationService : IRegistrationService
	{
		#region Dependencies

		readonly PortalDbContext _context;
		readonly ApplicationUserManager UserManager;
		readonly ApplicationRoleManager RoleManager;

		public RegistrationService(PortalDbContext context)
		{
			_context = context;
			UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
			RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
		}

		#endregion Dependencies

		public async Task<string> RegisterAsync(string emailAddress, string password, string givenName, string familyName)
		{
			if (await _context.Users.AnyAsync(x => x.Email.Equals(emailAddress, StringComparison.OrdinalIgnoreCase)))
				return "-1";

			var userAccount = new ApplicationUser
			{
				Email = emailAddress,
				GivenName = givenName,
				FamilyName = familyName,
				AvatarImage = FileDetail.LoadImageFromPath("/Areas/Admin/Content/Images/", "profile-image-male.png"),
				DateAdded = DateTime.Now,
				DateUpdated = DateTime.Now,
				UserName = GenerateUserName(),
				RegistrationDate = DateTime.Now
			};

			var result = await UserManager.CreateAsync(userAccount, password);

			return userAccount.Id;
		}

		public async Task ChangePasswordAsync(string userId, string newPassword)
		{
			var userAccount = await UserManager.FindByIdAsync(userId);
			if (userAccount == null) return;

			userAccount.DateUpdated = DateTime.Now;
			userAccount.PasswordHash = UserManager.PasswordHasher.HashPassword(newPassword);
			await UserManager.UpdateAsync(userAccount);
		}

		private string  GenerateUserName(int length=10)
		{
			string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
			string numbers = "1234567890";

			string characters = alphabets + small_alphabets + numbers;

			string userName = string.Empty;
			for (int i = 0; i < length; i++)
			{
				string character = string.Empty;
				do
				{
					int index = new Random().Next(0, characters.Length);
					character = characters.ToCharArray()[index].ToString();
				} while (userName.IndexOf(character) != -1);
				userName += character;
			}

			return userName;
		}
	}
}