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
        public IActionResult CreateGameField([FromBody] GameFieldCreateRequest request)
        {
            if (row <= 0 || col <= 0)
            {
                return BadRequest("Rows and columns must be greater than 0.");
            }

            GameField gameField = _gameFieldService.GenerateGameField(request.Row, request.Col);
            return Ok(gameField);
        }


        [HttpPost("save")]
        public async Task< IActionResult> PostGameField(GameField gameField) 
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
            await _gameFieldService.SaveGameField(gameField);
            return Ok(gameField);
        }
        [HttpGet]
        public  IActionResult GetGameFieldById(int Id)
        {
            GameField gameField = _gameFieldService.GetGameFieldById(Id);
            if (gameField == null)
            {
                return NotFound("GameField cannot be null.");
            }
            if (gameField.Rows <= 0 || gameField.Columns <= 0)
            {
                return BadRequest("Rows and columns must be greater than 0.");
            }
            if (gameField.Cells == null || !gameField.Cells.Any())
            {
                return BadRequest("GameField must have at least one cell.");
            }
            return Ok(gameField);
        }
    }
}
