using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VezeetaApi.Data;
using VezeetaApi.Dto;
using VezeetaApi.Models;

namespace VezeetaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(AppDbContext context, RoleManager<IdentityRole> roleManager
            , UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegistrationAsync([FromForm] RegisterDto registerUserDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.FullName=registerUserDto.FullName;
                user.UserName = registerUserDto.FullName.ToLower();
                user.PhoneNumber = registerUserDto.Phone;
                user.Gender = registerUserDto.Gender;
                user.Image = registerUserDto.Image;
                user.Email=registerUserDto.Email;
                user.DateofBirth = registerUserDto.DateOfBirth;
                user.SecurityStamp=Guid.NewGuid().ToString();
                user.Type = AccountType.Patient;
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;

                IdentityResult result = await _userManager.CreateAsync(user, registerUserDto.Password);


                if (result.Succeeded)
                {
                    ApplicationUser CreatedUser = await _userManager.FindByNameAsync(user.UserName);
                    var roleAssignmentResult = await _userManager.AddToRoleAsync(CreatedUser, "Patient");

                    if (roleAssignmentResult.Succeeded)
                    {
                        return Ok("Account Added Successfuly");
                    }
                    else
                    {
                        return BadRequest("Role Assignment Failed");
                    }
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Emial);

            if(user !=null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>();

                authClaims.Add(new Claim(ClaimTypes.Name, user.Email));
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                //get roles

                var roleuser = await _userManager.FindByEmailAsync(user.Email);

                var roles = await _userManager.GetRolesAsync(roleuser);
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                SecurityKey authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var serializedToken = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = serializedToken,
                    expiration = token.ValidTo
                });

            }

            return Unauthorized("Invalid credentials. Please check your email and password.");
        }
    }
}
