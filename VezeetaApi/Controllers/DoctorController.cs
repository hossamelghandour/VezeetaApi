using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using VezeetaApi.Dto;
using VezeetaApi.Models;
using VezeetaApi.Repository.DoctorRepo;
using VezeetaApi.Repository.SpacializationRepo;
using VezeetaApi.Repository;
using VezeetaApi.Repository.RequestRepo;

namespace VezeetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager ;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IBaseRepo<Doctor> _baseRepo;
        private readonly IBaseRepo<Specialization> _specializationrepo;
        private readonly IRequestRepo _requestRepo;

        public DoctorController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IDoctorRepo doctorRepo, IBaseRepo<Doctor> baseRepo, IBaseRepo<Specialization> specializationrepo, IRequestRepo requestRepo)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _doctorRepo = doctorRepo;
            _baseRepo = baseRepo;
            _specializationrepo = specializationrepo;
            _requestRepo = requestRepo;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult GetAll(int pageNumber=1,int pageSize=10,string search="")
        {
            var doctors=_doctorRepo.GetAll(pageNumber,pageSize,search);
            return Ok(doctors);
        }

        [HttpGet("id")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById([FromForm] string id)
        {
            var doctor = _doctorRepo.GetById(id);
            if(doctor!= null)
            {
                return Ok(doctor);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromForm] DoctorWithSpecializationDto doctorDto)
        {
            var specialization = _specializationrepo.GettById((int)doctorDto.SpecializationId);
            if (specialization == null)
            {
                return BadRequest("Invalid specialization ID");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser
            {
                Image = doctorDto.Image,
                FullName = doctorDto.FullName,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.Phone,
                Gender = doctorDto.Gender,
                DateofBirth = doctorDto.DateOfBirth,
                Type = AccountType.Doctor,
                LockoutEnabled = true,
                EmailConfirmed = true,
                UserName = doctorDto.FullName.ToLower(),
            };
            var result = await _userManager.CreateAsync(user, doctorDto.Password);

            if (result.Succeeded)
            {
                var doctor = new Doctor
                {

                    SpecializationId = (int)doctorDto.SpecializationId,
                    applicationUser = user
                };

                _baseRepo.ADD(doctor);

                string username = user.UserName;

                // ***important** //the doctor will added successfully but u might face problem in secure SMTP in sawgger to send email but the code is work correctly
                SendEmail(doctorDto.Email, username, doctorDto.Password);



                // Assign the role to the user
                await _userManager.AddToRoleAsync(user, "Doctor");

                return Ok("Doctor added successfully");
            }
            else
            {

                return BadRequest(result.Errors);
            }


        }

        //Bonus For sending email methods
        private void SendEmail(string email, string username, string password)
        {
            string senderEmail = "jossaf168@gmail.com";
            string senderPassword = "hohohmooo22";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };

            MailMessage mailMessage = new MailMessage(senderEmail, email)
            {
                Subject = "Welcome to the platform!",
                Body = $"Hello,\n\nYour username is: {username}\nYour password is: {password}\n\nWelcome aboard!"
            };

            client.Send(mailMessage);
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit([FromForm] string id,DoctorWithSpecializationDto doctorDto)
        {
            _doctorRepo.Update(id,doctorDto);
            return Ok(true);
        }


        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromForm] string id)
        {
            bool hasRequest=_requestRepo.HasRequests(id);
            if (hasRequest)
            {
                return BadRequest("Cannot delete the doctor as there are associated requests.");
            }
            _doctorRepo.Delete(id);
            return Ok(true);
        }


        [HttpGet("SearchOnDoctors")]
        [Authorize(Roles = "Patient")]
        public IActionResult SearchOnDoctor(int pageNumber = 1, int pageSize = 10, string search = "")
        {

            var doctors = _doctorRepo.SearchOnDoctors(pageNumber, pageSize, search);

            return Ok(doctors);
        }
    }
}
