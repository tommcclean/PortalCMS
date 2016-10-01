using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portal.CMS.Services.Authentication;
using Portal.CMS.Entities;
using System.Data.Common;
using System.Collections.Generic;
using Portal.CMS.Entities.Entities.Authentication;

namespace Portal.CMS.Services.Tests
{
    [TestClass]
    public class RoleServiceTests
    {
        private PortalEntityModel _mockContext;

        private UserService _userService;
        private RoleService _roleService;

        [TestInitialize]
        public void Initialise()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _mockContext = new PortalEntityModel(connection);
            _mockContext.Database.CreateIfNotExists();

            _userService = new UserService(_mockContext);
            _roleService = new RoleService(_mockContext, _userService);
        }

        [TestMethod]
        public void Validate_AdminCanAccessOtherRoles()
        {
            var entityRoles = new List<Role> { new Role { RoleId = 2, RoleName = "Other Role" } };

            var userRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Admin" } };

            var result = _roleService.Validate(entityRoles, userRoles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_NoEntityRolesGrantsAccessToAll()
        {
            var entityRoles = new List<Role>();

            var userRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Guest" } };

            var result = _roleService.Validate(entityRoles, userRoles);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_NoUserRolesHasNoAccessToRoleLockedEntity()
        {
            var entityRoles = new List<Role> { new Role { RoleId = 1, RoleName = "Any Role" } };

            var userRoles = new List<Role>();

            var result = _roleService.Validate(entityRoles, userRoles);

            Assert.IsFalse(result);
        }
    }
}
