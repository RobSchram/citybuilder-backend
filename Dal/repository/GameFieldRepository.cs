using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.model;
using Logic.Interfaces;

namespace Data.repository
{
    public class GameFieldRepository : IGameFieldRepository
    {
        private readonly ApplicationDbContext _context;
        public GameFieldRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Insert(GameField gameField)
        {
            _context.GameFields.Add(gameField);
            await _context.SaveChangesAsync();
            gameField.InitializeCells();
            await _context.SaveChangesAsync();

        }
        public async Task<GameField> GetById(int id)
        {
            GameField gameField = await _context.GameFields.Include(g => g.Cells).FirstOrDefaultAsync(g => g.Id == id);
            return gameField;
        }
        public async Task<List<GameField>> GetAllGameFieldsForUser(int userId)
        {
            List<GameField> gameFields = await _context.GameFields.Include(g => g.Cells).Where(g => g.usersId.Contains(userId)).ToListAsync();

            return gameFields;
        }
        public async Task AddUserToGameField(GameField gameField, int userId)
        {
                gameField.addUserId(userId);
                _context.GameFields.Update(gameField);
                await _context.SaveChangesAsync();
        }
        public void DeleteGameField(GameField gameField)
        {
             _context.GameFields.Remove(gameField);
            _context.SaveChanges();
        }


    }
}
