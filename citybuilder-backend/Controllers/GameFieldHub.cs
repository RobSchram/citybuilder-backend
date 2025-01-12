using Logic.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace citybuilder_backend.Hubs
{
    public class GameFieldHub : Hub
    {
        private readonly IGameFieldCellService _gameFieldCellService;
        private readonly IGameFieldService _gameFieldService;

        public GameFieldHub(IGameFieldCellService gameFieldCellService)
        {
            _gameFieldCellService = gameFieldCellService;
        }
        public async Task JoinGameGroup(int gameId)
        {
            try
            {
                var groupName = Convert.ToString( gameId);
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Caller.SendAsync("gameJoined", gameId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in JoinGameGroup: {ex.Message}");
                await Clients.Caller.SendAsync("joinGameError", ex.Message);
            }
        }
        public async Task UpdateGameFieldCell(int gameId, int cellId, string cellType)
            {
            Convert.ToString(gameId);
            await _gameFieldCellService.UpdateGameFieldCell(cellId, cellType);
            await Clients.Group(Convert.ToString(gameId)).SendAsync("ReceiveUpdate", cellId, cellType);
        }
    }
}
