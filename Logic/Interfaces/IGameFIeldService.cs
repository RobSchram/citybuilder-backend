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
        GameField GenerateGameField(int row, int column);
    }
}
