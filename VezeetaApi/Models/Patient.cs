using System.ComponentModel.DataAnnotations.Schema;

namespace VezeetaApi.Models
{
    public class Patient
    {
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<Request>? requests { get; set; }
    }
}
