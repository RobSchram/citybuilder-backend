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
        public async Task Insert(GameField entity)
        {
            await _context.GameFields.AddAsync(entity);
            await _context.SaveChangesAsync();

        }
        public async Task<GameField> GetById(int id)
        {
            var gameField = await _context.GameFields.FindAsync(id);
            return gameField;
        }
    }
}
