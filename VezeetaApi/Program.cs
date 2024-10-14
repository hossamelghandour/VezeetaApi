
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VezeetaApi.Data;
using VezeetaApi.Filters;
using VezeetaApi.Models;
using VezeetaApi.Repository.AppointmentRepo;
using VezeetaApi.Repository.DiscountRepo;
using VezeetaApi.Repository.DoctorRepo;
using VezeetaApi.Repository.PatientRepo;
using VezeetaApi.Repository.RequestRepo;
using VezeetaApi.Repository.SpacializationRepo;

namespace VezeetaApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

 //**Important** // this config for swagger to enable autherization but if there any problem please test it in Postman it work done there
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT token in the format 'Bearer {token}'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

                options.OperationFilter<AddAuthorizationHeaderOperationFilter>();
            });

            // add DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));


            // Configure identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // register dependency injection
            builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<RoleInitializer>();
            builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
            builder.Services.AddScoped<IDiscountRepo, DiscountRepo>();
            builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IRequestRepo, RequestRepo>();
            builder.Services.AddScoped<ISpecializationRepo, SpecializationRepo>();




            // jwt token
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
            options.TokenValidationParameters = new TokenValidationParameters
           {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
           };
           });

            //add My Roles
            
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var rolManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Doctor", "Patient" };

                foreach (var role in roles)
                {
                    if (!await rolManager.RoleExistsAsync(role))
                    {
                        await rolManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string email = "admin@gmail.com";

                var user = await userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var roleExists = await roleManager.RoleExistsAsync("Admin");

                    if (!roleExists)
                    {

                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }


                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {

                    Console.WriteLine("User not found with the specified email.");
                }
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(options =>
            {

                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();

            });



            app.MapControllers();

            app.Run();
        }
    }
}
