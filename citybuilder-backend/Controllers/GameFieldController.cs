using Logic.model;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace citybuilder_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameFieldController : ControllerBase
    {
        private readonly IGameFieldService _gameFieldService;

        public GameFieldController(IGameFieldService gameFieldService)
        {
            _gameFieldService = gameFieldService;
        }

        [HttpPost("create")]
        public IActionResult CreateGameField(int row, int col)
        {
            if (row <= 0 || col <= 0)
            {
                return BadRequest("Rows and columns must be greater than 0.");
            }

            int maxRows = 999;
            int maxColumns = 999;
            if (row > maxRows || col > maxColumns)
            {
                return BadRequest($"Rows and columns must be less than {maxRows} and {maxColumns}.");
            }

            GameField gameField = _gameFieldService.GenerateGameField(row, col);
            return Ok(gameField);
        }


        [HttpPost("save")]
        public IActionResult PostGameField(GameField gameField) 
        {
            if (gameField == null)
            {
                return BadRequest("GameField cannot be null.");
            }
            if (gameField.Rows <= 0 || gameField.Columns <= 0)
            {
                return BadRequest("Rows and columns must be greater than 0.");
            }
            if (gameField.Cells == null || !gameField.Cells.Any())
            {
                return BadRequest("GameField must have at least one cell.");
            }
            _gameFieldService.SaveGameField(gameField);
            return Ok(gameField);
        }
    }
}
