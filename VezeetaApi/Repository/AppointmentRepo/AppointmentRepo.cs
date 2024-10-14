using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using VezeetaApi.Data;
using VezeetaApi.Dto;
using VezeetaApi.Models;

namespace VezeetaApi.Repository.AppointmentRepo
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly AppDbContext _context;

        public AppointmentRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Add(AppointmentDto item)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == item.DoctorId);

            if (doctor != null)
            {
                var times = new List<Times> { new Times { Time = item.times.Time } };

                var Appointment = new Appointment()
                {
                    DoctorId = item.DoctorId,
                    Days = item.days,
                    times = times
                };

                Appointment.Doctor.Price = item.Price;
                _context.Appointments.Add(Appointment);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Doctor not found");
            }

        }


        public void Delete(int AppointmentId, TimeSpan time)
        {
            using(var context =new AppDbContext())
            {
                var appointment=context.Appointments
                    .Include(a=>a.times)
                    .FirstOrDefault(a=>a.AppointmentId== AppointmentId);

                if(appointment != null)
                {
                    // Check if the time to delete is already booked
                    var isTimeBooked = appointment.times.Any(t => t.Time == time);
                    if (isTimeBooked)
                    {
                        throw new Exception("The time is already booked and cannot be deleted");
                    }
                    //remove the time from the appointment
                    var timeToremove = appointment.times.FirstOrDefault(t => t.Time == time);
                    if (timeToremove != null)
                    {
                        appointment.times.Remove(timeToremove);
                    }
                    else
                    {
                        throw new Exception("The time not found in the appointment");
                    }

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Appointment not found");
                }
            }
        }

        public void Update(int appointmentId, TimeSpan newTime)
        {
            using(var context=new AppDbContext())
            {
                var appointment = context.Appointments
                    .Include(a => a.times)
                    .FirstOrDefault(a => a.AppointmentId == appointmentId);

                if(appointment != null)
                {
                    var isTimeAvailable = appointment.times.All(t => t.Time != newTime);
                    if (!isTimeAvailable)
                    {
                        throw new Exception("The new time is already booked");
                    }

                    var oldTime=appointment.times.FirstOrDefault();
                    if (oldTime != null)
                    {
                        oldTime.Time = newTime;
                    }
                    else
                    {
                        throw new Exception("No time found to update");
                    }

                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Appointment not found");
                }
            }
        }
    }
}
