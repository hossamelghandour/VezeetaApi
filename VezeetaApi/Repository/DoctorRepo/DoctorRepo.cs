using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VezeetaApi.Data;
using VezeetaApi.Dto;
using VezeetaApi.Models;

namespace VezeetaApi.Repository.DoctorRepo
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorRepo(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Delete(string id)
        {
            var doctorToDelete=_context.Doctors
                .Include(d=>d.applicationUser)
                .Include(d=>d.specialization)
                .FirstOrDefault(d=>d.Id == id);

            if(doctorToDelete != null)
            {
                _context.Doctors.Remove(doctorToDelete);
                doctorToDelete.applicationUser.IsDeleted=true;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Doctor not found");
            }
        }

        public List<DoctorWithSpecializationDto> GetAll(int pageNumber=1, int pageSize=10, string search="")
        {
            var query = _context.Doctors.Include(d => d.specialization).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query=query.Where(d=>
                d.applicationUser.FullName.Contains(search)||
                d.applicationUser.Email.Contains(search)||
                d.specialization.NameAr.Contains(search)||
                d.specialization.NameEn.Contains(search)
                );
            }

            //Pagination
            var paginatedResult=query
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .Select(d => new DoctorWithSpecializationDto
                {
                    Image=d.applicationUser.Image,
                    Email=d.applicationUser.Email,
                    FullName=d.applicationUser.FullName,
                    Phone=d.applicationUser.PhoneNumber,
                    Gender=(Gender)d.applicationUser.Gender,
                    SpecializationId=d.SpecializationId,
                    SpecializationNameEN = d.specialization.NameAr,
                    SpecializationNameAR = d.specialization.NameEn
                })
                .ToList();

            return paginatedResult;
        }

        public DoctorWithSpecializationDto GetById(string id)
        {
            var doctorWithSpecialization = _context.Doctors
                .Include(d => d.specialization)
                .Where(d => d.Id == id)
                .Select(d => new DoctorWithSpecializationDto
                {
                    Image = d.applicationUser.Image,
                    Email = d.applicationUser.Email,
                    FullName = d.applicationUser.FullName,
                    Phone = d.applicationUser.PhoneNumber,
                    Gender = (Gender)d.applicationUser.Gender,
                    SpecializationNameEN = d.specialization.NameAr,
                    SpecializationNameAR = d.specialization.NameEn
                }).FirstOrDefault();
            return doctorWithSpecialization;
        }

        public int NumberOfDoctor()
        {
            var doctors = _context.Doctors.Count();
            return doctors;
        }

        public List<DoctorDto> SearchOnDoctors(int pageNumber, int pageSize, string search)
        {
            var query = _context.Doctors
                .Include(d => d.specialization)
                .Include(d => d.appointments)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(d =>
                d.applicationUser.FullName.Contains(search) ||
                d.applicationUser.Email.Contains(search) ||
                d.specialization.NameAr.Contains(search) ||
                d.specialization.NameEn.Contains(search)
                );
            }

            var paginatedResult = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(d => new DoctorDto
                {
                    Image = d.applicationUser.Image,
                    FullName = d.applicationUser.FullName,
                    Email = d.applicationUser.Email,
                    Phone = d.applicationUser.PhoneNumber,
                    Gender = (Gender)d.applicationUser.Gender,
                    SpeciaizationNameEN = d.specialization.NameEn,
                    SpeciaizationNameAR = d.specialization.NameAr,
                    Price = (decimal)d.Price,
                    Appointments = d.appointments.ToList()
                }).ToList();

            return paginatedResult;
        }

        public List<TopDoctorDto> Top10Doctor()
        {
            var top10Doctors = _context.Doctors
          .Select(d => new TopDoctorDto
          {
              Image = d.applicationUser.Image,
              FullName = d.applicationUser.FullName,
              Specialize = d.applicationUser.FullName,
              Requests = d.requests.Count()
          })
          .OrderByDescending(d => d.Requests)
          .Take(10)
          .ToList();

            return top10Doctors;
        }

        public List<Top5SpecializeDto> Top5Specialize()
        {
            var specialize = _context.Specializations.
            Select(d => new Top5SpecializeDto
            {


                NameEn = d.NameEn,
                NameAr = d.NameAr,
                Requests = d.Doctors.Count(),


            })
             .OrderByDescending(d => d.Requests)
             .Take(5)
             .ToList();
            return specialize;
        }

        public void Update(string id, DoctorWithSpecializationDto doctorDto)
        {
            Doctor oldDoctor=_context.Doctors.Include(d=>d.applicationUser)
                                             .Include(d => d.specialization)
                                             .FirstOrDefault(d=>d.Id==id);

            if(oldDoctor != null)
            {
                oldDoctor.applicationUser.Image = doctorDto.Image;
                oldDoctor.applicationUser.FullName = doctorDto.FullName;
                oldDoctor.applicationUser.Email = doctorDto.Email;
                oldDoctor.applicationUser.Gender = doctorDto.Gender;
                oldDoctor.applicationUser.DateofBirth = doctorDto.DateOfBirth;
                oldDoctor.applicationUser.PhoneNumber = doctorDto.Phone;
            }

            oldDoctor.SpecializationId =(int) doctorDto.SpecializationId;

            _context.SaveChanges();
        }
    }
}
