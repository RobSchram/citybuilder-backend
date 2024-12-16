using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.model;

namespace Logic.Interfaces
{
    public interface IGameFieldService
    {
        Task<GameField> GenerateGameField(int row, int column);
        Task SaveGameField(GameField gameField);
        Task<GameField> GetGameFieldById(int id);
        Task<List<GameField>> GetAllGameFields();
    }
}
