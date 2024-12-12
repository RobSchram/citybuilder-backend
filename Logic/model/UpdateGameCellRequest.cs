using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.model
{
    public class UpdateGameCellRequest
    {
        public int cellId { get; set; }
        public string cellType { get; set; }
    }
}
