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
        void Insert(GameField entity);
        GameField GetById(int id);
    }
}
