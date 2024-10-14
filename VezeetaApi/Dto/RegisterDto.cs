using System.ComponentModel.DataAnnotations;
using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class RegisterDto
    {
        public string? FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Image { get; set; }
        public string? Phone { get; set; }
        public Gender? Gender{ get; set; }
        public DateTime? DateOfBirth { get; set; }
        public AccountType? AccountType { get; set; }

    }
}
