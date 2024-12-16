using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.model
{
    public class GameFieldDto
    {
        [Required]
        public int Row { get; set; }
        [Required]
        public int Col { get; set; }
    }
}
