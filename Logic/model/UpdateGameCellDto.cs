using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.model
{
    public class UpdateGameCellDto
    {
        [Required]
        public int cellId { get; set; }
        [Required]
        public string cellType { get; set; }
    }
}
