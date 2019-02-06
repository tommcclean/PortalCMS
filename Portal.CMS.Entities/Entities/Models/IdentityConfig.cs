using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;

namespace Portal.CMS.Entities.Entities.Models
{
	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
	public class CustomUserManager : UserManager<CustomUser, int>
	{
		public CustomUserManager(IUserStore<CustomUser, int> store) : base(store)
		{
		}

		public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options,IOwinContext context)
		{
			var manager = new CustomUserManager(new CustomUserStore(context.Get<PortalDbContext>()));

			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<CustomUser, int>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
			};
			// Configure user lockout defaults
			manager.UserLockoutEnabledByDefault = true;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			manager.MaxFailedAccessAttemptsBeforeLockout = 5;
			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug in here.
			manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<CustomUser, int>
			{
				MessageFormat = "Your security code is: {0}"
			});

			manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<CustomUser, int>
			{
				Subject = "SecurityCode",
				BodyFormat = "Your security code is {0}"
			});

			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();

			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider = new DataProtectorTokenProvider<CustomUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
			}

			return manager;
		}

		public bool Validate(IEnumerable<CustomRole> entityRoles, IEnumerable<string> userRoles)
		{
			// PASS: Where no roles are specified on the entity, access is granted to all users.
			if (!entityRoles.Any())
				return true;

			// PASS: Administrators can access any content.
			if (userRoles.Any(x => x.Equals("Admin", System.StringComparison.OrdinalIgnoreCase)))
				return true;

			// EVALUATE: Every Role on the Entity. One match grants access.
			foreach (var role in entityRoles)
				if (userRoles.Contains(role.Name))
					return true;

			return false;
		}
	}

	// Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
	public class CustomRoleManager : RoleManager<IdentityRole>
	{
		public CustomRoleManager(IRoleStore<IdentityRole, string> roleStore): base(roleStore)
		{
		}

		public static CustomRoleManager Create(IdentityFactoryOptions<CustomRoleManager> options, IOwinContext context)
		{
			return new CustomRoleManager(new RoleStore<IdentityRole>(context.Get<PortalDbContext>()));
		}
	}

	public class EmailService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your email service here to send an email.
			return Task.FromResult(0);
		}
	}

	public class SmsService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your sms service here to send a text message.
			return Task.FromResult(0);
		}
	}

	// This is useful if you do not want to tear down the database each time you run the application.
	// public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
	// This example shows you how to create a new database if the Model changes
	public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<PortalDbContext>
	{
		protected override void Seed(PortalDbContext context)
		{
			InitializeIdentityForEF(context);
			base.Seed(context);
		}

		//Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
		public static void InitializeIdentityForEF(PortalDbContext db)
		{
			var userManager = HttpContext.Current.GetOwinContext().GetUserManager<CustomUserManager>();
			var roleManager = HttpContext.Current.GetOwinContext().Get<CustomRoleManager>();
			const string name = "admin@example.com";
			const string password = "Admin@123456";
			const string roleName = "Admin";

			//Create Role Admin if it does not exist
			var role = roleManager.FindByName(roleName);
			if (role == null)
			{
				role = new IdentityRole(roleName);
				var roleresult = roleManager.Create(role);
			}

			var user = userManager.FindByName(name);
			if (user == null)
			{
				user = new CustomUser { UserName = name, Email = name };
				var result = userManager.Create(user, password);
				result = userManager.SetLockoutEnabled(user.Id, false);
			}

			// Add user admin to Role Admin if not already added
			var rolesForUser = userManager.GetRoles(user.Id);
			if (!rolesForUser.Contains(role.Name))
			{
				var result = userManager.AddToRole(user.Id, role.Name);
			}
		}
	}

	// Configure the application sign-in manager which is used in this application.
	public class CustomSignInManager : SignInManager<CustomUser, int>
	{
		public CustomSignInManager(CustomUserManager userManager, IAuthenticationManager authenticationManager) :base(userManager, authenticationManager)
		{ }

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(CustomUser user)
		{
			return user.GenerateUserIdentityAsync((CustomUserManager)UserManager);
		}

		public static CustomSignInManager Create(IdentityFactoryOptions<CustomSignInManager> options, IOwinContext context)
		{
			return new CustomSignInManager(context.GetUserManager<CustomUserManager>(), context.Authentication);
		}
	}
}