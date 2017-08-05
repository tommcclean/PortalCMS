using NUnit.Framework;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.CMS.Services.Tests.Authenticated
{
    [TestFixture]
    public class RoleServiceTests
    {
        #region Dependencies

        private PortalEntityModel _mockContext;

        private UserService _userService;
        private RoleService _roleService;

        [SetUp]
        public void Initialise()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _mockContext = new PortalEntityModel(connection);
            _mockContext.Database.CreateIfNotExists();

            _userService = new UserService(_mockContext);
            _roleService = new RoleService(_mockContext, _userService);
        }

        #endregion Dependencies

        #region RoleService.Get

        [Test]
        public async Task GetUserRoles_ReturnsRoles()
        {
            int? userId = 1;

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId.Value, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.SaveChanges();

            _mockContext.Roles.AddRange(new List<Role>
            {
                new Role { RoleId = 1, RoleName = "Role 1" },
                new Role { RoleId = 2, RoleName = "Role 2" }
            });

            _mockContext.SaveChanges();

            _mockContext.UserRoles.AddRange(new List<UserRole>
            {
                new UserRole { UserRoleId = 1 , UserId = userId.Value, RoleId = 1 },
                new UserRole { UserRoleId = 1 , UserId = userId.Value, RoleId = 2 },
            });

            _mockContext.SaveChanges();

            var result = await _roleService.GetAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public async Task GetUserRoles_ReturnsCorrectRole()
        {
            int? userId = 1;

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId.Value, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.SaveChanges();

            _mockContext.Roles.AddRange(new List<Role>
            {
                new Role { RoleId = 1, RoleName = "Role 1" },
                new Role { RoleId = 2, RoleName = "Role 2" }
            });

            _mockContext.SaveChanges();

            var userRoles = new List<UserRole>
            {
                new UserRole { UserRoleId = 1 , UserId = userId.Value, RoleId = 1 },
            };

            _mockContext.UserRoles.AddRange(userRoles);

            _mockContext.SaveChanges();

            var result = await _roleService.GetAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.First().RoleName == "Role 1");
        }

        [Test]
        public async Task GetUserRoles_NullUserReturnsAnonymous()
        {
            var result = await _roleService.GetAsync(null);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public async Task GetUserRoles_InvalidUserReturnsNoRoles()
        {
            int? userId = 100;

            var result = await _roleService.GetAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 0);
        }

        #endregion RoleService.Get

        #region RoleService.Update

        [Test]
        public async Task Update_RemovesAllRoles()
        {
            int? userId = 1;

            #region Setup Mock

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId.Value, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.SaveChanges();

            _mockContext.Roles.AddRange(new List<Role>
            {
                new Role { RoleId = 1, RoleName = "Role 1" },
                new Role { RoleId = 2, RoleName = "Role 2" }
            });

            _mockContext.SaveChanges();

            _mockContext.UserRoles.AddRange(new List<UserRole>
            {
                new UserRole { UserRoleId = 1 , UserId = userId.Value, RoleId = 1 },
                new UserRole { UserRoleId = 1 , UserId = userId.Value, RoleId = 2 },
            });

            _mockContext.SaveChanges();

            #endregion Setup Mock

            var result = await _roleService.GetAsync(userId);

            if (!result.Any())
                throw new ArgumentException("Mock Invalid, Expected Mock User to have 2 User Roles");

            await _roleService.UpdateAsync(userId.Value, new List<string>());

            result = await _roleService.GetAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 0);
        }

        [Test]
        public async Task Update_AddsAdditionalRole()
        {
            int? userId = 1;

            #region Setup Mock

            _mockContext.Users.AddRange(new List<User>
            {
                new User { UserId = userId.Value, GivenName = "Test", FamilyName = "User", EmailAddress = "Email", Password = "Password", DateAdded = DateTime.Now, DateUpdated = DateTime.Now }
            });

            _mockContext.SaveChanges();

            _mockContext.Roles.AddRange(new List<Role>
            {
                new Role { RoleId = 1, RoleName = "Role 1" },
                new Role { RoleId = 2, RoleName = "Role 2" }
            });

            _mockContext.SaveChanges();

            _mockContext.UserRoles.AddRange(new List<UserRole>
            {
                new UserRole { UserRoleId = 1 , UserId = userId.Value, RoleId = 1 },
            });

            _mockContext.SaveChanges();

            #endregion Setup Mock

            var result = await _roleService.GetAsync(userId);

            if (!result.Any())
                throw new ArgumentException("Mock Invalid, Expected Mock User to have 2 User Roles");

            await _roleService.UpdateAsync(userId.Value, new List<string> { "Role 1", "Role 2" });

            result = await _roleService.GetAsync(userId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }

        #endregion RoleService.Update

        #region RoleService.Validate

        [Test]
        public void Validate_Admin_CanAccessEverything()
        {
            var entityRoles = new List<Role> { new Role { RoleId = 2, RoleName = "Other Role" } };
            var userRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Admin" } };

            var result = _roleService.Validate(entityRoles, userRoles);

            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_Authenticated_CanAccessEmptyRoleSets()
        {
            var entityRoles = new List<Role>();
            var userRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Guest" } };

            var result = _roleService.Validate(entityRoles, userRoles);

            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_Anonymous_NoAccessToLockedEntity()
        {
            var entityRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Any Role" } };
            var userRoles = new List<Role>();

            var result = _roleService.Validate(entityRoles, userRoles);

            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_Authenticated_CanAccessEntityWithSameRole()
        {
            var entityRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Role 1" }, new Role { RoleId = 2, RoleName = "Role 2" } };
            var userRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Role 1" } };

            var result = _roleService.Validate(entityRoles, userRoles);

            Assert.IsTrue(result);
        }

        #endregion RoleService.Validate
    }
}