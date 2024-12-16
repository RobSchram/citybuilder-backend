using System.ComponentModel.DataAnnotations;

namespace Logic.model
{
    public class UserDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}

