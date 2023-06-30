using FluentAssertions;
using Microsoft.Extensions.Logging;
using MyLibrary.Api.Controllers;
using MyLibrary.Api.Services;
using NSubstitute;

namespace MyLibrary.Api.Tests
{
    public class UserControllerTests
    {
        private UserController _sut;
        private readonly ILogger<UserController> _logger = Substitute.For<ILogger<UserController>>();

        [SetUp]
        public void Setup()
        {
            _sut = new UserController(_logger, new UserService());
        }

        [Test]
        public void AddUser_ValidUser()
        {
            var newUser = new User() {
                Name = "TestUser",
                Email = "Test@email.com"                
            };
            var user = _sut.Create(newUser);

            user.Should().BeGreaterThanOrEqualTo(0);
        }
    }
}