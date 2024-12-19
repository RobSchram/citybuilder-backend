using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Logic.Interfaces;
using Logic.model;
using Logic.Services;
using Data;
using Data.repository;
using citybuilder_backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace citybuilder_backend.test
{
    [TestClass]
    public class GameFieldControllerTests
    {
        private ServiceProvider _serviceProvider;
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        [TestInitialize]
        public async Task Setup()
        {
            var serviceCollection = new ServiceCollection();
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GameFieldTestDB").Options;

            serviceCollection.AddScoped<IGameFieldRepository, GameFieldRepository>();
            serviceCollection.AddScoped<IGameFieldService, GameFieldService>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("GameFieldTestDB"));

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
        public async Task Test_CreateGameField_ShouldCreateGameField()
        {
            var gameFieldService = _serviceProvider.GetService<IGameFieldService>();
            var gameFieldController = new GameFieldController(gameFieldService);
            var gameFieldDto = new GameFieldDto
            {
                Row = 5,
                Col = 5
            };
            var Result = await gameFieldController.CreateGameField(gameFieldDto);
            Assert.IsNotNull(Result);

            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var gameField = await context.GameFields.FindAsync(1);
                Assert.IsNotNull(gameField);
                Assert.AreEqual(5, gameField.Rows);
                Assert.AreEqual(5, gameField.Columns);
            }
        }

        [TestMethod]
        public async Task Test_GetGameFieldById_ShouldReturnGameField()
        {
            var gameFieldService = _serviceProvider.GetService<IGameFieldService>();
            var gameField = new GameField(5, 5);
            await gameFieldService.SaveGameField(gameField);

            var gameFieldController = new GameFieldController(gameFieldService);
            var Result = await gameFieldController.GetGameFieldById(gameField.Id);

            var okResult = Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedGameField = okResult.Value as GameField;
            Assert.IsNotNull(returnedGameField);
            Assert.AreEqual(5, returnedGameField.Rows);
            Assert.AreEqual(5, returnedGameField.Columns);
        }

        [TestMethod]
        public async Task Test_GetAllGameFields_ShouldReturnGameFields()
        {
            var gameFieldService = _serviceProvider.GetService<IGameFieldService>();
            var gameField = new GameField(5, 5);
            await gameFieldService.SaveGameField(gameField);

            var gameFieldController = new GameFieldController(gameFieldService);

            var actionResult = await gameFieldController.GetAllGameFields();

            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedGameFields = okResult.Value as List<GameField>;
            Assert.IsNotNull(returnedGameFields);
            Assert.AreEqual(1, returnedGameFields.Count);
            Assert.AreEqual(5, returnedGameFields[0].Rows);
            Assert.AreEqual(5, returnedGameFields[0].Columns);
        }
    }
}
