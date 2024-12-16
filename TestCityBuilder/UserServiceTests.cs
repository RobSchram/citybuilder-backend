using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Logic.Interfaces;
using Logic.model;
using System;
using System.Threading.Tasks;

namespace Logic.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _userService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [TestMethod]
        public async Task AddUserAsync_ShouldAddUser_WhenUserDoesNotExist()
        {
            var userDto = new UserDto
            {
                username = "newuser",
                password = "Password123"
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(userDto.username)).ReturnsAsync((User)null);
            await _userService.AddUserAsync(userDto);

            _mockUserRepository.Verify(repo => repo.AddUserAsync(It.Is<User>(u => u.UserName == userDto.username)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Username is already taken.")]
        public async Task AddUserAsync_ShouldThrowArgumentException_WhenUserAlreadyExists()
        {
            var userDto = new UserDto
            {
                username = "existinguser",
                password = "Password123"
            };

            var existingUser = new User { UserName = "existinguser", Password = "HashedPassword" };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(userDto.username))
                .ReturnsAsync(existingUser);

            await _userService.AddUserAsync(userDto);
        }

        [TestMethod]
        public async Task AuthenticateAsync_ShouldReturnUser_WhenCredentialsAreValid()
        {
            var userDto = new UserDto
            {
                username = "validuser",
                password = "ValidPassword"
            };

            var user = new User
            {
                UserName = "validuser",
                Password = BCrypt.Net.BCrypt.HashPassword("ValidPassword")
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(userDto.username)).ReturnsAsync(user);

            var result = await _userService.AuthenticateAsync(userDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(userDto.username, result.UserName);
        }

        [TestMethod]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            var userDto = new UserDto
            {
                username = "invaliduser",
                password = "WrongPassword"
            };

            var user = new User
            {
                UserName = "validuser",
                Password = BCrypt.Net.BCrypt.HashPassword("ValidPassword")
            };

            _mockUserRepository.Setup(repo => repo.GetUserByUsernameAsync(userDto.username)).ReturnsAsync(user);

            var result = await _userService.AuthenticateAsync(userDto);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            var users = new List<User>
            {
                new User { UserName = "user1" },
                new User { UserName = "user2" }
            };

            _mockUserRepository.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _userService.GetAllUsersAsync();

            Assert.AreEqual(users.Count, result.Count());
            Assert.AreEqual(users[0].UserName, result.First().UserName);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            var user = new User { UserName = "existinguser", Id = 1 };
            _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _userService.GetUserByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("existinguser", result.UserName);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync((User)null);

            var result = await _userService.GetUserByIdAsync(1);

            Assert.IsNull(result);
        }
    }
}
