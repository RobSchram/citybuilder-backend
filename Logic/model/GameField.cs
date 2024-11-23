using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.model
{
    public class GameField
    {
        [Key]
        public int Id { get; private set; }

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public virtual ICollection<GameFieldCell> Cells { get; private set; }
        public GameField(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Cells = new List<GameFieldCell>();
        }
        public void InitializeCells()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Cells.Add(new GameFieldCell(i, j, Id, "grass"));
                }
            }
        }
    }
}
