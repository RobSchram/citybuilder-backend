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

        [HttpGet("{rows}/{columns}")]
        public IActionResult GetGameField(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                return BadRequest("Rows and columns must be greater than 0.");
            }
            GameField gameField = _gameFieldService.GenerateGameField(rows, columns);;
            return Ok(gameField);
        }
    }
}
