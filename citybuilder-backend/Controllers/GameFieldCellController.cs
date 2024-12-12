using Logic.Interfaces;
using Logic.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace citybuilder_backend.Controllers
{
    [Route("api/GameField/{GameFieldId}/GameFieldCell")]
    [ApiController]
    public class GameFieldCellController : ControllerBase
    {
        private readonly IGameFieldCellService _gameFieldCellService;

        public GameFieldCellController(IGameFieldCellService gameFieldCellService)
        {
            _gameFieldCellService = gameFieldCellService;
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateGameCellRequest updateGameCellRequest)
        {
            await _gameFieldCellService.UpdateGameFieldCell(updateGameCellRequest.cellId, updateGameCellRequest.cellType);
            return Ok();
        }
    }
}
