using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.model
{
    public class GameFieldCell
    {
        [Key]
        public int Id { get;  set; }

        public int Row { get;  set; }
        public int Column { get;  set; }

        [ForeignKey("GameField")]
        public int GameFieldId { get;  set; } 

        public string Type { get; set; }
        public GameFieldCell(int row, int column, int gameFieldId, string type)
        {
            Row = row;
            Column = column;
            GameFieldId = gameFieldId;
            Type = type;
        }
    }
}
