using System.ComponentModel.DataAnnotations.Schema;

namespace VezeetaApi.Models
{
    public class Doctor
    {
        public int SpecializationId { get; set; }
        public Specialization? specialization { get; set; }

        [Column(TypeName =("decimal(18,4)"))]
        public decimal Price { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public List<Request>? requests { get; set; }
        public List<Appointment>? appointments { get; set; }
    }
}
