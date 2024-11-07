using Logic.Interfaces;
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
        [HttpPut]
        public async Task<IActionResult> Put(int cellId, string cellType)
        {
            await _gameFieldCellService.UpdateGameFieldCell(cellId, cellType);
            return Ok();
        }
    }
}
