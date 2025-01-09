using Logic.model;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateGameField([FromBody] GameFieldDto request)
        {
            if (request.Row <= 0 || request.Col <= 0)
            {
                return BadRequest("Rows and columns must be greater than 0.");
            }

            GameField gameField = await _gameFieldService.GenerateGameField(request.Row, request.Col);
            gameField.addUserId(request.userId);
            await _gameFieldService.SaveGameField(gameField);
            return Ok(gameField);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetGameFieldById(int Id)
        {
            GameField gameField = await _gameFieldService.GetGameFieldById(Id);
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
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllGameFields([FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var gameFields = await _gameFieldService.GetAllGameFields(userId);

            if (gameFields == null || !gameFields.Any())
            {
                return NotFound($"No game fields found for user ID {userId}.");
            }

            return Ok(gameFields);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> AddUserToGameField([FromBody] AddUserDto addUserDto )
        {
            await _gameFieldService.AddUserToGameField(addUserDto.userName, addUserDto.gameId); 
            return Ok();
        }

    }
}
