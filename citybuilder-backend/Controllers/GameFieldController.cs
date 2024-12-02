﻿using Logic.model;
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

        [HttpPost]
        public async Task< IActionResult> CreateGameField(int row, int col)
        {
            if (row <= 0 || col <= 0)
            {
                return BadRequest("Rows and columns must be greater than 0.");
            }

            GameField gameField = await _gameFieldService.GenerateGameField(row, col);
            await _gameFieldService.SaveGameField(gameField);
            return Ok(gameField);
        }   

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
    }
}
