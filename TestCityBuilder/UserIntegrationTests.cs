using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Api.Controllers;
using Logic.Interfaces;
using Logic.Services;
using Data;
using Data.repository;
using Logic.model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Api.IntegrationTests
{
    [TestClass]
    public class UserControllerTests
    {
        private ServiceProvider _serviceProvider;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        [TestInitialize]
        public async Task Setup()
        {
            var serviceCollection = new ServiceCollection();
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("UserTestDB").Options;

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("UserTestDB"));

            serviceCollection.AddSingleton<IConfiguration>(provider =>
            {
                var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>{{ "Jwt:SecretKey", "mysupersupersecret123456789123456789" },{ "Jwt:Issuer", "test_issuer" },{ "Jwt:Audience", "test_audience" } }).Build();
                return configuration;
            });

            _serviceProvider = serviceCollection.BuildServiceProvider();

            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Database.EnsureCreated();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public async Task Test_RegisterUser_ShouldCreateUser()
        {
            var userService = _serviceProvider.GetService<IUserService>();
            var authService = _serviceProvider.GetService<IAuthService>();
            var userController = new UserController(userService, authService);

            var userDto = new UserDto
            {
                username = "testuser",
                password = "password123"
            };
            var result = await userController.Register(userDto) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == "testuser");
                Assert.IsNotNull(user);
                Assert.AreEqual("testuser", user.UserName);
            }
        }

        [TestMethod]
        public async Task Test_LoginUser_ShouldReturnToken()
        {
            var userService = _serviceProvider.GetService<IUserService>();
            var authService = _serviceProvider.GetService<IAuthService>();
            var userController = new UserController(userService, authService);

            var userDto = new UserDto
            {
                username = "testuser",
                password = "password123"
            };

            await userController.Register(userDto);
            var result = await userController.Login(userDto) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Token"));
        }

        [TestMethod]
        public async Task Test_GetUserById_ShouldReturnUser()
        {
            var userService = _serviceProvider.GetService<IUserService>();
            var authService = _serviceProvider.GetService<IAuthService>();
            var userController = new UserController(userService, authService);

            var userDto = new UserDto
            {
                username = "testuser",
                password = "password123"
            };
            await userController.Register(userDto);

            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == "testuser");
                var result = await userController.GetUserById(user.Id) as OkObjectResult;

                Assert.IsNotNull(result);
                Assert.AreEqual(200, result.StatusCode);

                var returnedUser = result.Value as User;
                Assert.IsNotNull(returnedUser);
                Assert.AreEqual("testuser", returnedUser.UserName);
            }
        }
    }
}
