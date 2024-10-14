using VezeetaApi.Models;

namespace VezeetaApi.Dto
{
    public class SpecializationDto
    {
        public int SpecializationId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public Doctor Doctor { get; set; }
    }
}
