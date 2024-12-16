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
    public class GameFieldCellServiceTests
    {
        private Mock<IGameFieldCellRepository> _mockGameFieldCellRepository;
        private GameFieldCellService _gameFieldCellService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGameFieldCellRepository = new Mock<IGameFieldCellRepository>();
            _gameFieldCellService = new GameFieldCellService(_mockGameFieldCellRepository.Object);
        }

        [TestMethod]
        public async Task UpdateGameFieldCell_ShouldCallUpdateCell_WhenCalledWithValidArguments()
        {
            int cellId = 1;
            string cellType = "house";
            await _gameFieldCellService.UpdateGameFieldCell(cellId, cellType);
            _mockGameFieldCellRepository.Verify(repo => repo.UpdateCell(cellId, cellType), Times.Once);
        }

        [TestMethod]
        public async Task UpdateGameFieldCell_ShouldNotCallUpdateCell_WhenArgumentsAreInvalid()
        {
            int cellId = -1;
            string cellType = "Empty";
            await Assert.ThrowsExceptionAsync<Exception>(() => _gameFieldCellService.UpdateGameFieldCell(cellId, cellType));
            _mockGameFieldCellRepository.Verify(repo => repo.UpdateCell(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }


        [TestMethod]
        public async Task UpdateGameFieldCell_ShouldHandleExceptionsGracefully()
        {
            int cellId = 1;
            string cellType = "house";
            _mockGameFieldCellRepository.Setup(repo => repo.UpdateCell(cellId, cellType)).ThrowsAsync(new Exception("Database error"));

            await Assert.ThrowsExceptionAsync<Exception>(() => _gameFieldCellService.UpdateGameFieldCell(cellId, cellType));
        }
    }
}
