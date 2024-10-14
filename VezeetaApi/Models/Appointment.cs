using System.ComponentModel.DataAnnotations.Schema;

namespace VezeetaApi.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId {  get; set; }
        public Doctor? Doctor { get; set; }
        public Days Days { get; set; }
        public List<Times>? times { get; set; }
        public Request? Request { get; set; }




    }
}
