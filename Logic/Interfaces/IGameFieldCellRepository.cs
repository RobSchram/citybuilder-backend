using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IGameFieldCellRepository
    {
        Task UpdateCell(int cellId, string cellType);
    }
}
