using Logic.Interfaces;
using Logic.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class GameFieldCellService : IGameFieldCellService
    {
        private readonly IGameFieldCellRepository _gameFieldCellRepository;
        public GameFieldCellService(IGameFieldCellRepository gameFieldCellRepository)
        {
            _gameFieldCellRepository = gameFieldCellRepository;
        }
        public async Task UpdateGameFieldCell(int cellId, string cellType)
        {
            if (cellId >= 0 && cellType != null)
            {
                await _gameFieldCellRepository.UpdateCell(cellId, cellType);
            }
            else
            {
                throw new Exception("cellId must be bigger then null and celltype musnt be null");
            }
        }
    }
}
