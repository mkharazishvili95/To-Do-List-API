using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_API.Models;
using To_Do_List_API.Services;
using To_Do_List_API_Tests.FakeServices;

namespace To_Do_List_API_Tests
{
    public class UserServiceTests
    {
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            var users = new List<User>
            {
                new User { Id = 1, UserName = "user1", Password = "password1" },
                new User { Id = 2, UserName = "user2", Password = "password2" }
            };

            _userService = new FakeUserService(users);
        }

        [Test]
        public void Login_ValidCredentials_ReturnsUser()
        {
            var user = _userService.Login(new LoginModel { UserName = "user1", Password = "password1" });

            Assert.IsNotNull(user);
            Assert.AreEqual(1, user.Id);
        }

        [Test]
        public void Login_InvalidCredentials_ReturnsNull()
        {
            var user = _userService.Login(new LoginModel { UserName = "user1", Password = "invalidPassword" });

            Assert.IsNull(user);
        }
        [Test]
        public void Register_InvalidData_ReturnsNull()
        {

            var invalidRegisterModel = new RegisterModel
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "",
                Password = "testPassword"
            };

            var result = _userService.Register(invalidRegisterModel);

            Assert.IsNull(result);
        }
        [Test]
        public void Register_ValidData_ReturnsUser()
        {
            var validRegisterModel = new RegisterModel
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "johndoe",
                Password = "testPassword"
            };

            var result = _userService.Register(validRegisterModel);

            Assert.IsNotNull(result);
            Assert.AreEqual(validRegisterModel.FirstName, result.FirstName);
            Assert.AreEqual(validRegisterModel.LastName, result.LastName);
            Assert.AreEqual(validRegisterModel.UserName, result.UserName);
        
        }
    }
}
