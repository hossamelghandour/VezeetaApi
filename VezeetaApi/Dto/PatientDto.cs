using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class PatientDto
    {
        public string? Image { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
