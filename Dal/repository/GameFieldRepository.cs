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
        private readonly DbSet<GameField> _dbSet;
        public GameFieldRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<GameField>();
        }
        public void Insert(GameField entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();

        }
        public GameField GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
