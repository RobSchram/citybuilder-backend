using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.repository
{
    public class GameFieldCellRepository : IGameFieldCellRepository
    {
        private readonly ApplicationDbContext _context;
        public GameFieldCellRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateCell(int cellId, string cellType)
        {
            var cell = await _context.GameFieldCells.FirstOrDefaultAsync(x=> x.Id == cellId);
            if (cell == null) { }
            else 
            {
                cell.Type = cellType;
            }
            await _context.SaveChangesAsync();
        }
    }
}
