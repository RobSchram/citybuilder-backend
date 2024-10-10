using Logic.Interfaces;
using Logic.model;
using Logic.Services;
using Moq;
using System;
[TestClass]
public class GameFieldServiceTests
{
    private readonly Mock<IGameFieldRepository> _mockRepository;
    private readonly GameFieldService _gameFieldService;

    public GameFieldServiceTests()
    {
        _mockRepository = new Mock<IGameFieldRepository>();
        _gameFieldService = new GameFieldService(_mockRepository.Object);
    }
    [TestMethod]
    public void GenerateGameField_ShouldReturnNewGameField()
    {
        int row = 5;
        int col = 6;
        var gameField = _gameFieldService.GenerateGameField(row, col);

        Assert.IsNotNull(gameField);

    }
}
