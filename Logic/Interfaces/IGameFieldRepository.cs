using Logic.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IGameFieldRepository
    {
        Task Insert(GameField entity);
        Task<GameField> GetById(int id);
        Task<List<GameField>> GetAllGameFieldsForUser(int userId);
        Task AddUserToGameField(GameField gameField, int userId);
        void DeleteGameField(GameField gameField);
    }
}
