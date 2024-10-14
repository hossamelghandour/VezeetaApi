using System.ComponentModel.DataAnnotations;

namespace VezeetaApi.Dto
{
    public class LoginDto
    {
        [Required]
        public string Emial { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
