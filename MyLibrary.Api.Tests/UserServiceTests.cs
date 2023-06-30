using FluentAssertions;
using MyLibrary.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Api.Tests
{
    public class UserServiceTests
    {
        private  UserService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new UserService();

        }

        public UserServiceTests()
        {
        }

        [Test]
        public void GetUsers_when_no_users()
        {
            var users = _sut.GetAll();

            users.Should().BeEmpty();
        }

        [Test]
        public void GetUsers()
        {
            var newUser = new User() { Email = "TEst@email.com", Name = "name" };
            _sut.Create(newUser);
            var newUser1 = new User() { Email = "TEst1@email.com", Name = "name1" };
            _sut.Create(newUser1);

            var users = _sut.GetAll();

            users.Should().BeEquivalentTo(new List<User>() { newUser, newUser1});
        }

        [Test]
        public void GetUsersById()
        {
            var newUser = new User() { Email = "TEst@email.com", Name = "name" };
            _sut.Create(newUser);
            var newUser1 = new User() { Email = "TEst1@email.com", Name = "name1" };
            _sut.Create(newUser1);

            var user1 = _sut.GetById(1);
            var user2 = _sut.GetById(2);
            var nonExistent = _sut.GetById(100);

            user1.Should().BeEquivalentTo( newUser);
            user2.Should().BeEquivalentTo( newUser1);
            nonExistent.Should().BeNull();
        }

        [Test]
        public void AddUser()
        {
            var newUser = new User()
            {
                Email = "testuser@email.com",
                Name = "testuser"
            };

            var id = _sut.Create(newUser);

            id.Should().Be(1);
        }

        [Test]
        public void UpdateUser()
        {
            var newUser = new User()
            {
                Email = "testuser@email.com",
                Name = "testuser"
            };

            var id = _sut.Create(newUser);

            var updatedUser = new User()
            {
                Email = "updated@email.com",
                Name = "updated",
                Id = id
            };

            var result = _sut.Update(updatedUser);

            result.Should().BeTrue();
            _sut.GetById(id).Should().BeEquivalentTo(updatedUser);
        }

        [Test]
        public void UpdateNonExistentUser()
        {
            var newUser = new User()
            {
                Email = "testuser@email.com",
                Name = "testuser"
            };

           var result = _sut.Update(newUser);

            result.Should().BeFalse();
        }

        [Test]
        public void DeleteUser()
        {
            var newUser = new User()
            {
                Email = "testuser@email.com",
                Name = "testuser"
            };

            var id = _sut.Create(newUser);

            var result = _sut.Delete(id);

            result.Should().BeTrue();
            _sut.GetAll().Should().BeEmpty();
        }

        [Test]
        public void DeleteNonExistentUser()
        {
            var newUser = new User() { Email = "TEst@email.com", Name = "name" };
            _sut.Create(newUser);
            var newUser1 = new User() { Email = "TEst1@email.com", Name = "name1" };
            _sut.Create(newUser1);


            var result = _sut.Delete(10);

            result.Should().BeFalse();
            _sut.GetAll().Should().BeEquivalentTo(new List<User>() { newUser, newUser1 });
        }

    }
}
