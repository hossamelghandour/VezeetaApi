using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VezeetaApi.Dto;
using VezeetaApi.Repository.AppointmentRepo;

namespace VezeetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Doctor")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepo _appointmentRepo;

        public AppointmentController(IAppointmentRepo appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        [HttpPost("ADDAppointment")]
        public IActionResult Add([FromForm]AppointmentDto item)
        {
            try
            {
                _appointmentRepo.Add(item);
                return Ok("Appointment added successfully");
            }
            catch(Exception ex) 
            {
                return BadRequest("doctor not found");
            }
        }

        [HttpPut("UpdateAppointment")]
        public IActionResult update(int id, TimeSpan time)
        {

            try
            {
                _appointmentRepo.Update(id, time);
                return Ok("Appointment added successfully");
            }
            catch (Exception ex)
            {

                return BadRequest("Doctor not found");

            }
        }

        [HttpDelete("DeleteAppointment")]
        public IActionResult delete(int appointmentId, TimeSpan time)
        {
            try
            {
                _appointmentRepo.Delete(appointmentId, time);
                return Ok(true);
            }
            catch
            {

                return Ok(false);

            }

        }
    }
}
