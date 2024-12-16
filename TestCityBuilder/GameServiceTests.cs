using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Logic.Interfaces;
using Logic.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Tests
{
    [TestClass]
    public class GameFieldServiceTests
    {
        private Mock<IGameFieldRepository> _mockGameFieldRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private GameFieldService _gameFieldService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGameFieldRepository = new Mock<IGameFieldRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _gameFieldService = new GameFieldService(_mockGameFieldRepository.Object, _mockUserRepository.Object);
        }
        [TestMethod]
        public void GenerateGameField_ShouldReturnGameField_WithGivenRowsAndColumns()
        {
            int rows = 5;
            int columns = 5;
            var result = _gameFieldService.GenerateGameField(rows, columns).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(rows, result.Rows);
            Assert.AreEqual(columns, result.Columns);
        }

        [TestMethod]
        public async Task SaveGameField_ShouldCallInsert_WhenCalled()
        {
            var gameField = new GameField(5, 5);
            await _gameFieldService.SaveGameField(gameField);
            _mockGameFieldRepository.Verify(repo => repo.Insert(gameField), Times.Once);
        }

        [TestMethod]
        public async Task GetGameFieldById_ShouldReturnNull_WhenIdIsInvalid()
        {
            int gameFieldId = -1;
            _mockGameFieldRepository.Setup(repo => repo.GetById(gameFieldId)).ReturnsAsync((GameField)null);
            var result = await _gameFieldService.GetGameFieldById(gameFieldId);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllGameFields_ShouldReturnListOfGameFields()
        {
            var gameFields = new List<GameField>
            {
                new GameField(5, 5),
                new GameField(10, 10)
            };
            _mockGameFieldRepository.Setup(repo => repo.GetAll()).ReturnsAsync(gameFields);
            var result = await _gameFieldService.GetAllGameFields();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
